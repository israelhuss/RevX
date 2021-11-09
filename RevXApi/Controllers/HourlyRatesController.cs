using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System;
using System.Collections.Generic;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]

	public class HourlyRatesController : ControllerBase
	{
		private readonly IHourlyRateData _rateData;

		public HourlyRatesController(IHourlyRateData rateData)
		{
			_rateData = rateData;
		}

		[HttpGet]
		public List<HourlyRate> GetAll([FromQuery] string userId, [FromQuery] string providerId)
		{
			return _rateData.GetAll(userId, providerId);
		}

		[HttpPost]
		public void AddRate(HourlyRate rate)
		{
			_rateData.AddHourlyRate(rate);
		}

		[HttpGet("date")]
		public HourlyRate GetProviderById([FromQuery] DateTime date, [FromQuery] string userId, [FromQuery] string providerId)
		{
			return _rateData.GetByDate(date, userId, providerId);
		}

		[HttpPost("edit")]
		public void EditRate(HourlyRate model)
		{
			_rateData.EditRate(model);
		}
	}
}
