using ReportExporter.Handlers;
using ReportExporter.Managers;
using System;
using System.IO;

namespace ReportExporter.Utils
{
	internal class FileScanner : IDisposable
	{
		private readonly FileManager _fileManager;
		private readonly FileHandler _fileHandler;

		private FileSystemWatcher _watcher;

		internal FileScanner(FileManager fileManager, FileHandler handler)
		{
			_fileManager = fileManager;
			_fileHandler = handler;
		}

		internal void StartWatch(string folderPath, string fileExtension)
		{
			_watcher = new FileSystemWatcher(folderPath, "*" + fileExtension);
			_watcher.Created += OnCreated;
			_watcher.Deleted += OnDeleted;
			_watcher.Renamed += OnRenamed;

			_watcher.EnableRaisingEvents = true;
		}

		private void OnRenamed(object sender, RenamedEventArgs e)
		{
			_fileManager.RemoveFile(e.OldFullPath);
			_fileManager.AddFile(e.FullPath);
		}

		private void OnDeleted(object sender, FileSystemEventArgs e)
		{
			_fileManager.RemoveFile(e.FullPath);
		}

		private void OnCreated(object sender, FileSystemEventArgs e)
		{
			_fileManager.AddFile(e.FullPath);
			_fileHandler.Handle(e.FullPath);
		}

		public void Dispose()
		{
			_watcher?.Dispose();
		}
	}
}
