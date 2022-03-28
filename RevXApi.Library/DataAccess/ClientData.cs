using RevXApi.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace RevXApi.Library.DataAccess
{
	public class ClientData : IClientData
	{
		private readonly ISqlDataAccess _sql;

		public ClientData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public List<ClientModel> GetAll(string userId)
		{
			return _sql.LoadData<ClientModel, dynamic>("dbo.spClient_GetAll", new { userId }, "RevXData");
		}

		public List<ClientModel> GetEnabled(string userId)
		{
			return _sql.LoadData<ClientModel, dynamic>("dbo.spClient_GetEnabled", new { userId }, "RevXData");
		}

		public ClientModel GetById(int id, string userId)
		{
			return _sql.LoadData<ClientModel, dynamic>("dbo.spClient_GetById", new { Id = id, userId }, "RevXData").FirstOrDefault();

		}

		public void AddClient(ClientModel model)
		{
			_sql.SaveData("dbo.spClient_Insert", model, "RevXData");
		}

		public void EditClient(ClientModel model)
		{
			_sql.SaveData("dbo.spClient_Update", model, "RevXData");
		}
	}
}
