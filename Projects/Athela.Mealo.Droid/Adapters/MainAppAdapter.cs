using System.Linq;
using Android.App;
using Android.Support.V4.App;
using Athela.Mealo.Droid.Views;
using Java.Lang;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platform;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace Athela.Mealo.Droid.Adapters
{
    class MainAppAdapter : FragmentPagerAdapter
    {
        private readonly Activity activity;

        public MainAppAdapter(Activity activity, FragmentManager fragmentManager)
            : base(fragmentManager)
        {
            this.activity = activity;
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            switch (position)
            {
                case 0:
                    return new String("Quick Meal");
                case 1:
                    return new String("Timer");
            }
            return null;
        }

        public override int Count => 2;

        public override Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0:
                    return InstantiateFragment<QuickMealFragment>();
                case 1:
                    return InstantiateFragment<CookingFragment>();
            }
            return null;
        }

        Fragment InstantiateFragment<T>() where T : MvxFragment
        {
            var fragment = (MvxFragment)Fragment.Instantiate(activity, Class.FromType(typeof(T)).Name);
            fragment.DataContext = Mvx.Resolve(typeof(T).GetProperties().First(p => p.Name == "ViewModel").PropertyType);
            return fragment;
        }
    }
}