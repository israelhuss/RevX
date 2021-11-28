using RevXApi.Library.Models;
using System;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IHourlyRateData
	{
		void AddHourlyRate(HourlyRate rate);
		List<HourlyRate> GetAll(string userId, int providerId);
		HourlyRate GetByDate(DateTime date, string userId, int providerId);
		void EditRate(HourlyRate model);
	}
}