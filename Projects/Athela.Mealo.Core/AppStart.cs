using Athela.Mealo.Core.ViewModels;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace Athela.Mealo.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        private readonly IMvxNavigationService navigationService;

        public AppStart(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public async void Start(object hint = null)
        {
            await navigationService.Navigate<MainViewModel>();
        }
    }
}
