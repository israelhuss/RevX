﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

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
		public List<HourlyRate> GetAll([FromQuery] int providerId)
		{
			return _rateData.GetAll(User.FindFirstValue(ClaimTypes.NameIdentifier), providerId);
		}

		[HttpPost]
		public void AddRate(HourlyRate rate)
		{
			rate.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			_rateData.AddHourlyRate(rate);
		}

		[HttpGet("date")]
		public HourlyRate GetProviderById([FromQuery] DateTime date, [FromQuery] string userId, [FromQuery] int providerId)
		{
			userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			return _rateData.GetByDate(date, userId, providerId);
		}

		[HttpPost("edit")]
		public void EditRate(HourlyRate model)
		{
			model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			_rateData.EditRate(model);
		}
	}
}
