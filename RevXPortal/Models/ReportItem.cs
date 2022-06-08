﻿namespace RevXPortal.Models
{
	public class ReportItem
	{
		public int Id { get; set; }
		public List<ReportItemDetail> ItemDetails { get; set; }
		public string Color { get; set; }
		public ReportItemView ViewAs { get; set; }
		public string Nickname { get; set; }
		public Dictionary<string, List<SessionModel>> Sessions { get; set; }
		public double TotalMoney { get; set; }
		public double TotalHours { get; set; }
	}
}