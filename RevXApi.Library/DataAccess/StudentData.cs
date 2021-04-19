using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Library.DataAccess
{
	public class StudentData : IStudentData
	{
		private readonly ISqlDataAccess _sql;

		public StudentData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public List<StudentModel> GetAll()
		{
			return _sql.LoadData<StudentModel>("dbo.spStudent_GetAll", "RevXData");
		}

		public StudentModel GetById(int id)
		{
			return _sql.LoadData<StudentModel, dynamic>("dbo.spStudent_GetById", new { Id = id }, "RevXData").FirstOrDefault();
			
		}

		public void AddStudent(StudentModel model)
		{
			_sql.SaveData("dbo.spStudent_Insert", model, "RevXData");
		}
	}
}
