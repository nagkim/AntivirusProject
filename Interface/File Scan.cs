using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interface
{
    public partial class File_Scan : Form
    {
        private OpenFileDialog openFileDialog;
        private const string HybridAnalysisApiKey = "j6763n5jfb8bdc8c1439rylm273773d60xx91ymx547cecfcb2qoqr1l93a0ed99"; // Replace with your actual API key
        private const string HybridAnalysisApiUrl = "https://www.hybrid-analysis.com/api/v2/"; // Replace with the actual API URL

        public File_Scan()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files|*.*"; // You can set the filter to the type of files you want to allow
            pictureBox2.Visible = false; // Initially hide the picture box
            pictureBox3.Visible = false; // Initially hide the picture box
            pictureBox4.Visible = false; // Initially hide the picture box
            pictureBox5.Visible = false; // Initially hide the picture box
            listBox1.Visible = false; // Initially hide the list box
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                ResetLabels();
                textBox1.Text = "File path: " + selectedFilePath;

                pictureBox4.Visible = true; // Show picture box when file is selected

                string scanResult = await GetHybridAnalysisReputation(selectedFilePath);
                textBox3.Text = scanResult;
                pictureBox4.Visible = false; // Hide picture box after upload status
                pictureBox2.Visible = false; // Hide picture box when result is displayed

            }
        }

        private void ResetLabels()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox2.ForeColor = System.Drawing.Color.Black; // Reset to default color
            textBox3.ForeColor = System.Drawing.Color.Black; // Reset to default color
            pictureBox2.Visible = false; // Hide picture box when resetting
            pictureBox3.Visible = false; // Hide picture box when resetting
            pictureBox5.Visible = false; // Hide picture box when resetting
            listBox1.Visible = false; // Hide list box when resetting
            listBox1.Items.Clear(); // Clear listBox1 when resetting
        }

        private async Task<string> GetHybridAnalysisReputation(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                MessageBox.Show("File path is invalid or file does not exist.");
                return "unknown";
            }

            string jobId = await SubmitFileForScanning(filePath, "120");
            if (jobId != null)
            {
                pictureBox4.Visible = false;
                textBox2.Text = "Success";
                textBox2.ForeColor = System.Drawing.Color.Green; // Set color to green
                pictureBox2.Visible = true; // Show picture box when upload is successful
            }
            else
            {
                textBox2.Text = "Failed";
                textBox2.ForeColor = System.Drawing.Color.Red; // Set color to red
                return "unknown";
            }

            int maxRetries = 40;
            int retryCount = 0;
            var status = await CheckSubmissionStatus(jobId);

            while (status != null && status.state != "SUCCESS" && status.state != "ERROR" && retryCount < maxRetries)
            {
                await Task.Delay(60000);
                status = await CheckSubmissionStatus(jobId);
                retryCount++;
            }

            if (status == null || status.state == "ERROR")
            {
                MessageBox.Show("Failed to get successful scan status.");
                return "unknown";
            }

            if (status.state == "SUCCESS")
            {
                var report = await GetAnalysisReport(jobId);
                if (report != null)
                {
                    var reportText = FormatReport(report);

                    // Populate listBox1 with detailed report
                    PopulateListBox(report);
                    listBox1.Visible = true; // Show list box when results are ready

                    if (InterpretReport(report))
                    {
                        pictureBox3.Visible = true; // Show picture box when the result is no specific threat
                        textBox3.ForeColor = System.Drawing.Color.Green;
                        return "No Specific Threat";
                    }
                    else
                    {
                        pictureBox5.Visible = true; // Show picture box when the result is malicious
                        textBox3.ForeColor = System.Drawing.Color.Red; // Set color to red for malicious result
                        return "Malicious";
                    }
                }
                else
                {
                    MessageBox.Show("Failed to retrieve analysis report.");
                }
            }

            return "unknown";
        }

        private async Task<string> SubmitFileForScanning(string filePath, string environmentId)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Falcon Sandbox");
            client.DefaultRequestHeaders.Add("api-key", HybridAnalysisApiKey.Trim());

            try
            {
                using var content = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(fileContent, "file", System.IO.Path.GetFileName(filePath));
                content.Add(new StringContent(environmentId), "environment_id");

                var response = await client.PostAsync($"{HybridAnalysisApiUrl}submit/file", content);
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<SubmissionResponse>(await response.Content.ReadAsStringAsync());
                    return result?.job_id;
                }
                else
                {
                    MessageBox.Show("Failed to submit file: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting file for scanning: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        private async Task<SubmissionStatus> CheckSubmissionStatus(string jobId)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Falcon Sandbox");
            client.DefaultRequestHeaders.Add("api-key", HybridAnalysisApiKey.Trim());

            try
            {
                var response = await client.GetAsync($"{HybridAnalysisApiUrl}report/{jobId}/state");
                if (response.IsSuccessStatusCode)
                {
                    var status = JsonSerializer.Deserialize<SubmissionStatus>(await response.Content.ReadAsStringAsync());
                    return status;
                }
                else
                {
                    MessageBox.Show("Failed to check submission status: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking submission status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        private async Task<AnalysisReport> GetAnalysisReport(string jobId)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Falcon Sandbox");
            client.DefaultRequestHeaders.Add("api-key", HybridAnalysisApiKey.Trim());

            try
            {
                var response = await client.GetAsync($"{HybridAnalysisApiUrl}report/{jobId}/summary");
                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        AllowTrailingCommas = true
                    };
                    var report = JsonSerializer.Deserialize<AnalysisReport>(await response.Content.ReadAsStringAsync(), options);
                    return report;
                }
                else
                {
                    MessageBox.Show("Failed to retrieve analysis report: " + response.ReasonPhrase);
                }
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show($"JSON Error: {jsonEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving analysis report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        private string FormatReport(AnalysisReport report)
        {
            return $"Verdict: {report.verdict}\n" +
                   $"Threat Score: {report.threat_score}\n" +
                   $"Threat Level: {report.threat_level}\n";
        }

        private bool InterpretReport(AnalysisReport report)
        {
            string verdict = report.verdict ?? "unknown";
            int? threatScore = report.threat_score;
            int? threatLevel = report.threat_level;

            return verdict == "no specific threat" && (threatScore == null || threatScore == 0) && (threatLevel == null || threatLevel == 0);
        }


        private void PopulateListBox(AnalysisReport report)
        {
            listBox1.Items.Add($"Verdict: {report.verdict}");
            listBox1.Items.Add($"Threat Score: {report.threat_score}");
            listBox1.Items.Add($"Threat Level: {report.threat_level}");
            // Add detailed analysis to listBox1
            listBox1.Items.Add($"Environment: {report.environment_description}");
            // Removed signatures display
            listBox1.Items.Add($"Classification Tags: {string.Join(", ", report.classification_tags)}");
            listBox1.Items.Add($"AV Detections: {report.av_detect}");
            listBox1.Items.Add($"VX Family: {report.vx_family}");
        }
        private class SubmissionResponse
        {
            public string job_id { get; set; }
        }

        private class SubmissionStatus
        {
            public string state { get; set; }
        }



        private class AnalysisReport
        {
            public string verdict { get; set; }
            public int? threat_score { get; set; }
            public int? threat_level { get; set; }
            public string environment_description { get; set; }
            // Removed the signatures property
            public List<string> classification_tags { get; set; } = new List<string>();
            public int av_detect { get; set; }
            public string vx_family { get; set; }
        }
        private void File_Scan_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}