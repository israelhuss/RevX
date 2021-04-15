﻿using System.Collections.Generic;

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
		int SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
		int SaveDataInTransaction<T>(string storedProcedure, T parameters);
		void StartTransaction(string connectionStringName);
	}
}