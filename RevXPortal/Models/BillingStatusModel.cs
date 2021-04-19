using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXPortal.Models
{
	public class BillingStatusModel : ISelectionFriendly
	{
		public int Id { get; set; }
		public string BillingStatus { get; set; }

		public string DisplayName => BillingStatus;
	}
}
