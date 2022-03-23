using Microsoft.JSInterop;

namespace RevXPortal.Services
{
	public class BrowserResizeService
	{
		public static event Func<Task> OnResize;

		[JSInvokable]
		public static async Task OnBrowserResize()
		{
			await OnResize?.Invoke();
		}
	}
}
