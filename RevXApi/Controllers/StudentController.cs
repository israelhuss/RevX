using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private readonly IStudentData _studentData;

		public StudentController(IStudentData studentData)
		{
			_studentData = studentData;
		}

		[HttpGet]
		public List<StudentModel> GetAllStudents()
		{
			return _studentData.GetAll();
		}

		[HttpPost]
		public void AddStudent(StudentModel model)
		{
			_studentData.AddStudent(model);
		}

		[HttpGet("{id}")]
		public StudentModel GetStudentById(int id)
		{
			return _studentData.GetById(id);
		}
	}
}
