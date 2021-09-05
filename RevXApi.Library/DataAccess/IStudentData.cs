using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IStudentData
	{
		List<StudentModel> GetAll(string userId);
		List<StudentModel> GetEnabled(string userId);
		StudentModel GetById(int id, string userId);
		void AddStudent(StudentModel model);
		void EditStudent(StudentModel model);
	}
}