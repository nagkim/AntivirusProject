using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace Interface
{
    public partial class Services : Form
    {
        private static readonly string[] SuspiciousPaths = { "AppData", "Temp", "Roaming", "Local" };
        private System.Timers.Timer updateTimer;
        private Dictionary<string, PerformanceCounter> cpuCounters = new Dictionary<string, PerformanceCounter>();
        private Dictionary<string, PerformanceCounter> memoryCounters = new Dictionary<string, PerformanceCounter>();
        private DataTable servicesTable;
        private Dictionary<string, Queue<double>> cpuUsageHistory = new Dictionary<string, Queue<double>>();
        private Dictionary<string, Queue<double>> memoryUsageHistory = new Dictionary<string, Queue<double>>();
        private int logicalProcessorCount;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetSystemTimes(out FILETIME lpIdleTime, out FILETIME lpKernelTime, out FILETIME lpUserTime);

        [StructLayout(LayoutKind.Sequential)]
        struct FILETIME
        {
            public uint dwLowDateTime;
            public uint dwHighDateTime;
        }

        private ulong GetCpuTime(FILETIME time)
        {
            return ((ulong)time.dwHighDateTime << 32) | time.dwLowDateTime;
        }

        private double CalculateCpuUsage(string serviceName, ulong previousIdleTime, ulong previousKernelTime, ulong previousUserTime)
        {
            FILETIME idleTime, kernelTime, userTime;
            if (!GetSystemTimes(out idleTime, out kernelTime, out userTime))
            {
                return 0.0;
            }

            ulong currentIdleTime = GetCpuTime(idleTime);
            ulong currentKernelTime = GetCpuTime(kernelTime);
            ulong currentUserTime = GetCpuTime(userTime);

            ulong idleTimeDifference = currentIdleTime - previousIdleTime;
            ulong kernelTimeDifference = currentKernelTime - previousKernelTime;
            ulong userTimeDifference = currentUserTime - previousUserTime;
            ulong totalTimeDifference = kernelTimeDifference + userTimeDifference;

            if (totalTimeDifference == 0)
            {
                return 0.0;
            }

            return (1.0 - ((double)idleTimeDifference / totalTimeDifference)) * 100.0;
        }

        public Services()
        {
            InitializeComponent();
            InitializeUpdateTimer();
            InitializeCharts();
        }

        private void Services_Load(object sender, EventArgs e)
        {
            logicalProcessorCount = GetLogicalProcessorCount();
            ListServices();
            StartUpdateTimer();
        }

        private void InitializeUpdateTimer()
        {
            updateTimer = new System.Timers.Timer(5000); // Update every 5 seconds
            updateTimer.Elapsed += UpdateTimer_Elapsed;
            updateTimer.AutoReset = true;
            updateTimer.Enabled = true; // Start timer at the beginning
        }

        private void StartUpdateTimer()
        {
            updateTimer.Enabled = true;
        }

        private void InitializeCharts()
        {
            InitializeChart(cpuUsageChart, "CPU Usage Chart (%)");
            InitializeChart(memoryUsageChart, "Memory Usage Chart (MB)");
        }

        private void InitializeChart(Chart chart, string title)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            var chartArea = new ChartArea("Chart Area");
            chart.ChartAreas.Add(chartArea);
            chart.Titles.Clear();
            chart.Titles.Add(title);
            chart.Dock = DockStyle.None;

            // Add axis labels
            chartArea.AxisX.Title = "Time";
            if (chart == cpuUsageChart)
            {
                chartArea.AxisY.Title = "CPU Usage (%)";
            }
            else if (chart == memoryUsageChart)
            {
                chartArea.AxisY.Title = "Memory Usage (MB)";
            }
        }

        private void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateCpuUsage();
            UpdateMemoryUsage();
        }

        private void ListServices()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ListServices));
                return;
            }

            servicesTable = new DataTable();
            servicesTable.Columns.Add("Name", typeof(string));
            servicesTable.Columns.Add("Display Name", typeof(string));
            servicesTable.Columns.Add("Status", typeof(string));
            servicesTable.Columns.Add("Start Type", typeof(string));
            servicesTable.Columns.Add("Description", typeof(string));
            servicesTable.Columns.Add("Executable Path", typeof(string));
            servicesTable.Columns.Add("CPU Usage (%)", typeof(string)); // New column for CPU usage
            servicesTable.Columns.Add("Memory Usage (MB)", typeof(string)); // New column for Memory usage
            servicesTable.Columns.Add("Suspicious", typeof(bool)); // New column for Suspicious flag

            try
            {
                ServiceController[] services = ServiceController.GetServices();
                foreach (ServiceController service in services)
                {
                    string startType = GetServiceStartType(service);
                    string description = GetServiceDescription(service);
                    string executablePath = GetServiceExecutablePath(service);
                    bool isSuspicious = CheckIfServiceIsSuspicious(service);

                    servicesTable.Rows.Add(service.ServiceName, service.DisplayName, service.Status.ToString(), startType, description, executablePath, "0.00", "0.00", isSuspicious);
                    cpuUsageHistory[service.ServiceName] = new Queue<double>(new double[60]);
                    memoryUsageHistory[service.ServiceName] = new Queue<double>(new double[60]);
                }

                services_grid.DataSource = servicesTable;

                // Set column widths manually
                services_grid.Columns[0].Width = 50; // Name
                services_grid.Columns[1].Width = 50; // Display Name
                services_grid.Columns[2].Width = 50; // Status
                services_grid.Columns[3].Width = 50; // Start Type
                services_grid.Columns[4].Width = 55; // Description
                services_grid.Columns[5].Width = 60; // Executable Path
                services_grid.Columns[6].Width = 50; // CPU Usage (%)
                services_grid.Columns[7].Width = 50; // Memory Usage (MB)
                services_grid.Columns[8].Width = 70; // Suspicious

                // Set the sort mode of the CPU and Memory Usage columns
                services_grid.Columns["CPU Usage (%)"].SortMode = DataGridViewColumnSortMode.Automatic;
                services_grid.Columns["Memory Usage (MB)"].SortMode = DataGridViewColumnSortMode.Automatic;

                UpdateServiceCounts();
                ColorRowsBasedOnSuspicion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error listing services: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCpuUsage()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateCpuUsage));
                return;
            }

            try
            {
                FILETIME previousIdleTime, previousKernelTime, previousUserTime;
                GetSystemTimes(out previousIdleTime, out previousKernelTime, out previousUserTime);

                Thread.Sleep(1000); // Wait for a second to get a valid reading

                FILETIME currentIdleTime, currentKernelTime, currentUserTime;
                GetSystemTimes(out currentIdleTime, out currentKernelTime, out currentUserTime);

                foreach (DataRow row in servicesTable.Rows)
                {
                    string serviceName = row["Name"].ToString();
                    double cpuUsage = CalculateCpuUsage(serviceName, GetCpuTime(previousIdleTime), GetCpuTime(previousKernelTime), GetCpuTime(previousUserTime));
                    row["CPU Usage (%)"] = cpuUsage.ToString("F2");
                    UpdateChart(cpuUsageChart, serviceName, cpuUsage, cpuUsageHistory);
                }

                services_grid.DataSource = servicesTable;
                services_grid.Refresh();

                // Sort the DataGridView by CPU Usage column
                services_grid.Sort(services_grid.Columns["CPU Usage (%)"], System.ComponentModel.ListSortDirection.Descending);

                UpdateServiceCounts();
                ColorRowsBasedOnSuspicion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating CPU usage: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateMemoryUsage()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateMemoryUsage));
                return;
            }

            try
            {
                foreach (DataRow row in servicesTable.Rows)
                {
                    string serviceName = row["Name"].ToString();
                    double memoryUsage = GetMemoryUsage(serviceName);
                    row["Memory Usage (MB)"] = memoryUsage.ToString("F2");
                    UpdateChart(memoryUsageChart, serviceName, memoryUsage, memoryUsageHistory);
                }

                services_grid.DataSource = servicesTable;
                services_grid.Refresh();

                // Sort the DataGridView by Memory Usage column
                services_grid.Sort(services_grid.Columns["Memory Usage (MB)"], System.ComponentModel.ListSortDirection.Descending);

                ColorRowsBasedOnSuspicion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating memory usage: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateChart(Chart chart, string serviceName, double usage, Dictionary<string, Queue<double>> usageHistory)
        {
            if (!usageHistory.ContainsKey(serviceName))
                return;

            var history = usageHistory[serviceName];
            if (history.Count >= 60)
                history.Dequeue();

            history.Enqueue(usage);

            if (!chart.Series.Any(s => s.Name == serviceName))
            {
                var series = new Series(serviceName)
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2
                };
                chart.Series.Add(series);
            }

            var seriesToUpdate = chart.Series[serviceName];
            seriesToUpdate.Points.Clear();
            foreach (var value in history)
            {
                seriesToUpdate.Points.AddY(value);
            }

            // Adjust chart area to fit the new data
            var chartArea = chart.ChartAreas[0];
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = history.Count;
            chartArea.AxisY.Minimum = 0;

            // Set different maximum Y-axis based on the chart
            if (chart == memoryUsageChart)
            {
                chartArea.AxisY.Maximum = 1000; // For memory usage chart
            }
            else
            {
                chartArea.AxisY.Maximum = 100; // For other charts like CPU usage
            }

            chart.Invalidate();
        }

        private double GetMemoryUsage(string serviceName)
        {
            // Simulate memory usage for demonstration purposes
            Random rand = new Random();
            return rand.Next(50, 500);
        }

        private PerformanceCounter GetMemoryCounter(string serviceName)
        {
            if (!memoryCounters.ContainsKey(serviceName))
            {
                try
                {
                    memoryCounters[serviceName] = new PerformanceCounter("Process", "Working Set", serviceName);
                }
                catch (Exception ex)
                {
                    // Handle the case where the performance counter instance does not exist
                    MessageBox.Show("Error creating memory counter for " + serviceName + ": " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    memoryCounters[serviceName] = null;
                }
            }

            return memoryCounters[serviceName];
        }

        private string GetServiceStartType(ServiceController service)
        {
            try
            {
                string registryPath = @"SYSTEM\CurrentControlSet\Services\" + service.ServiceName;
                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(registryPath))
                {
                    int startType = (int)key.GetValue("Start", -1);
                    return startType switch
                    {
                        2 => "Automatic",
                        3 => "Manual",
                        4 => "Disabled",
                        _ => "Unknown",
                    };
                }
            }
            catch
            {
                return "Unknown";
            }
        }

        private string GetServiceDescription(ServiceController service)
        {
            try
            {
                using (var registryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Services\{service.ServiceName}"))
                {
                    if (registryKey != null)
                    {
                        object description = registryKey.GetValue("Description");
                        if (description != null)
                        {
                            return description.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting service description: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return string.Empty;
        }

        private string GetServiceExecutablePath(ServiceController service)
        {
            try
            {
                using (var registryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Services\{service.ServiceName}"))
                {
                    if (registryKey != null)
                    {
                        object imagePath = registryKey.GetValue("ImagePath");
                        if (imagePath != null)
                        {
                            return imagePath.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting service executable path: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return string.Empty;
        }

        private bool CheckIfServiceIsSuspicious(ServiceController service)
        {
            string executablePath = GetServiceExecutablePath(service);
            return SuspiciousPaths.Any(path => executablePath.IndexOf(path, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void UpdateServiceCounts()
        {
            int safeCount = servicesTable.Rows.Cast<DataRow>().Count(row => !(bool)row["Suspicious"]);
            int suspiciousCount = servicesTable.Rows.Cast<DataRow>().Count(row => (bool)row["Suspicious"]);
            int totalCount = servicesTable.Rows.Count;

            safeCountLabel.Text = $"Safe: {safeCount}";
            susCountLabel.Text = $"Suspicious: {suspiciousCount}";
            totalCountLabel.Text = $"Total: {totalCount}";
        }

        private void ColorRowsBasedOnSuspicion()
        {
            foreach (DataGridViewRow row in services_grid.Rows)
            {
                if (row.Cells["Suspicious"].Value != null && (bool)row.Cells["Suspicious"].Value)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
            }
        }

        private int GetLogicalProcessorCount()
        {
            try
            {
                using (var searcher = new System.Management.ManagementObjectSearcher("select NumberOfLogicalProcessors from Win32_ComputerSystem"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        return Convert.ToInt32(obj["NumberOfLogicalProcessors"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting logical processor count: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return 1; // Default to 1 if the actual count cannot be determined
        }
    }
}