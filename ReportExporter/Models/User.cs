using System;

namespace ReportExporter.Models
{
	internal class User
	{
		public long UserId { get; set; }
        public long Pen { get; set; }
        public DateTime ExpDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }   
        public string Phone { get; set; }
    }
}
