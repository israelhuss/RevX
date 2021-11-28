using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public interface IHourlyRateEndpoint
	{
		Task AddRate(HourlyRate model);
		Task EditRate(HourlyRate model);
		Task<List<HourlyRate>> GetAll();
		Task<HourlyRate> GetByDate(DateTime date);
	}
}