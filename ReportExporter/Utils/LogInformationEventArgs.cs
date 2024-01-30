using System;

namespace ReportExporter.Utils
{
	internal class LogInformationEventArgs : System.EventArgs
	{
		public DateTime Issued { get; set; } = DateTime.Now;
		public string Message { get; set; }
	}
}
