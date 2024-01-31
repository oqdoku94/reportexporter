using ReportExporter.Handlers;
using ReportExporter.Managers;
using System;
using System.IO;
using System.Windows.Forms;

namespace ReportExporter
{
	public partial class MainForm : Form, IDisposable
	{
		private FileHandler _fileHandler;
		private string _currentJsonReport;
		private string _pathToScan;
		private string _pathToReport;

		public MainForm()
		{
			InitializeComponent();
			_pathToScan = SettingsManager.Instance.Get(SettingsManager.FOLDER_SCAN_PATH_SETTING_KEY);
			_pathToReport = SettingsManager.Instance.Get(SettingsManager.FOLDER_SAVE_PATH_SETTING_KEY);
		}

		private void InputFileWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (e.Result is Tuple<int, string> result)
			{
				SetVisibleRecordCountLabel(true);
				ExportButton.Enabled = true;
				RecordsCountLabel.Text = result.Item1.ToString();
				_currentJsonReport = result.Item2;
			}
		}

		private void SetVisibleRecordCountLabel(bool isVisible)
		{
			label1.Visible = isVisible;
			RecordsCountLabel.Visible = isVisible;
		}

		private void InputFileWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			_currentJsonReport = null;
			while (!InputFileWorker.CancellationPending)
			{
				if (!Directory.Exists(_pathToScan))
					continue;

				if (_fileHandler == null)
					continue;

				var result = _fileHandler.HandleNext(out var filePath);

				if (!string.IsNullOrEmpty(filePath))
				{
					var matches = result != null ? result.Item1 : 0;
					PrintLog($"File: \"{filePath}\" handeled.");
					PrintLog($"Found \"{matches}\" matches.");
				}

				if (result != null)
				{
					e.Result = result;
					break;
				}
			}
		}

		private void PrintLog(string message)
		{
			if (this.InvokeRequired && !this.IsDisposed)
			{
				this.Invoke(new Action(() => PrintLog(message)));
				return;
			}

			FileLoggerListBox.Items.Add($"[{DateTime.Now}] " + message);
			FileLoggerListBox.TopIndex = FileLoggerListBox.Items.Count - 1;
		}

		private void FileCloseMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void SettingsMenuItem_Click(object sender, EventArgs e)
		{
			ShowSettings();
		}

		private void ExportButton_Click(object sender, EventArgs e)
		{
			if (!CheckReportPathExists())
				return;

			var pathToFile = Path.Combine(_pathToReport, DateTime.Now.ToString("ddMMyyyyhhmmssFF") + ".json");
			File.WriteAllText(pathToFile, _currentJsonReport);
			ExportButton.Enabled = false;
			SetVisibleRecordCountLabel(false);
			RecordsCountLabel.Text = "0";
			InputFileWorker.RunWorkerAsync();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			UpdateFileHandler();
			InputFileWorker.RunWorkerAsync();
		}

		public void UpdateFileHandler()
		{
			if (!string.IsNullOrEmpty(_pathToScan))
			{
				if (!CheckPathToScanExists())
					return;

				_fileHandler = new FileHandler(_pathToScan);
			}
		}

		public bool CheckPathToScanExists()
		{
			if (!Directory.Exists(_pathToScan))
			{
				SettingsManager.Instance.Save(SettingsManager.FOLDER_SCAN_PATH_SETTING_KEY, null);
				MessageBox.Show("Directory for scanning report files does't exist!\nPlease set the correct path to the folder.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				ShowSettings();
				return false;
			}

			return true;
		}

		public bool CheckReportPathExists()
		{
			if (string.IsNullOrEmpty(_pathToReport))
			{
				MessageBox.Show("The folder to save the report is not specified.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			if (!Directory.Exists(_pathToReport))
			{
				try
				{
					Directory.CreateDirectory(_pathToReport);
				}
				catch
				{
					SettingsManager.Instance.Save(SettingsManager.FOLDER_SAVE_PATH_SETTING_KEY, null);
					MessageBox.Show("Directory for export report file does't exist!\nPlease set the correct path to the folder.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					ShowSettings();
					return false;
				}
			}

			return true;
		}

		private void ShowSettings()
		{
			using (SettingsForm settingsForm = new SettingsForm())
			{
				settingsForm.ShowDialog();

				_pathToReport = SettingsManager.Instance.Get(SettingsManager.FOLDER_SAVE_PATH_SETTING_KEY);
				var newPathToScan = SettingsManager.Instance.Get(SettingsManager.FOLDER_SCAN_PATH_SETTING_KEY);

				if (newPathToScan != _pathToScan)
				{
					_pathToScan = newPathToScan;
					UpdateFileHandler();
				}
			}
		}
	}
}
