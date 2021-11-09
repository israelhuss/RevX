using RevXApi.Library.Models;
using System;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IHourlyRateData
	{
		void AddHourlyRate(HourlyRate rate);
		List<HourlyRate> GetAll(string userId, string providerId);
		HourlyRate GetByDate(DateTime date, string userId, string providerId);
		void EditRate(HourlyRate model);
	}
}