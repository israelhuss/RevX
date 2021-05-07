using RevXPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public interface IBillingStatusEndpoint
	{
		List<BillingStatusModel> BillingStatuses { get; set; }
		Task AddBillingStatus(BillingStatusModel model);
		Task<List<BillingStatusModel>> GetAll();
	}
}