namespace ReportExporter
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileCloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileLoggerListBox = new System.Windows.Forms.ListBox();
			this.ExportButton = new System.Windows.Forms.Button();
			this.ReportCountTitleLabel = new System.Windows.Forms.Label();
			this.ReportCountLabel = new System.Windows.Forms.Label();
			this.InputFileWorker = new System.ComponentModel.BackgroundWorker();
			this.MainMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainMenu
			// 
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.SettingsMenuItem});
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(551, 24);
			this.MainMenu.TabIndex = 0;
			this.MainMenu.Text = "MainMenu";
			// 
			// FileMenuItem
			// 
			this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileCloseMenuItem});
			this.FileMenuItem.Name = "FileMenuItem";
			this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
			this.FileMenuItem.Text = "File";
			// 
			// FileCloseMenuItem
			// 
			this.FileCloseMenuItem.Name = "FileCloseMenuItem";
			this.FileCloseMenuItem.Size = new System.Drawing.Size(103, 22);
			this.FileCloseMenuItem.Text = "Close";
			this.FileCloseMenuItem.Click += new System.EventHandler(this.FileCloseMenuItem_Click);
			// 
			// SettingsMenuItem
			// 
			this.SettingsMenuItem.Name = "SettingsMenuItem";
			this.SettingsMenuItem.Size = new System.Drawing.Size(61, 20);
			this.SettingsMenuItem.Text = "Settings";
			this.SettingsMenuItem.Click += new System.EventHandler(this.SettingsMenuItem_Click);
			// 
			// FileLoggerListBox
			// 
			this.FileLoggerListBox.FormattingEnabled = true;
			this.FileLoggerListBox.Location = new System.Drawing.Point(12, 27);
			this.FileLoggerListBox.Name = "FileLoggerListBox";
			this.FileLoggerListBox.Size = new System.Drawing.Size(527, 186);
			this.FileLoggerListBox.TabIndex = 1;
			// 
			// ExportButton
			// 
			this.ExportButton.Enabled = false;
			this.ExportButton.Location = new System.Drawing.Point(412, 235);
			this.ExportButton.Name = "ExportButton";
			this.ExportButton.Size = new System.Drawing.Size(127, 23);
			this.ExportButton.TabIndex = 2;
			this.ExportButton.Text = "Export";
			this.ExportButton.UseVisualStyleBackColor = true;
			this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
			// 
			// ReportCountTitleLabel
			// 
			this.ReportCountTitleLabel.AutoSize = true;
			this.ReportCountTitleLabel.Location = new System.Drawing.Point(12, 240);
			this.ReportCountTitleLabel.Name = "ReportCountTitleLabel";
			this.ReportCountTitleLabel.Size = new System.Drawing.Size(80, 13);
			this.ReportCountTitleLabel.TabIndex = 3;
			this.ReportCountTitleLabel.Text = "Records found:";
			this.ReportCountTitleLabel.Visible = false;
			// 
			// ReportCountLabel
			// 
			this.ReportCountLabel.AutoSize = true;
			this.ReportCountLabel.ForeColor = System.Drawing.Color.Black;
			this.ReportCountLabel.Location = new System.Drawing.Point(89, 240);
			this.ReportCountLabel.Name = "ReportCountLabel";
			this.ReportCountLabel.Size = new System.Drawing.Size(13, 13);
			this.ReportCountLabel.TabIndex = 4;
			this.ReportCountLabel.Text = "0";
			this.ReportCountLabel.Visible = false;
			// 
			// InputFileWorker
			// 
			this.InputFileWorker.WorkerSupportsCancellation = true;
			this.InputFileWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.InputFileWorker_DoWork);
			this.InputFileWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.InputFileWorker_RunWorkerCompleted);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(551, 270);
			this.Controls.Add(this.ReportCountLabel);
			this.Controls.Add(this.ReportCountTitleLabel);
			this.Controls.Add(this.ExportButton);
			this.Controls.Add(this.FileLoggerListBox);
			this.Controls.Add(this.MainMenu);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.MainMenu;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "Report Exporter";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SettingsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FileCloseMenuItem;
		private System.Windows.Forms.ListBox FileLoggerListBox;
		private System.Windows.Forms.Button ExportButton;
		private System.Windows.Forms.Label ReportCountTitleLabel;
		private System.Windows.Forms.Label ReportCountLabel;
		private System.ComponentModel.BackgroundWorker InputFileWorker;
	}
}

