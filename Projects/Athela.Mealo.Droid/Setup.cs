using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Support.Design.Widget;
using Athela.Mealo.Core;
using Athela.Mealo.Core.MessageHandlers;
using Athela.Mealo.Core.Models;
using Athela.Mealo.Droid.Notifications;
using Athela.Mealo.Droid.Services;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Shared.Presenter;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.Messenger;
using Newtonsoft.Json;

namespace Athela.Mealo.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        public override void InitializeSecondary()
        {
            base.InitializeSecondary();

            Mvx.LazyConstructAndRegisterSingleton(() => new NotificationService(Mvx.Resolve<IMvxMessenger>()));
            CreatableTypes().Inherits(typeof(IMessageHandler)).AsTypes().RegisterAsSingleton();
            StartForegroundService();

            OptimiseRuntime();
        }

        private void OptimiseRuntime()
        {
            Task.Factory.StartNew(() => JsonConvert.SerializeObject(new List<Milestone> { new Milestone() }));
        }

        private static void StartForegroundService()
        {
            Application.Context.StartService(new Intent(Application.Context, typeof(CookingForegroundService)));
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFragmentsPresenter = new MvxFragmentsPresenter(AndroidViewAssemblies);
            Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);
            return mvxFragmentsPresenter;
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(NavigationView).Assembly,
            typeof(FloatingActionButton).Assembly,
            typeof(Android.Support.V7.Widget.Toolbar).Assembly,
            typeof(Android.Support.V4.Widget.DrawerLayout).Assembly,
            typeof(Android.Support.V4.View.ViewPager).Assembly,
        };

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            MvxAppCompatSetupHelper.FillTargetFactories(registry);
            base.FillTargetFactories(registry);
        }
    }
}