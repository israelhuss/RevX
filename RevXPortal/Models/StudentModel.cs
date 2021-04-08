using RevXPortal.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RevXPortal.Models
{
	public class StudentModel : ISelectionFriendly
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public string DisplayName => $"{FirstName} {LastName}";
	}
}
