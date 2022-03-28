
namespace RevXPortal.API
{
	public interface IWorkplaceEndpoint
	{
		Task<WorkplaceModel> GetMyWorkplaceInfo();
	}
}