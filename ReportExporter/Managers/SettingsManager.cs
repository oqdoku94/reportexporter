using System.Configuration;
using System.Linq;

namespace ReportExporter.Managers
{
	internal class SettingsManager
	{
		public const string FOLDER_SCAN_PATH_SETTING_KEY = "FOLDER_PATH";

		private readonly Configuration _configuration;

		private static SettingsManager _instance;

		internal static SettingsManager Instance
		{
			get
			{
				if (_instance == null)
					_instance = new SettingsManager();
				
				return _instance;
			}
		}

		private SettingsManager() 
		{
			_configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
		}

		internal void Save(string key, string value)
		{
			if (_configuration.AppSettings.Settings.AllKeys.Contains(key))
				_configuration.AppSettings.Settings.Remove(key);

			if (!string.IsNullOrEmpty(value))
				_configuration.AppSettings.Settings.Add(key, value);

			_configuration.Save();
		}

		internal string Get(string key)
		{
			if (_configuration.AppSettings.Settings.AllKeys.Contains(key))
				return _configuration.AppSettings.Settings[key].Value;

			return string.Empty;
		}
	}
}
