using Microsoft.Extensions.Configuration.UserSecrets;
using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RevXApi.Library.DataAccess
{
	public class UserData : IUserData
	{
		private readonly ISqlDataAccess _sql;

		public UserData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public UserModel GetUserById(string Id)
		{
			var output = _sql.LoadData<UserModel, dynamic>("dbo.spUser_GetById", new { Id }, "RevXData").FirstOrDefault();
			return output;
		}

		public void CreateUser(UserModel user)
		{
			try
			{

				_sql.SaveData("dbo.spUser_Insert", new { user.Id, user.FirstName, user.LastName, user.EmailAddress }, "RevXData");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}
