using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IStudentData
	{
		List<StudentModel> GetAll();
		StudentModel GetById(int id);
		void AddStudent(StudentModel model);
	}
}