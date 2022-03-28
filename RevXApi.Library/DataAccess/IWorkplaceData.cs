using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IWorkplaceData
	{
		WorkplaceModel GetCurrentUserInfo(string userId);
	}
}