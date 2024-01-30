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

		public MainForm()
		{
			InitializeComponent();
			InitializeScanProcess();
		}

		public void InitializeScanProcess()
		{
			var pathToScanFolder = SettingsManager.Instance.Get(SettingsManager.FOLDER_SCAN_PATH_SETTING_KEY);
			if (!string.IsNullOrEmpty(pathToScanFolder) && Directory.Exists(pathToScanFolder))
			{
				_fileHandler = new FileHandler(pathToScanFolder);

				if (InputFileWorker.IsBusy)
					InputFileWorker.CancelAsync();

				InputFileWorker.DoWork -= InputFileWorker_DoWork;
				InputFileWorker.DoWork += InputFileWorker_DoWork;
				InputFileWorker.RunWorkerCompleted -= InputFileWorker_RunWorkerCompleted;
				InputFileWorker.RunWorkerCompleted += InputFileWorker_RunWorkerCompleted;
				InputFileWorker.RunWorkerAsync();
			}
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
				var result = _fileHandler.HandleNext(out var filePath);

				if (!string.IsNullOrEmpty(filePath))
				{
					var matches = result != null ? result.Item1 : 0;
					Log($"File: \"{filePath}\" handeled.");
					Log($"Found \"{matches}\" matches.");
				}

				if (result != null)
				{
					e.Result = result;
					break;
				}
			}
		}

		private void Log(string message)
		{
			if (this.InvokeRequired && !this.IsDisposed)
			{
				this.Invoke(new Action(() => Log(message)));
				return;
			}

			FileLoggerListBox.Items.Add($"[{DateTime.Now}] " + message);
			FileLoggerListBox.TopIndex = FileLoggerListBox.Items.Count - 1;
		}

		//private void FileHandler_OnFilePairFound(object sender, Utils.FilePairFoundEventArgs e)
		//{


		//	ExportButton.Enabled = true;
		//	RecordsCountLabel.Visible = true;
		//	RecordsCountLabel.Text = e.Users.Count().ToString();
		//	_currentReportUsers = e.Users;
		//}

		//private void FileHandler_OnWriteSystemInformation(object sender, Utils.LogInformationEventArgs e)
		//{
		//	if (this.InvokeRequired)
		//	{
		//		this.Invoke(new Action(() => FileHandler_OnWriteSystemInformation(sender, e)));
		//		return;
		//	}

		//	FileLoggerListBox.TopIndex = FileLoggerListBox.Items.Count - 1;
		//	FileLoggerListBox.Items.Add($"[{e.Issued}] {e.Message}");
		//}

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

		private void ExportButton_Click(object sender, EventArgs e)
		{
			File.WriteAllText($"1.json", _currentJsonReport);
			ExportButton.Enabled = false;
			SetVisibleRecordCountLabel(false);
			RecordsCountLabel.Text = "0";
			InputFileWorker.RunWorkerAsync();
		}
	}
}
