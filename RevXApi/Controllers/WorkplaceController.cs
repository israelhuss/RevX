using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System.Security.Claims;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class WorkplaceController : ControllerBase
	{
		private readonly IWorkplaceData _workplaceData;

		public WorkplaceController(IWorkplaceData workplaceData)
		{
			_workplaceData = workplaceData;
		}

		[HttpGet]
		[Route("GetMyInfo")]
		public WorkplaceModel GetMyWorkplaceInfo()
		{
			return _workplaceData.GetCurrentUserInfo(User.FindFirstValue(ClaimTypes.NameIdentifier));
		}
	}
}
