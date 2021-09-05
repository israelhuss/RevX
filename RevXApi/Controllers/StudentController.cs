using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class StudentController : ControllerBase
	{
		private readonly IStudentData _studentData;

		public StudentController(IStudentData studentData)
		{
			_studentData = studentData;
		}

		[HttpGet]
		public List<StudentModel> GetAllStudents([FromQuery] string userId)
		{
			return _studentData.GetAll(userId);
		}

		[HttpGet("enabled")]
		public List<StudentModel> GetEnabled([FromQuery] string userId)
		{
			return _studentData.GetEnabled(userId);
		}

		[HttpPost("add")]
		public void AddStudent(StudentModel model)
		{
			_studentData.AddStudent(model);
		}

		[HttpPost("edit")]
		public void EditStudent(StudentModel model)
		{
			_studentData.EditStudent(model);
		}

		[HttpGet("{id}")]
		public StudentModel GetStudentById(int id, [FromQuery] string userId)
		{
			return _studentData.GetById(id, userId);
		}
	}
}
