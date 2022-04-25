
namespace RevXPortal.API
{
	public interface IHebcalEndpoint
	{
		Task<List<HebcalEvent>> GetHebcalEvents(int month, int year);
	}
}