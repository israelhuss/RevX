﻿using RevXPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public interface IProviderEndpoint
	{
		Task AddProvider(ProviderModel model);
		Task<List<ProviderModel>> GetEnabled();
		Task EditProvider(ProviderModel model);
		Task<List<ProviderModel>> GetAll();
	}
}