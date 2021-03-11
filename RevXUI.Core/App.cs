using MvvmCross.ViewModels;
using RevXUI.Core.ViewModels;

namespace RevXUI.Core
{
	public class App : MvxApplication
	{
		public override void Initialize()
		{
			RegisterAppStart<SampleViewModel>();
		}
	}
}
