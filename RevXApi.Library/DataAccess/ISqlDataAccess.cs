using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXApi.Library.DataAccess
{
	public interface ISqlDataAccess
	{
		void CommitTransaction();
		void Dispose();
		string GetConnectionString(string name);
		List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
		List<T> LoadData<T>(string storedProcedure, string connectionStringName);
		List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters);
		void RollBackTransaction();
		Task SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
		Task SaveDataInTransaction<T>(string storedProcedure, T parameters);
		void StartTransaction(string connectionStringName);
	}
}