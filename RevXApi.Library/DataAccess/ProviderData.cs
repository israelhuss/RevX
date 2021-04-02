using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Library.DataAccess
{
	public class ProviderData : IProviderData
	{
		private readonly ISqlDataAccess _sql;

		public ProviderData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public List<ProviderModel> GetAll()
		{
			return _sql.LoadData<ProviderModel>("dbo.spProvider_GetAll", "RevXData");
		}

		public ProviderModel GetById(int id)
		{
			return _sql.LoadData<ProviderModel, dynamic>("dbo.spProvider_GetById", new { Id = id }, "RevXData").FirstOrDefault();
		}

		public void AddProvider(ProviderModel model)
		{
			_sql.SaveData("dbo.spProvider_Insert", model, "RevXData");
		}
	}
}
