using RevXPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public interface ISessionEndpoint
	{
		Task<List<ManageSessionModel>> GetAll();
		Task SaveSession(ManageSessionModel model);
		Task EditSession(ManageSessionModel model);
		Task DeleteSession(int id);
		Task<List<SessionModel>> GetAllSessions();
	}
}