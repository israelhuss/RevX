using RevXPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public interface ISessionEndpoint
	{
		Task<List<DisplaySessionModel>> GetAll();
		Task SaveSession(ManageSessionModel model);
	}
}