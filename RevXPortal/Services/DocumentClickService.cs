using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace RevXPortal.Services
{
	public class DocumentClickService
	{
		public static IJSRuntime JSRuntime { get; }

		public static event Action<MouseClickArgs> OnClick;


		[JSInvokable]
		public static void OnDocumentClicked(MouseClickArgs e)
		{
			JSRuntime.InvokeVoidAsync("log", e);
			OnClick?.Invoke(e);
		}
	}
}
