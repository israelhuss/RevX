using RevXPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public interface IUserEndpoint
	{
		Task<UserModel> GetCurrentUser();
		Task<string> GetCurrentUserId();
	}
}