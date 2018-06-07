using System.Linq;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Athela.Mealo.Core.ViewModels;
using Java.Lang;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platform;

namespace Athela.Mealo.Droid.Views
{
    [Register("athela.mealo.droid.views.QuickMealFragment")]
    public class QuickMealFragment : MvxFragment<QuickMealViewModel>
    {
        private const int MenuContextDeleteFood = 1;

        public QuickMealFragment()
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.QuickMealView, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            RegisterForContextMenu(view.FindViewById<MvxListView>(Resource.Id.quick_meal_food_list));

            ViewModel.ShowAddFoodDialogAction = f =>
            {
                var fragment = (MvxDialogFragment<AddFoodViewModel>)Fragment.Instantiate(this.Activity, Class.FromType(typeof(AddFoodFragment)).Name);
                var viewModel = Mvx.Resolve<AddFoodViewModel>();
                fragment.DataContext = viewModel;
                if (f != null)
                {
                    viewModel.FoodId = f.Id;
                    viewModel.Name = f.Name;
                    viewModel.Temperature = f.Temperature;
                    viewModel.Time = f.Time;
                }
                fragment.Show(FragmentManager, "dialog add food");
            };
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            if (item.ItemId == MenuContextDeleteFood)
            {
                var menuInfo = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
                ViewModel.RemoveFoodCommand.Execute(ViewModel.Foods.ElementAt(menuInfo.Position));
                return true;
            }

            return base.OnContextItemSelected(item);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);

            if (v.Id == Resource.Id.quick_meal_food_list)
            {
                menu.Add(Menu.None, MenuContextDeleteFood, 0, "Remove");
            }
        }
    }
}