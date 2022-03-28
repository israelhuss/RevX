using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Library.DataAccess
{
	public class WorkplaceData : IWorkplaceData
	{
		private readonly ISqlDataAccess _sql;

		public WorkplaceData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public WorkplaceModel GetCurrentUserInfo(string userId)
		{
			return _sql.LoadData<WorkplaceModel, dynamic>("dbo.spWorkplace_GetByUserId", new { userId }, "RevXData").FirstOrDefault();
		}
	}
}
