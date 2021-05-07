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
	public class BillingStatusController : ControllerBase
	{
		private readonly IBillingStatusData _billingStatusData;

		public BillingStatusController(IBillingStatusData billingStatusData)
		{
			_billingStatusData = billingStatusData;
		}

		[HttpGet]
		public List<BillingStatusModel> GetAllBillingStatus()
		{
			return _billingStatusData.GetAll();
		}

		[HttpPost]
		public void AddBillingStatus(BillingStatusModel model)
		{
			_billingStatusData.AddBillingStatus(model);
		}

		[HttpGet("{id}")]
		public BillingStatusModel GetBillingStatusById(int id)
		{
			return _billingStatusData.GetById(id);
		}
	}
}
