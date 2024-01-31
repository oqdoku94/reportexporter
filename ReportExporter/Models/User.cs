using Newtonsoft.Json;
using System;

namespace ReportExporter.Models
{
	internal class User
	{
		public long UserId { get; set; }
        public long Pen { get; set; }
        [JsonIgnore]
        public DateTime ExpDate { get; set; }
		[JsonProperty(nameof(ExpDate))]
		public string ExpDateString => ExpDate.ToString("dd/MM/yyyy");
		public string FirstName { get; set; }
        public string LastName { get; set; }   
        public string Phone { get; set; }
    }
}
