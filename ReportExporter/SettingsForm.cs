using ReportExporter.Managers;
using System;
using System.Windows.Forms;

namespace ReportExporter
{
	public partial class SettingsForm : Form
	{
		private string _folderPathSettingCurrentValue;
		private string _reportPathSettingCurrentValue;

		public SettingsForm()
		{
			InitializeComponent();
			_folderPathSettingCurrentValue = SettingsManager.Instance.Get(SettingsManager.FOLDER_SCAN_PATH_SETTING_KEY);
			_reportPathSettingCurrentValue = SettingsManager.Instance.Get(SettingsManager.FOLDER_SAVE_PATH_SETTING_KEY);
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			PathToScanTextBox.Text = _folderPathSettingCurrentValue;
			PathToScanTextBox.TextChanged += InputTextChanged;
			PathToSaveTextBox.Text = _reportPathSettingCurrentValue;
			PathToSaveTextBox.TextChanged += InputTextChanged;
		}

		private void InputTextChanged(object sender, EventArgs e)
		{
			if (PathToScanTextBox.Text != _folderPathSettingCurrentValue || PathToSaveTextBox.Text != _reportPathSettingCurrentValue)
			{
				SaveButton.Enabled = true;
				return;
			}

			SaveButton.Enabled = false;
		}

		private void selectPathButton_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
			{
				DialogResult result = folderDialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					if (sender == SelectPathToScanButton)
					{
						PathToScanTextBox.Text = folderDialog.SelectedPath;
						return;
					}

					PathToSaveTextBox.Text = folderDialog.SelectedPath;
				}
			}
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			try
			{
				SettingsManager.Instance.Save(SettingsManager.FOLDER_SCAN_PATH_SETTING_KEY, PathToScanTextBox.Text);
				_folderPathSettingCurrentValue = PathToScanTextBox.Text;
				SettingsManager.Instance.Save(SettingsManager.FOLDER_SAVE_PATH_SETTING_KEY, PathToSaveTextBox.Text);
				_reportPathSettingCurrentValue = PathToSaveTextBox.Text;
				SaveButton.Enabled = false;
				Close();
			} 
			catch
			{
				MessageBox.Show("An error occurred while saving the settings file.", "Error");
			}
		}
	}
}
