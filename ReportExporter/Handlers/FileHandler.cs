using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.FileIO;
using ReportExporter.DAO;
using ReportExporter.Managers;
using ReportExporter.Model;
using ReportExporter.Models;
using ReportExporter.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ReportExporter.Handlers
{
	internal class FileHandler : IDisposable
	{
		private const string XML_FILE_EXTENSION = ".xml";
		private const string CSV_FILE_EXTENSION = ".csv";

		private readonly FileManager _xmlFileManager;
		private readonly FileManager _csvFileManager;
		private readonly FileScanner _xmlFileScanner;
		private readonly FileScanner _csvFileScanner;

		private readonly string _folderToScan;
		private bool _inWaitProcess = false;

		internal event EventHandler<LogInformationEventArgs> OnWriteSystemInformation;
		internal event EventHandler<FilePairFoundEventArgs> OnFilePairFound;

		internal FileHandler(string folderToScan)
		{
			_folderToScan = folderToScan;

			_xmlFileManager = new FileManager();
			_csvFileManager = new FileManager();

			_xmlFileScanner = new FileScanner(_xmlFileManager, this);
			_csvFileScanner = new FileScanner(_csvFileManager, this);

			RuntimeInitialize();
		}

		private void RuntimeInitialize()
		{
			var existsXmlFiles = GetFilesFromDirectory($"*{XML_FILE_EXTENSION}");
			_xmlFileManager.AddRange(existsXmlFiles);
			_xmlFileScanner.StartWatch(_folderToScan, XML_FILE_EXTENSION);

			var existsCsvFiles = GetFilesFromDirectory($"*{CSV_FILE_EXTENSION}");
			_csvFileManager.AddRange(existsCsvFiles);
			_csvFileScanner.StartWatch(_folderToScan, CSV_FILE_EXTENSION);
		}

		public void HandleExistsFiles()
		{
			_inWaitProcess = false;
			if (!_xmlFileManager.GetAllFiles().Any() || !_csvFileManager.GetAllFiles().Any())
				return;

			for (int i = 0; i < _xmlFileManager.GetAllFiles().Count(); i++)
				Handle(_xmlFileManager.GetAllFiles()[i]);
		}

		private IEnumerable<string> GetFilesFromDirectory(string searchPattern)
		{
			if (!Directory.Exists(_folderToScan))
				return Enumerable.Empty<string>();

			return Directory.GetFiles(_folderToScan, searchPattern, System.IO.SearchOption.AllDirectories);
		}

		internal void Handle(string filePath)
		{
			if (!_inWaitProcess)
			{
				OnWriteSystemInformation?.Invoke(this, new LogInformationEventArgs() { Message = $"Processing file: \"{filePath}\"..." });

				switch (Path.GetExtension(filePath).ToLower())
				{
					case XML_FILE_EXTENSION:
						HandleXml(filePath);
						break;
					case CSV_FILE_EXTENSION:
						break;
					default:
						OnWriteSystemInformation?.Invoke(this, new LogInformationEventArgs() { Message = $"No handler registered for file: \"{filePath}\"." });
						break;
				}

				OnWriteSystemInformation?.Invoke(this, new LogInformationEventArgs() { Message = $"File: \"{filePath}\" handeled." });
			}
		}

		private void HandleXml(string filePath)
		{
			if (!File.Exists(filePath))
				return;

			var xmlObj = DeserializeXmlFile(filePath);
			var xmlObjIds = xmlObj.CardList
				.Select(card => card.UserId)
				.Distinct();

			for(int i = 0; i < _csvFileManager.GetAllFiles().Count; i++)
			{
				var csvFile = _csvFileManager.GetAllFiles()[i];
				var csvObj = DeserializeCSVFile(csvFile);
				var csvObjIds = csvObj.Select(obj => obj.UserId)
					.Distinct();

				var intersectedIds = csvObjIds.Intersect(xmlObjIds);
				if (intersectedIds.Any())
				{
					var users = GetUser(xmlObj, csvObj, intersectedIds);

					OnFilePairFound?.Invoke(this, new FilePairFoundEventArgs() { Users = users });

					_csvFileManager.RemoveFile(csvFile);
					_xmlFileManager.RemoveFile(filePath);

					_inWaitProcess = true;
					break;
				}
			}
		}

		private IEnumerable<Models.User> GetUser(Cards xmlObj, IEnumerable<CSVUserData> csvObj, IEnumerable<string> ids)
		{
			List<Models.User> users = new List<Models.User>();

			foreach(var id in ids)
			{
				if (!long.TryParse(id, out var userId))
					continue;

				var currentXmlObj = xmlObj.CardList.FirstOrDefault(obj => obj.UserId == id);

				if (currentXmlObj == null)
					continue;

				var currentCsvObj = csvObj.FirstOrDefault(obj => obj.UserId == id);

				if (currentCsvObj == null)
					continue;

				if (!long.TryParse(currentXmlObj.Pan, out var pen))
					continue;

				if (!DateTime.TryParseExact(currentXmlObj.ExpDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var expDate))
					continue;

				var user = new Models.User()
				{
					UserId = userId,
					Pen = pen,
					ExpDate = expDate,
					FirstName = currentCsvObj.Name,
					LastName = currentCsvObj.SecondName,
					Phone = currentCsvObj.Number
				};

				users.Add(user);
			}

			return users;
		}

		private Cards DeserializeXmlFile(string filePath)
		{
			try
			{
				XmlSerializer ser = new XmlSerializer(typeof(Cards));

				using (Stream fs = File.Open(filePath, FileMode.Open))
				{
					return (Cards)ser.Deserialize(fs);
				}
			}
			catch
			{
				OnWriteSystemInformation?.Invoke(this, new LogInformationEventArgs() { Message = $"The file \"{filePath}\" contains an unknown data format." });
				return new Cards();
			}
		}

		private IEnumerable<CSVUserData> DeserializeCSVFile(string filePath)
		{
			try
			{
				List<CSVUserData> data = new List<CSVUserData>();
				using (TextFieldParser textFieldParser = new TextFieldParser(filePath))
				{
					textFieldParser.TextFieldType = FieldType.Delimited;
					textFieldParser.SetDelimiters(";");

					int rowNumber = 0;
					while (!textFieldParser.EndOfData)
					{
						string[] cols = textFieldParser.ReadFields();
						rowNumber++;

						if (rowNumber == 1 || cols.Length != 4)
							continue;

						data.Add(new CSVUserData 
						{ 
							UserId = cols[0], 
							Name = cols[1], 
							SecondName = cols[2], 
							Number = cols[3] 
						});
					}
				}

				return data;
			} 
			catch
			{
				OnWriteSystemInformation?.Invoke(this, new LogInformationEventArgs() { Message = $"The file \"{filePath}\" contains an unknown data format." });
				return Enumerable.Empty<CSVUserData>();
			}
		}

		public void Dispose()
		{
			_xmlFileScanner?.Dispose();
			_csvFileScanner?.Dispose();
		}
	}
}
