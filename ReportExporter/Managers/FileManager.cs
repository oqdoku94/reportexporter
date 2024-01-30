using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ReportExporter.Managers
{
	internal class FileManager
	{
		private readonly List<string> _files;

		public FileManager()
		{
			_files = new List<string>();
		}

		public void AddRange(IEnumerable<string> filePaths)
		{
			foreach (string path in filePaths)
				AddFile(path);
		}

		public void AddFile(string filePath)
		{
			if (!_files.Contains(filePath))
				_files.Add(filePath);
		}

		public bool RemoveFile(string filePath) 
		{
			var result = _files.Remove(filePath);

			return result;
		}

		public ReadOnlyCollection<string> GetAllFiles()
		{
			return _files.AsReadOnly();
		}
	}
}
