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
		public List<ProviderModel> GetAllProviders()
		{
			return _providerData.GetAll();
		}

		[HttpPost]
		public void AddProvider(ProviderModel model)
		{
			_providerData.AddProvider(model);
		}

		[HttpGet("{id}")]
		public ProviderModel GetProviderById(int id)
		{
			return _providerData.GetById(id);
		}
	}
}
