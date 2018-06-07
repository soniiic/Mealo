using Athela.Mealo.Core.MessageHandlers;
using Athela.Mealo.Core.Messages;
using Athela.Mealo.Droid.Notifications;
using Athela.Mealo.Droid.Services;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Droid.MessageHandlers
{
    class CookingCanceledMessageHandler : MessageHandler<CookingCanceledMessage>
    {
        private readonly NotificationService notificationService;

        public CookingCanceledMessageHandler(IMvxMessenger messenger, NotificationService notificationService) : base(messenger)
        {
            this.notificationService = notificationService;
        }

        protected override void Handle(CookingCanceledMessage message)
        {
            if (Mvx.CanResolve<CookingForegroundServiceConnection>())
            {
                Mvx.Resolve<CookingForegroundServiceConnection>().Binder.Service.Stop();
            }

            notificationService.ClearNotifications();
        }
    }
}