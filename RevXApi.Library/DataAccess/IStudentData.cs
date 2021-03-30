using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IStudentData
	{
		List<StudentModel> GetAll();
		void AddStudent(StudentModel model);
	}
}