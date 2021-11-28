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
	public class ProviderController : ControllerBase
	{
		private readonly IProviderData _providerData;

		public ProviderController(IProviderData providerData)
		{
			_providerData = providerData;
		}

		[HttpGet]
		public List<ProviderModel> GetAllProviders()
		{
			return _providerData.GetAll(User.FindFirstValue(ClaimTypes.NameIdentifier));
		}

		[HttpGet("enabled")]
		public List<ProviderModel> GetEnabled()
		{
			return _providerData.GetEnabled(User.FindFirstValue(ClaimTypes.NameIdentifier));
		}

		[HttpPost("add")]
		public void AddProvider(ProviderModel model)
		{
			model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			_providerData.AddProvider(model);
		}

		[HttpPost("edit")]
		public void EditProvider(ProviderModel model)
		{
			model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			_providerData.EditProvider(model);
		}

		[HttpGet("{id}")]
		public ProviderModel GetProviderById(int id)
		{
			return _providerData.GetById(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
		}
	}
}
