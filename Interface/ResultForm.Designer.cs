using System.Drawing;
using System.Windows.Forms;

namespace Interface
{
    partial class ResultForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startup_programs_grid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.startup_programs_grid)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 17);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "New Scan";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(288, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scan Results";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(305, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 2;
            // 
            // startup_grid
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
            this.startup_programs_grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.startup_programs_grid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.startup_programs_grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.startup_programs_grid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.startup_programs_grid.BorderStyle = System.Windows.Forms.BorderStyle.None;

            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            headerStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            headerStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            headerStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.startup_programs_grid.ColumnHeadersDefaultCellStyle = headerStyle;

            this.startup_programs_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            cellStyle.BackColor = System.Drawing.SystemColors.Window;
            cellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.startup_programs_grid.DefaultCellStyle = cellStyle;

            this.startup_programs_grid.EnableHeadersVisualStyles = false;
            this.startup_programs_grid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.startup_programs_grid.Location = new System.Drawing.Point(12, 77);
            this.startup_programs_grid.Name = "services_grid";

            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle();
            rowStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            rowStyle.Font = new System.Drawing.Font("Arial", 7F);
            rowStyle.ForeColor = System.Drawing.Color.Black;
            this.startup_programs_grid.RowsDefaultCellStyle = rowStyle;

            this.startup_programs_grid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Gray;
            this.startup_programs_grid.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;

            this.startup_programs_grid.Size = new System.Drawing.Size(597, 377);
            this.startup_programs_grid.TabIndex = 3;
            // 
            // ResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(721, 433);
            this.Controls.Add(this.startup_programs_grid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ResultForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ResultForm";
            this.Load += new System.EventHandler(this.ResultForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.startup_programs_grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView startup_programs_grid;
    }
}