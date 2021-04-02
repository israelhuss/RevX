using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Library.DataAccess
{
	public class BillingStatusData : IBillingStatusData
	{
		private readonly ISqlDataAccess _sql;

		public BillingStatusData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public List<BillingStatusModel> GetAll()
		{
			return _sql.LoadData<BillingStatusModel>("dbo.spBillingStatus_GetAll", "RevXData");
		}

		public BillingStatusModel GetById(int id)
		{
			return _sql.LoadData<BillingStatusModel, dynamic>("dbo.spBillingStatus_GetById", new { Id = id }, "RevXData").FirstOrDefault();
		}

		public void AddBillingStatus(BillingStatusModel model)
		{
			_sql.SaveData("dbo.spBillingStatus_Insert", model, "RevXData");
		}
	}
}
