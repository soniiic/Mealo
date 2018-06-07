using System.Threading.Tasks;
using Android.Content;
using Athela.Mealo.Core.MessageHandlers;
using Athela.Mealo.Core.Messages;
using Athela.Mealo.Droid.Activities;
using Athela.Mealo.Droid.Notifications;
using Athela.Mealo.Droid.Services;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Newtonsoft.Json;

namespace Athela.Mealo.Droid.MessageHandlers
{
    class MilestonesCreatedMessageHandler : MessageHandler<MilestonesCreatedMessage>
    {
        private readonly NotificationService notificationService;

        public MilestonesCreatedMessageHandler(IMvxMessenger messenger, NotificationService notificationService) : base(messenger)
        {
            this.notificationService = notificationService;
        }

        protected override void Handle(MilestonesCreatedMessage message)
        {
            notificationService.ClearNotifications();
            Task.Factory.StartNew(() =>
            {
                var service = new Intent(MainActivity.Instance, typeof(CookingForegroundService));
                service.PutExtra("milestones", JsonConvert.SerializeObject(message.Milestones));
                var conn = new CookingForegroundServiceConnection();
                Mvx.RegisterSingleton(conn);
                MainActivity.Instance.StartService(service);
                MainActivity.Instance.BindService(service, conn, Bind.AutoCreate);
            });
        }
    }
}