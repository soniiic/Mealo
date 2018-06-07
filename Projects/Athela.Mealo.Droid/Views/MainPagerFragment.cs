using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Athela.Mealo.Core.ViewModels;
using Athela.Mealo.Droid.Adapters;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;

namespace Athela.Mealo.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("athela.mealo.droid.views.MainPagerFragment")]
    public class MainPagerFragment : MvxFragment<PagerViewModel>
    {
        public MainPagerFragment()
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.PagerView, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var viewPager = view.FindViewById<ViewPager>(Resource.Id.ViewPager);
            viewPager.Adapter = new MainAppAdapter(Activity, Activity.SupportFragmentManager);
            var tabLayout = view.FindViewById<TabLayout>(Resource.Id.TabLayout);
            tabLayout.SetupWithViewPager(viewPager);

            ViewModel.ShowTimerAction = () => viewPager.SetCurrentItem(1, true);
        }
    }
}