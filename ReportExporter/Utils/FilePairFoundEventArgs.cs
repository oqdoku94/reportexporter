using ReportExporter.Models;
using System;
using System.Collections.Generic;

namespace ReportExporter.Utils
{
	internal class FilePairFoundEventArgs : EventArgs
	{
		public IEnumerable<User> Users { get; set; }
	}
}
