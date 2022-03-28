using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevXPortal.Models
{
	public class UserModel
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName {  get; set; }
		public string EmailAddress {  get; set; }
		public int BillingCycleId { get; set; }

	}
}
