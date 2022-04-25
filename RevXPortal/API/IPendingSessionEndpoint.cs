
namespace RevXPortal.API
{
	public interface IPendingSessionEndpoint
	{
		Task AddSchedule(ScheduleModel schedule);
		Task Delete(int id);
		Task<List<SessionApiModel>> GetByMonth(int month, int year);
	}
}