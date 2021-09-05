using RevXApi.Library.Models;
using System;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public class UserData : IUserData
	{
		private readonly ISqlDataAccess _sql;

		public UserData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public List<UserModel> GetUserById(string Id)
		{
			var output = _sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", new { Id }, "RevXData");

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
				System.Console.WriteLine(ex);
			}
		}
	}
}
