using RevXApi.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace RevXApi.Library.DataAccess
{
	public class ProviderData : IProviderData
	{
		private readonly ISqlDataAccess _sql;

		public ProviderData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public List<ProviderModel> GetAll(string userId)
		{
			return _sql.LoadData<ProviderModel, dynamic>("dbo.spProvider_GetAll", new { userId }, "RevXData");
		}
		public List<ProviderModel> GetEnabled(string userId)
		{
			return _sql.LoadData<ProviderModel, dynamic>("dbo.spProvider_GetEnabled", new { userId }, "RevXData");
		}

		public ProviderModel GetById(int id, string userId)
		{
			return _sql.LoadData<ProviderModel, dynamic>("dbo.spProvider_GetById", new { Id = id, userId }, "RevXData").FirstOrDefault();
		}

		public void AddProvider(ProviderModel model)
		{
			_sql.SaveData("dbo.spProvider_Insert", model, "RevXData");
		}

		public void EditProvider(ProviderModel model)
		{
			_sql.SaveData("dbo.spProvider_Update", model, "RevXData");
		}
	}
}
