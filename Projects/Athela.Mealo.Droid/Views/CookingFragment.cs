using Android.OS;
using Android.Runtime;
using Android.Views;
using Athela.Mealo.Core.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;

namespace Athela.Mealo.Droid.Views
{
    [Register("athela.mealo.droid.views.CookingFragment")]
    public class CookingFragment : MvxFragment<CookingViewModel>
    {
        public CookingFragment()
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.CookingView, null);
        }
    }
}