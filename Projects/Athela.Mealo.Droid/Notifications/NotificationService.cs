using System.Collections.Generic;
using Android.App;
using Athela.Mealo.Core.Messages;
using Athela.Mealo.Droid.Activities;
using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Droid.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly List<MvxSubscriptionToken> subscriptionTokens = new List<MvxSubscriptionToken>();

        public NotificationService(IMvxMessenger messenger)
        {
            subscriptionTokens.Add(messenger.Subscribe<CookingCanceledMessage>(m => ClearNotifications()));
        }

        public void ClearNotifications()
        {
            var notificationManager = NotificationManager.FromContext(MainActivity.Instance);
            notificationManager.CancelAll();
        }
    }
}