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

		[HttpGet("enabled")]
		public List<BillingStatusModel> GetEnabled()
		{
			return _billingStatusData.GetEnabled();
		}

		[HttpPost("add")]
		public void AddBillingStatus(BillingStatusModel model)
		{
			_billingStatusData.AddBillingStatus(model);
		}

		[HttpPost("edit")]
		public void EditBillingStatus(BillingStatusModel model)
		{
			_billingStatusData.EditBillingStatus(model);
		}

		[HttpGet("{id}")]
		public BillingStatusModel GetBillingStatusById(int id)
		{
			return _billingStatusData.GetById(id);
		}
	}
}
