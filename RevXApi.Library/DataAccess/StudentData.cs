using RevXApi.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace RevXApi.Library.DataAccess
{
	public class StudentData : IStudentData
	{
		private readonly ISqlDataAccess _sql;

		public StudentData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public List<StudentModel> GetAll(string userId)
		{
			return _sql.LoadData<StudentModel, dynamic>("dbo.spStudent_GetAll", new { userId }, "RevXData");
		}

		public List<StudentModel> GetEnabled(string userId)
		{
			return _sql.LoadData<StudentModel, dynamic>("dbo.spStudent_GetEnabled", new { userId }, "RevXData");
		}

		public StudentModel GetById(int id, string userId)
		{
			return _sql.LoadData<StudentModel, dynamic>("dbo.spStudent_GetById", new { Id = id, userId }, "RevXData").FirstOrDefault();

		}

		public void AddStudent(StudentModel model)
		{
			_sql.SaveData("dbo.spStudent_Insert", model, "RevXData");
		}

		public void EditStudent(StudentModel model)
		{
			_sql.SaveData("dbo.spStudent_Update", model, "RevXData");
		}
	}
}
