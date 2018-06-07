using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Athela.Mealo.Core.ViewModels;
using Athela.Mealo.Droid.Extensions;
using Athela.Mealo.Droid.Services;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Athela.Mealo.Droid.Activities
{
    [Activity(Label = "Main Activity", Theme = "@style/AppTheme",
        LaunchMode = LaunchMode.SingleTop,
        ScreenOrientation = ScreenOrientation.Portrait,
        Name = "athela.mealo.droid.activities.MainActivity")]
    public class MainActivity : MvxCachingFragmentCompatActivity<MainViewModel>
    {
        public static MainActivity Instance { get; private set; }

        public new MainViewModel ViewModel
        {
            get => base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void OnPause()
        {
            base.OnPause();
            IsVisible = false;
        }

        protected override void OnResume()
        {
            base.OnResume();
            IsVisible = true;
        }

        public bool IsVisible { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Instance = this;
            IsVisible = true;
            SetContentView(Resource.Layout.MainView);

            this.SetCustomTitle("Mealo");
            ViewModel.ShowPagerView();
        }
    }
}