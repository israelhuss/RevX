using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IProviderData
	{
		void AddProvider(ProviderModel model);
		List<ProviderModel> GetAll();
		ProviderModel GetById(int id);
	}
}