using Android.OS;

namespace Athela.Mealo.Droid.Services
{
    public class CookingBinder : Binder
    {
        public CookingForegroundService Service { get; }

        public CookingBinder(CookingForegroundService service)
        {
            Service = service;
        }
    }
}