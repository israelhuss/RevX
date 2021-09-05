using RevXPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public interface IBillingStatusEndpoint
	{
		List<BillingStatusModel> BillingStatuses { get; set; }
		Task<List<BillingStatusModel>> GetEnabled();
		Task AddBillingStatus(BillingStatusModel model);
		Task<List<BillingStatusModel>> GetAll();
		Task EditBillingStatus(BillingStatusModel model);
	}
}