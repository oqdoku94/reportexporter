namespace ReportExporter
{
	partial class SettingsForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.folderPathTextBox = new System.Windows.Forms.TextBox();
			this.SaveButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.selectPathButton = new System.Windows.Forms.Button();
			this.reportPathTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SelectReportPathButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Path to input files:";
			// 
			// folderPathTextBox
			// 
			this.folderPathTextBox.Location = new System.Drawing.Point(12, 29);
			this.folderPathTextBox.Name = "folderPathTextBox";
			this.folderPathTextBox.Size = new System.Drawing.Size(232, 20);
			this.folderPathTextBox.TabIndex = 1;
			// 
			// SaveButton
			// 
			this.SaveButton.Enabled = false;
			this.SaveButton.Location = new System.Drawing.Point(28, 122);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(88, 23);
			this.SaveButton.TabIndex = 2;
			this.SaveButton.Text = "Save";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// CloseButton
			// 
			this.CloseButton.Location = new System.Drawing.Point(156, 122);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(88, 23);
			this.CloseButton.TabIndex = 1;
			this.CloseButton.Text = "Close";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// selectPathButton
			// 
			this.selectPathButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.selectPathButton.Location = new System.Drawing.Point(247, 29);
			this.selectPathButton.Name = "selectPathButton";
			this.selectPathButton.Size = new System.Drawing.Size(20, 20);
			this.selectPathButton.TabIndex = 4;
			this.selectPathButton.Text = "...";
			this.selectPathButton.UseVisualStyleBackColor = true;
			this.selectPathButton.Click += new System.EventHandler(this.selectPathButton_Click);
			// 
			// reportPathTextBox
			// 
			this.reportPathTextBox.Location = new System.Drawing.Point(12, 79);
			this.reportPathTextBox.Name = "reportPathTextBox";
			this.reportPathTextBox.Size = new System.Drawing.Size(232, 20);
			this.reportPathTextBox.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(103, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Path to report folder:";
			// 
			// SelectReportPathButton
			// 
			this.SelectReportPathButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SelectReportPathButton.Location = new System.Drawing.Point(247, 79);
			this.SelectReportPathButton.Name = "SelectReportPathButton";
			this.SelectReportPathButton.Size = new System.Drawing.Size(20, 20);
			this.SelectReportPathButton.TabIndex = 7;
			this.SelectReportPathButton.Text = "...";
			this.SelectReportPathButton.UseVisualStyleBackColor = true;
			this.SelectReportPathButton.Click += new System.EventHandler(this.selectPathButton_Click);
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(280, 157);
			this.Controls.Add(this.SelectReportPathButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.reportPathTextBox);
			this.Controls.Add(this.selectPathButton);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.SaveButton);
			this.Controls.Add(this.folderPathTextBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Settings";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox folderPathTextBox;
		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.Button selectPathButton;
		private System.Windows.Forms.TextBox reportPathTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button SelectReportPathButton;
	}
}