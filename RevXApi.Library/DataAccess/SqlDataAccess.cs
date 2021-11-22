using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RevXApi.Library.DataAccess
{
	public class SqlDataAccess : IDisposable, ISqlDataAccess
	{
		private readonly IConfiguration _config;
		private readonly ILogger<SqlDataAccess> _logger;

		public SqlDataAccess(IConfiguration config, ILogger<SqlDataAccess> logger)
		{
			_config = config;
			_logger = logger;
		}

		public string GetConnectionString(string name)
		{
			return _config.GetConnectionString(name);
		}

		public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				List<T> rows = connection.Query<T>(storedProcedure, parameters,
					commandType: CommandType.StoredProcedure).ToList();

				return rows;
			}
		}

		public List<T> LoadData<T>(string storedProcedure, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				List<T> rows = connection.Query<T>(storedProcedure,
					commandType: CommandType.StoredProcedure).ToList();

				return rows;
			}
		}

		public List<T> Query<T>(string sql, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				List<T> rows = connection.Query<T>(sql,
					commandType: CommandType.Text).ToList();

				return rows;
			}
		}

		public int SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				return connection.Execute(storedProcedure, parameters,
					commandType: CommandType.StoredProcedure);
			}
		}


		public List<T> SaveDataWithResult<T, U>(string storedProcedure, U parameters, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				List<T> rows = connection.Query<T>(storedProcedure, parameters,
					commandType: CommandType.StoredProcedure).ToList();
				return rows;
			}
		}


		private IDbConnection _connection;
		private IDbTransaction _transaction;

		public void StartTransaction(string connectionStringName)
		{
			_connection = new SqlConnection(GetConnectionString(connectionStringName));
			_connection.Open();

			_transaction = _connection.BeginTransaction();
			IsClosed = false;
		}

		public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
		{
			List<T> rows = _connection.Query<T>(storedProcedure, parameters,
				commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();

			return rows;
		}

		public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
		{
			_connection.Execute(storedProcedure, parameters,
				commandType: CommandType.StoredProcedure, transaction: _transaction);
		}

		public int ExecuteCommandInTransaction<T>(string sql)
		{
			var affected = _connection.Execute(sql, commandType: CommandType.Text, transaction: _transaction);
			return affected;
		}

		public List<T> SaveDataInTransactionWithResult<T, U>(string storedProcedure, U parameters)
		{
			List<T> rows = _connection.Query<T>(storedProcedure, parameters,
				commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();
			return rows;
		}

		private bool IsClosed = false;

		public void CommitTransaction()
		{
			_transaction?.Commit();
			_connection?.Close();
			IsClosed = true;
		}

		public void RollBackTransaction()
		{
			_transaction.Rollback();
			_connection?.Close();
			IsClosed = true;
		}

		public void Dispose()
		{
			if (!IsClosed)
			{
				try
				{
					CommitTransaction();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Commit transaction failed in the Dispose method.");
				}
			}

			_transaction = null;
			_connection = null;
		}
	}
}
