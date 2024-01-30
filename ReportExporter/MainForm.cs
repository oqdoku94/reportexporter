using Newtonsoft.Json;
using ReportExporter.Handlers;
using ReportExporter.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ReportExporter
{
	public partial class MainForm : Form, IDisposable
	{
		private FileSystemWatcher _watcher;
		private FileHandler _fileHandler;
		private IEnumerable<Models.User> _currentReportUsers;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			var pathToScanFolder = SettingsManager.Instance.Get(SettingsManager.FOLDER_SCAN_PATH_SETTING_KEY);

			if (string.IsNullOrEmpty(pathToScanFolder))
				return;

			_fileHandler = new FileHandler(pathToScanFolder);
			_fileHandler.OnFilePairFound += FileHandler_OnFilePairFound;
			_fileHandler.OnWriteSystemInformation += FileHandler_OnWriteSystemInformation;
			_fileHandler.HandleExistsFiles();
		}

		private void FileHandler_OnFilePairFound(object sender, Utils.FilePairFoundEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new Action(() => FileHandler_OnFilePairFound(sender, e)));
				return;
			}

			ExportButton.Enabled = true;
			RecordsCountLabel.Visible = true;
			RecordsCountLabel.Text = e.Users.Count().ToString();
			_currentReportUsers = e.Users;
		}

		private void FileHandler_OnWriteSystemInformation(object sender, Utils.LogInformationEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new Action(() => FileHandler_OnWriteSystemInformation(sender, e)));
				return;
			}

			FileLoggerListBox.TopIndex = FileLoggerListBox.Items.Count - 1;
			FileLoggerListBox.Items.Add($"[{e.Issued}] {e.Message}");
		}

		private void FileCloseMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void SettingsMenuItem_Click(object sender, EventArgs e)
		{
			using (SettingsForm settingsForm = new SettingsForm())
			{
				settingsForm.ShowDialog();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				components?.Dispose();
				_watcher?.Dispose();
				_watcher = null;
			}
			base.Dispose(disposing);
		}

		private void ExportButton_Click(object sender, EventArgs e)
		{
			ExportButton.Enabled = false;
			RecordsCountLabel.Visible = false;
			RecordsCountLabel.Text = "0";
			File.WriteAllText("1.json", JsonConvert.SerializeObject(_currentReportUsers));

			_fileHandler.HandleExistsFiles();
		}
	}
}
