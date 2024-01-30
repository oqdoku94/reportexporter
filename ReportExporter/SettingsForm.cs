using ReportExporter.Managers;
using System;
using System.Windows.Forms;

namespace ReportExporter
{
	public partial class SettingsForm : Form
	{
		private string _folderPathSettingCurrentValue;

		public SettingsForm()
		{
			InitializeComponent();
			_folderPathSettingCurrentValue = SettingsManager.Instance.Get(SettingsManager.FOLDER_SCAN_PATH_SETTING_KEY);
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			folderPathTextBox.Text = _folderPathSettingCurrentValue;
			folderPathTextBox.TextChanged += FolderPathTextBox_TextChanged;
		}

		private void FolderPathTextBox_TextChanged(object sender, EventArgs e)
		{
			if (folderPathTextBox.Text != _folderPathSettingCurrentValue)
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
				if (result == DialogResult.OK) // Test result.
				{
					folderPathTextBox.Text = folderDialog.SelectedPath;
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
				SettingsManager.Instance.Save(SettingsManager.FOLDER_SCAN_PATH_SETTING_KEY, folderPathTextBox.Text);
				_folderPathSettingCurrentValue = folderPathTextBox.Text;
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
