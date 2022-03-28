using RevXPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public interface IClientEndpoint
	{
		Task Add(ClientModel model);
		Task<List<ClientModel>> GetAll();
		Task<List<ClientModel>> GetEnabled();
		Task Edit(ClientModel model);
	}
}