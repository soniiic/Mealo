using Android.Content;
using Android.OS;

namespace Athela.Mealo.Droid.Services
{
    public class CookingForegroundServiceConnection : Java.Lang.Object, IServiceConnection
    {
        public bool IsConnected { get; private set; }
        public CookingBinder Binder { get; private set; }

        public CookingForegroundServiceConnection()
        {
            IsConnected = false;
            Binder = null;
        }

        public void OnServiceConnected(ComponentName name, IBinder binder)
        {
            Binder = binder as CookingBinder;
            IsConnected = Binder != null;
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            IsConnected = false;
            Binder = null;
        }
    }
}