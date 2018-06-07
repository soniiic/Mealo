using Athela.Mealo.Core.MessageHandlers;
using Athela.Mealo.Core.ViewModels;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            CreatableTypes().EndingWith("ViewModel").AsTypes().RegisterAsSingleton();
            CreatableTypes().Inherits(typeof(IMessageHandler)).AsTypes().RegisterAsSingleton();
            Mvx.RegisterType(() => new AddFoodViewModel(Mvx.Resolve<IMvxMessenger>()));

            RegisterAppStart(new AppStart(Mvx.Resolve<IMvxNavigationService>()));
        }
    }
}
