using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IClientData
	{
		List<ClientModel> GetAll(string userId);
		List<ClientModel> GetEnabled(string userId);
		ClientModel GetById(int id, string userId);
		void AddClient(ClientModel model);
		void EditClient(ClientModel model);
	}
}