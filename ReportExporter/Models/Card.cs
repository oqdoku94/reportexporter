using System.Xml.Serialization;

namespace ReportExporter.Models
{
	[XmlRoot("Card")]
	public class Card
	{
		[XmlAttribute]
		public string UserId { get; set; }
		[XmlElement]
		public string Pan {  get; set; }
		[XmlElement]
		public string ExpDate { get; set; }
	}
}
