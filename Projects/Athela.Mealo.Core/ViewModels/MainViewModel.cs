using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Athela.Mealo.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;

        public MainViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public async void ShowPagerView()
        {
            await navigationService.Navigate(Mvx.Resolve<PagerViewModel>());
        }
    }
}