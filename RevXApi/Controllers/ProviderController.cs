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
	public class ProviderController : ControllerBase
	{
		private readonly IProviderData _providerData;

		public ProviderController(IProviderData providerData)
		{
			_providerData = providerData;
		}

		[HttpGet]
		public List<ProviderModel> GetAllProviders([FromQuery] string userId)
		{
			return _providerData.GetAll(userId);
		}

		[HttpGet("enabled")]
		public List<ProviderModel> GetEnabled([FromQuery] string userId)
		{
			return _providerData.GetEnabled(userId);
		}

		[HttpPost("add")]
		public void AddProvider(ProviderModel model)
		{
			_providerData.AddProvider(model);
		}

		[HttpPost("edit")]
		public void EditProvider(ProviderModel model)
		{
			_providerData.EditProvider(model);
		}

		[HttpGet("{id}")]
		public ProviderModel GetProviderById(int id, [FromQuery] string userId)
		{
			return _providerData.GetById(id, userId);
		}
	}
}
