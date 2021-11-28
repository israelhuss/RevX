
namespace RevXPortal.Services
{
	public interface IToastService
	{
		event Action OnHide;
		event Action<string, ToastLevel> OnShow;

		void Dispose();
		void ShowToast(string message, ToastLevel level);
	}
}