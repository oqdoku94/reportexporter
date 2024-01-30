using ReportExporter.Model;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ReportExporter.Models
{
	[XmlRoot("Cards")]
	public class Cards
	{
		[XmlElement("Card")]
		public List<Card> CardList { get; set; } = new List<Card>();
	}
}
