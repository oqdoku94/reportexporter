using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ReportExporter.Utils
{
	internal static class Serializer
	{
		private const string CSV_DELIMITER = ";";

		internal static T DeserializeXml<T>(string filePath)
		{
			XmlSerializer ser = new XmlSerializer(typeof(T));

			using (Stream fs = File.Open(filePath, FileMode.Open))
			{
				return (T)ser.Deserialize(fs);
			}
		}

		internal static List<string[]> DeserializeCSV(string filePath)
		{
			List<string[]> data = new List<string[]>();
			using (TextFieldParser textFieldParser = new TextFieldParser(filePath))
			{
				textFieldParser.TextFieldType = FieldType.Delimited;
				textFieldParser.SetDelimiters(";");

				while (!textFieldParser.EndOfData)
				{
					string[] cols = textFieldParser.ReadFields();
					data.Add(cols);
				}
			}

			return data;
		}
	}
}
