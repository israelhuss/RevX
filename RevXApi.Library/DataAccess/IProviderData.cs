using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IProviderData
	{
		void AddProvider(ProviderModel model);
		List<ProviderModel> GetEnabled(string userId);
		void EditProvider(ProviderModel model);
		List<ProviderModel> GetAll(string userId);
		ProviderModel GetById(int id, string userId);
	}
}