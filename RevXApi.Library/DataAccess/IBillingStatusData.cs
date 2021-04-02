using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IBillingStatusData
	{
		void AddBillingStatus(BillingStatusModel model);
		List<BillingStatusModel> GetAll();
		BillingStatusModel GetById(int id);
	}
}