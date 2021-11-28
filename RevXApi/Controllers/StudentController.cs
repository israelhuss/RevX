using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System.Collections.Generic;
using System.Security.Claims;

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
		public List<StudentModel> GetAllStudents()
		{
			return _studentData.GetAll(User.FindFirstValue(ClaimTypes.NameIdentifier));
		}

		[HttpGet("enabled")]
		public List<StudentModel> GetEnabled()
		{
			return _studentData.GetEnabled(User.FindFirstValue(ClaimTypes.NameIdentifier));
		}

		[HttpPost("add")]
		public void AddStudent(StudentModel model)
		{
			model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			_studentData.AddStudent(model);
		}

		[HttpPost("edit")]
		public void EditStudent(StudentModel model)
		{
			model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			_studentData.EditStudent(model);
		}

		[HttpGet("{id}")]
		public StudentModel GetStudentById(int id)
		{
			return _studentData.GetById(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
		}
	}
}
