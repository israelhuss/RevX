using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXPortal.Models
{
	public class ProviderModel : ISelectionFriendly
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public string DisplayName => Name;
	}
}
