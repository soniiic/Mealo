using Android.App;
using Android.Content;
using Android.Graphics;
using Android.InputMethodServices;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Athela.Mealo.Core.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views;
using Keycode = Android.Views.Keycode;

namespace Athela.Mealo.Droid.Views
{
    [Register("athela.mealo.droid.views.AddFoodFragment")]
    public class AddFoodFragment : MvxDialogFragment<AddFoodViewModel>
    {
        public AddFoodFragment()
        {
        }

        public override void Dismiss()
        {
            base.Dismiss();
            HideKeyboard();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.AddFoodView, null);
            var foodNameView = view.FindViewById<EditText>(Resource.Id.food_name);
            foodNameView.RequestFocus();
            //ShowKeyboard(foodNameView);
            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            var foodTimeView = View.FindViewById<EditText>(Resource.Id.food_time);
            foodTimeView.EditorAction += FoodTimeView_EditorAction;

            PopulateWithEditValues(foodTimeView);

            SetWindowSize();
            
        }

        private void SetWindowSize()
        {
            var display = Activity.WindowManager.DefaultDisplay;
            var size = new Point();
            display.GetSize(size);

            Dialog.Window.SetLayout(size.X - 50, 700);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.SetSoftInputMode(SoftInput.StateVisible);
            base.OnActivityCreated(savedInstanceState);
        }

        private void PopulateWithEditValues(EditText foodTimeView)
        {
            if (!ViewModel.FoodId.HasValue) return;

            foodTimeView.Text = $"{ViewModel.Time.Value.Hours:#0}:{ViewModel.Time.Value.Minutes:00}";
            var button = View.FindViewById<Button>(Resource.Id.add_to_meal);
            button.Text = "Save";
        }

        private void FoodTimeView_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            if (e.ActionId == ImeAction.Done || (e.Event?.KeyCode == Keycode.Enter && e.Event?.Action == KeyEventActions.Down))
            {
                ViewModel.AddFoodCommand.Execute();
            }
            else
            {
                e.Handled = false;
            }
        }

        private static void HideKeyboardFrom(Context context, View view)
        {
            var imm = (InputMethodManager)context.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(view.WindowToken, 0);
        }

        private void HideKeyboard()
        {
            HideKeyboardFrom(this.Context, View);
        }

        private void ShowKeyboard(View view)
        {
            view.RequestFocus();
            var inputMethodManager = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            inputMethodManager.ShowSoftInput(view, ShowFlags.Implicit);
            inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            ViewModel.CloseDialog = Dismiss;
        }
    }
}