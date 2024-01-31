using System.Collections.Generic;

namespace ReportExporter.Models
{
	public class Report<T>
	{
		public List<T> Records { get; set; }
	}
}
