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
			this.PathToScanLabel = new System.Windows.Forms.Label();
			this.PathToScanTextBox = new System.Windows.Forms.TextBox();
			this.SaveButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.SelectPathToScanButton = new System.Windows.Forms.Button();
			this.PathToSaveTextBox = new System.Windows.Forms.TextBox();
			this.PathToSaveLabel = new System.Windows.Forms.Label();
			this.SelectPathToSaveButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// PathToScanLabel
			// 
			this.PathToScanLabel.AutoSize = true;
			this.PathToScanLabel.Location = new System.Drawing.Point(13, 13);
			this.PathToScanLabel.Name = "PathToScanLabel";
			this.PathToScanLabel.Size = new System.Drawing.Size(91, 13);
			this.PathToScanLabel.TabIndex = 0;
			this.PathToScanLabel.Text = "Path to input files:";
			// 
			// PathToScanTextBox
			// 
			this.PathToScanTextBox.Location = new System.Drawing.Point(12, 29);
			this.PathToScanTextBox.Name = "PathToScanTextBox";
			this.PathToScanTextBox.Size = new System.Drawing.Size(232, 20);
			this.PathToScanTextBox.TabIndex = 1;
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
			// SelectPathToScanButton
			// 
			this.SelectPathToScanButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SelectPathToScanButton.Location = new System.Drawing.Point(247, 29);
			this.SelectPathToScanButton.Name = "SelectPathToScanButton";
			this.SelectPathToScanButton.Size = new System.Drawing.Size(20, 20);
			this.SelectPathToScanButton.TabIndex = 4;
			this.SelectPathToScanButton.Text = "...";
			this.SelectPathToScanButton.UseVisualStyleBackColor = true;
			this.SelectPathToScanButton.Click += new System.EventHandler(this.selectPathButton_Click);
			// 
			// PathToSaveTextBox
			// 
			this.PathToSaveTextBox.Location = new System.Drawing.Point(12, 79);
			this.PathToSaveTextBox.Name = "PathToSaveTextBox";
			this.PathToSaveTextBox.Size = new System.Drawing.Size(232, 20);
			this.PathToSaveTextBox.TabIndex = 5;
			// 
			// PathToSaveLabel
			// 
			this.PathToSaveLabel.AutoSize = true;
			this.PathToSaveLabel.Location = new System.Drawing.Point(13, 63);
			this.PathToSaveLabel.Name = "PathToSaveLabel";
			this.PathToSaveLabel.Size = new System.Drawing.Size(103, 13);
			this.PathToSaveLabel.TabIndex = 6;
			this.PathToSaveLabel.Text = "Path to report folder:";
			// 
			// SelectPathToSaveButton
			// 
			this.SelectPathToSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SelectPathToSaveButton.Location = new System.Drawing.Point(247, 79);
			this.SelectPathToSaveButton.Name = "SelectPathToSaveButton";
			this.SelectPathToSaveButton.Size = new System.Drawing.Size(20, 20);
			this.SelectPathToSaveButton.TabIndex = 7;
			this.SelectPathToSaveButton.Text = "...";
			this.SelectPathToSaveButton.UseVisualStyleBackColor = true;
			this.SelectPathToSaveButton.Click += new System.EventHandler(this.selectPathButton_Click);
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(280, 157);
			this.Controls.Add(this.SelectPathToSaveButton);
			this.Controls.Add(this.PathToSaveLabel);
			this.Controls.Add(this.PathToSaveTextBox);
			this.Controls.Add(this.SelectPathToScanButton);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.SaveButton);
			this.Controls.Add(this.PathToScanTextBox);
			this.Controls.Add(this.PathToScanLabel);
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

		private System.Windows.Forms.Label PathToScanLabel;
		private System.Windows.Forms.TextBox PathToScanTextBox;
		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.Button SelectPathToScanButton;
		private System.Windows.Forms.TextBox PathToSaveTextBox;
		private System.Windows.Forms.Label PathToSaveLabel;
		private System.Windows.Forms.Button SelectPathToSaveButton;
	}
}