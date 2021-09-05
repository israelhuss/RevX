﻿using RevXPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public interface IStudentEndpoint
	{
		Task AddStudent(StudentModel model);
		Task<List<StudentModel>> GetAll(string userId);
		//Task<List<StudentModel>> GetAll();
		Task<List<StudentModel>> GetEnabled(string userId);
		Task EditStudent(StudentModel model);
	}
}