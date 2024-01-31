using Newtonsoft.Json;
using ReportExporter.Models;
using ReportExporter.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReportExporter.Handlers
{
	internal class FileHandler
	{
		private const string XML_FILE_EXTENSION = ".xml";
		private const string CSV_FILE_EXTENSION = ".csv";

		private readonly ConcurrentDictionary<string, InputFile> _inputFiles = new ConcurrentDictionary<string, InputFile>();
		private readonly string _folderToScan;

		private FileSystemWatcher _watcher;

		internal FileHandler(string folderToScan)
		{
			_folderToScan = folderToScan;
			RuntimeInitialize();
		}

		private void RuntimeInitialize()
		{
			AddInputFiles();
			AddFileSystemWatcher();
		}

		private void AddInputFiles()
		{
			if (!Directory.Exists(_folderToScan))
				return;

			foreach (var file in Directory.GetFiles(_folderToScan, "*", SearchOption.TopDirectoryOnly))
				AddFile(file);
		}

		private void AddFile(string filePath)
		{
			if (!File.Exists(filePath))
				return;

			var currentFileExtension = Path.GetExtension(filePath).ToLower();

			if (currentFileExtension != XML_FILE_EXTENSION && currentFileExtension != CSV_FILE_EXTENSION)
				return;

			var existsInputFile = _inputFiles.Values.FirstOrDefault(file => file.Path == filePath);
			if (existsInputFile != null)
			{
				existsInputFile.State = InputFileStateEnum.Created;
				return;
			}

			var inputFile = new InputFile()
			{

				Extension = currentFileExtension,
				Path = filePath,
				State = InputFileStateEnum.Created,
			};
			_inputFiles.TryAdd(filePath, inputFile);
		}

		private void AddFileSystemWatcher()
		{
			if (!Directory.Exists(_folderToScan))
				return;

			_watcher = new FileSystemWatcher(_folderToScan);
			_watcher.Created += OnCreated;
			_watcher.Deleted += OnDeleted;
			_watcher.Renamed += OnRenamed; ;

			_watcher.EnableRaisingEvents = true;
		}

		private void OnRenamed(object sender, RenamedEventArgs e)
		{
			var currentInputFile = _inputFiles.Values.FirstOrDefault(inputFile => inputFile.Path == e.OldFullPath);
			if (currentInputFile == null)
				return;

			currentInputFile.State = InputFileStateEnum.Deleted;
			AddFile(e.FullPath);
		}

		private void OnChanged(object sender, FileSystemEventArgs e)
		{
			if (e.ChangeType != WatcherChangeTypes.Changed)
				return;

			var currentInputFile = _inputFiles.Values.FirstOrDefault(inputFile => inputFile.Path == e.FullPath);

			if (currentInputFile == null)
				return;

			if (currentInputFile.State == InputFileStateEnum.Failed)
				currentInputFile.State = InputFileStateEnum.Changed;
		}

		private void OnDeleted(object sender, FileSystemEventArgs e)
		{
			var currentInputFile = _inputFiles.Values.FirstOrDefault(inputFile => inputFile.Path == e.FullPath);

			if (currentInputFile == null)
				return;

			currentInputFile.State = InputFileStateEnum.Deleted;
		}

		private void OnCreated(object sender, FileSystemEventArgs e)
		{
			AddFile(e.FullPath);
		}

		internal Tuple<int, string> HandleNext(out string filePath)
		{
			var file = _inputFiles.Values.FirstOrDefault(inputFile => inputFile.State == InputFileStateEnum.Created || inputFile.State == InputFileStateEnum.Changed);
			filePath = null;

			if (file == null)
				return null;

			filePath = file.Path;
			var resultObj = HandleFile(file);

			if (resultObj.Any())
			{
				var report = new Report<User> { Records = resultObj };
				return new Tuple<int, string>(report.Records.Count(), JsonConvert.SerializeObject(report));
			}

			return null;
		}

		private List<Models.User> HandleFile(InputFile file)
		{
			List<Models.User> users = new List<Models.User>();

			try
			{
				file.State = InputFileStateEnum.Checked;
				switch (file.Extension)
				{
					case XML_FILE_EXTENSION:
						users = HandleXml(file);
						break;
					case CSV_FILE_EXTENSION:
						users = HandleCsv(file);
						break;
				}
			}
			catch
			{
				file.State = InputFileStateEnum.Failed;
				return new List<Models.User>();
			}

			return users;
		}

		private List<Models.User> HandleXml(InputFile file)
		{
			if (!File.Exists(file.Path))
				return new List<Models.User>();

			var xmlObj = Serializer.DeserializeXml<Cards>(file.Path);
			var cards = xmlObj.CardList.ToDictionary(keyValue => keyValue.UserId);

			var csvFiles = _inputFiles.Values.Where(inputFile => inputFile.Extension == CSV_FILE_EXTENSION
										&& inputFile.State != InputFileStateEnum.Loaded
										&& inputFile.State != InputFileStateEnum.Deleted
										&& inputFile.State != InputFileStateEnum.Failed);

			List<Models.User> users = new List<Models.User>();
			foreach (var csvFile in csvFiles)
			{
				try
				{
					if (!File.Exists(csvFile.Path))
						continue;

					var csvObjs = Serializer.DeserializeCSV(csvFile.Path);
					var intersectionCsvList = csvObjs
						.Where(obj => obj.Length == 4 && cards.ContainsKey(obj[0]));

					if (intersectionCsvList.Any())
					{
						foreach (var csvObj in intersectionCsvList)
						{
							string userId = csvObj[0];

							if (!long.TryParse(userId, out var longUserId))
								continue;

							if (!long.TryParse(cards[userId].Pan, out var pen))
								continue;

							if (!DateTime.TryParseExact(cards[userId].ExpDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var expDate))
								continue;

							var user = new Models.User()
							{
								UserId = longUserId,
								Pen = pen,
								ExpDate = expDate,
								FirstName = csvObj[1],
								LastName = csvObj[2],
								Phone = csvObj[3]
							};

							users.Add(user);
						}

						file.State = InputFileStateEnum.Loaded;
						csvFile.State = InputFileStateEnum.Loaded;
						return users;
					}
				}
				catch
				{
					// IGNORE
				}
			}

			return users;
		}

		private List<Models.User> HandleCsv(InputFile file)
		{
			if (!File.Exists(file.Path))
				return new List<Models.User>();

			var csvObjs = Serializer.DeserializeCSV(file.Path);
			var scvUserData = csvObjs.ToDictionary(obj => obj[0]);

			var xmlFiles = _inputFiles.Values.Where(inputFile => inputFile.Extension == XML_FILE_EXTENSION
										&& inputFile.State != InputFileStateEnum.Loaded
										&& inputFile.State != InputFileStateEnum.Deleted);

			List<Models.User> users = new List<Models.User>();
			foreach (var xmlFile in xmlFiles)
			{
				try
				{
					if (!File.Exists(xmlFile.Path))
						continue;

					var xmlObjs = Serializer.DeserializeXml<Cards>(xmlFile.Path);
					var intersectionXmlList = xmlObjs.CardList.Where(card => scvUserData.ContainsKey(card.UserId));

					if (intersectionXmlList.Any())
					{
						foreach (var xmlObj in intersectionXmlList)
						{
							if (!long.TryParse(xmlObj.UserId, out var longUserId))
								continue;

							if (!long.TryParse(xmlObj.Pan, out var pen))
								continue;

							if (!DateTime.TryParseExact(xmlObj.ExpDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var expDate))
								continue;

							var user = new Models.User()
							{
								UserId = longUserId,
								Pen = pen,
								ExpDate = expDate,
								FirstName = scvUserData[xmlObj.UserId][1],
								LastName = scvUserData[xmlObj.UserId][2],
								Phone = scvUserData[xmlObj.UserId][3]
							};

							users.Add(user);
						}

						file.State = InputFileStateEnum.Loaded;
						xmlFile.State = InputFileStateEnum.Loaded;
						return users;
					}
				}
				catch
				{
					// IGNORE
				}
			}

			return users;
		}
	}
}
