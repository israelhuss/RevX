using System.Collections.Generic;

namespace RevXPortal.Models
{
	public interface IEditTable
	{
		public List<string> ColNames { get; set; }
		public List<IEditTableRow> TableRows { get; set; }

	}

	public interface IEditTableObject
	{
		public int Id { get; set; }
		public int RowId { get; set; }
		public int Value { get; set; }
	}

	public interface IEditTableRow
	{
		public int Id { get; set; }
		public List<string> RowObjects { get; }
	}
}
