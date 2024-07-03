using System.Windows.Forms;

namespace Interface
{
    partial class Services
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView services_grid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart cpuUsageChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart memoryUsageChart;
        private System.Windows.Forms.Label safeCountLabel;
        private System.Windows.Forms.Label susCountLabel;
        private System.Windows.Forms.Label totalCountLabel;
        private System.Windows.Forms.Panel countPanel;
        private System.Windows.Forms.Label cpuUsageUnitLabel;
        private System.Windows.Forms.Label memoryUsageUnitLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.services_grid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cpuUsageChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.memoryUsageChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.safeCountLabel = new System.Windows.Forms.Label();
            this.susCountLabel = new System.Windows.Forms.Label();
            this.totalCountLabel = new System.Windows.Forms.Label();
            this.countPanel = new System.Windows.Forms.Panel();
            this.cpuUsageUnitLabel = new System.Windows.Forms.Label();
            this.memoryUsageUnitLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.services_grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpuUsageChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoryUsageChart)).BeginInit();
            this.countPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // services_grid
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
            this.services_grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.services_grid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.services_grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.services_grid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.services_grid.BorderStyle = System.Windows.Forms.BorderStyle.None;

            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            headerStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            headerStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            headerStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.services_grid.ColumnHeadersDefaultCellStyle = headerStyle;

            this.services_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            cellStyle.BackColor = System.Drawing.SystemColors.Window;
            cellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.services_grid.DefaultCellStyle = cellStyle;

            this.services_grid.EnableHeadersVisualStyles = false;
            this.services_grid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.services_grid.Location = new System.Drawing.Point(12, 77);
            this.services_grid.Name = "services_grid";

            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle();
            rowStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            rowStyle.Font = new System.Drawing.Font("Arial", 7F);
            rowStyle.ForeColor = System.Drawing.Color.Black;
            this.services_grid.RowsDefaultCellStyle = rowStyle;

            this.services_grid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Gray;
            this.services_grid.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;

            this.services_grid.Size = new System.Drawing.Size(597, 377);
            this.services_grid.TabIndex = 3;
            //
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(305, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Services";
            // 
            // cpuUsageChart
            // 
            this.cpuUsageChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cpuUsageChart.Location = new System.Drawing.Point(620, 77);
            this.cpuUsageChart.Name = "cpuUsageChart";
            this.cpuUsageChart.Size = new System.Drawing.Size(300, 200);
            this.cpuUsageChart.TabIndex = 6;
            // 
            // memoryUsageChart
            // 
            this.memoryUsageChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.memoryUsageChart.Location = new System.Drawing.Point(620, 283);
            this.memoryUsageChart.Name = "memoryUsageChart";
            this.memoryUsageChart.Size = new System.Drawing.Size(300, 200);
            this.memoryUsageChart.TabIndex = 7;
            // 
            // safeCountLabel
            // 
            this.safeCountLabel.AutoSize = true;
            this.safeCountLabel.BackColor = System.Drawing.Color.LimeGreen;
            this.safeCountLabel.ForeColor = System.Drawing.Color.White;
            this.safeCountLabel.Location = new System.Drawing.Point(3, 5);
            this.safeCountLabel.Name = "safeCountLabel";
            this.safeCountLabel.Size = new System.Drawing.Size(41, 13);
            this.safeCountLabel.TabIndex = 7;
            this.safeCountLabel.Text = "Safe: 0";
            // 
            // susCountLabel
            // 
            this.susCountLabel.AutoSize = true;
            this.susCountLabel.BackColor = System.Drawing.Color.Red;
            this.susCountLabel.ForeColor = System.Drawing.Color.White;
            this.susCountLabel.Location = new System.Drawing.Point(150, 5);
            this.susCountLabel.Name = "susCountLabel";
            this.susCountLabel.Size = new System.Drawing.Size(70, 13);
            this.susCountLabel.TabIndex = 8;
            this.susCountLabel.Text = "Suspicious: 0";
            // 
            // totalCountLabel
            // 
            this.totalCountLabel.AutoSize = true;
            this.totalCountLabel.Location = new System.Drawing.Point(300, 5);
            this.totalCountLabel.Name = "totalCountLabel";
            this.totalCountLabel.Size = new System.Drawing.Size(43, 13);
            this.totalCountLabel.TabIndex = 9;
            this.totalCountLabel.Text = "Total: 0";
            // 
            // countPanel
            // 
            this.countPanel.Controls.Add(this.safeCountLabel);
            this.countPanel.Controls.Add(this.susCountLabel);
            this.countPanel.Controls.Add(this.totalCountLabel);
            this.countPanel.Location = new System.Drawing.Point(12, 47);
            this.countPanel.Name = "countPanel";
            this.countPanel.Size = new System.Drawing.Size(577, 24);
            this.countPanel.TabIndex = 10;
            // 
            // cpuUsageUnitLabel
            // 
            this.cpuUsageUnitLabel.AutoSize = true;
            this.cpuUsageUnitLabel.Location = new System.Drawing.Point(600, 280);
            this.cpuUsageUnitLabel.Name = "cpuUsageUnitLabel";
            this.cpuUsageUnitLabel.Size = new System.Drawing.Size(80, 13);
            this.cpuUsageUnitLabel.TabIndex = 11;
            this.cpuUsageUnitLabel.Text = "CPU Usage (%)";
            // 
            // memoryUsageUnitLabel
            // 
            this.memoryUsageUnitLabel.AutoSize = true;
            this.memoryUsageUnitLabel.Location = new System.Drawing.Point(600, 490);
            this.memoryUsageUnitLabel.Name = "memoryUsageUnitLabel";
            this.memoryUsageUnitLabel.Size = new System.Drawing.Size(103, 13);
            this.memoryUsageUnitLabel.TabIndex = 12;
            this.memoryUsageUnitLabel.Text = "Memory Usage (MB)";
            // 
            // Services
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1004, 542);
            this.Controls.Add(this.countPanel);
            this.Controls.Add(this.memoryUsageUnitLabel);
            this.Controls.Add(this.cpuUsageUnitLabel);
            this.Controls.Add(this.memoryUsageChart);
            this.Controls.Add(this.cpuUsageChart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.services_grid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Services";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Services";
            this.Load += new System.EventHandler(this.Services_Load);
            ((System.ComponentModel.ISupportInitialize)(this.services_grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpuUsageChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoryUsageChart)).EndInit();
            this.countPanel.ResumeLayout(false);
            this.countPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }

}
