using Microsoft.Extensions.Logging;
using RevXApi.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace RevXApi.Library.DataAccess
{
	public class BillingStatusData : IBillingStatusData
	{
		private readonly ISqlDataAccess _sql;
        private readonly ILogger _logger;

        public BillingStatusData(ISqlDataAccess sql, ILogger<BillingStatusData> logger)
		{
			_sql = sql;
            _logger = logger;
        }

		public List<BillingStatusModel> GetAll()
		{
			List<BillingStatusModel> result = _sql.LoadData<BillingStatusModel>("dbo.spBillingStatus_GetAll", "RevXData");
			return result;
		}

		public List<BillingStatusModel> GetEnabled()
		{
			return _sql.LoadData<BillingStatusModel>("dbo.spBillingStatus_GetEnabled", "RevXData");
		}

		public BillingStatusModel GetById(int id)
		{
			return _sql.LoadData<BillingStatusModel, dynamic>("dbo.spBillingStatus_GetById", new { Id = id }, "RevXData").FirstOrDefault();
		}

		public void AddBillingStatus(BillingStatusModel model)
		{
			_sql.SaveData("dbo.spBillingStatus_Insert", model, "RevXData");
		}

		public void EditBillingStatus(BillingStatusModel model)
		{
			_sql.SaveData("dbo.spBillingStatus_Update", model, "RevXData");
		}
	}
}
