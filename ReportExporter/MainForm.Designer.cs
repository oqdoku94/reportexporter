namespace ReportExporter
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				components?.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileCloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileLoggerListBox = new System.Windows.Forms.ListBox();
			this.ExportButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.RecordsCountLabel = new System.Windows.Forms.Label();
			this.InputFileWorker = new System.ComponentModel.BackgroundWorker();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.SettingsMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(551, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
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
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 240);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Records found:";
			this.label1.Visible = false;
			// 
			// RecordsCountLabel
			// 
			this.RecordsCountLabel.AutoSize = true;
			this.RecordsCountLabel.ForeColor = System.Drawing.Color.Black;
			this.RecordsCountLabel.Location = new System.Drawing.Point(89, 240);
			this.RecordsCountLabel.Name = "RecordsCountLabel";
			this.RecordsCountLabel.Size = new System.Drawing.Size(13, 13);
			this.RecordsCountLabel.TabIndex = 4;
			this.RecordsCountLabel.Text = "0";
			this.RecordsCountLabel.Visible = false;
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
			this.Controls.Add(this.RecordsCountLabel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ExportButton);
			this.Controls.Add(this.FileLoggerListBox);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "Report Exporter";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SettingsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FileCloseMenuItem;
		private System.Windows.Forms.ListBox FileLoggerListBox;
		private System.Windows.Forms.Button ExportButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label RecordsCountLabel;
		private System.ComponentModel.BackgroundWorker InputFileWorker;
	}
}

