using RevXPortal.Models;
using System.Threading.Tasks;

namespace RevXPortal.Authentication
{
	public interface IAuthenticationService
	{
		Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication);
		Task Logout();
	}
}