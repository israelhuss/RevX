using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IUserData
	{
		void CreateUser(UserModel user);
		List<UserModel> GetUserById(string Id);
	}
}