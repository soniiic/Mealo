using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Athela.Mealo.Core.Models;
using Athela.Mealo.Droid.Activities;
using Newtonsoft.Json;

namespace Athela.Mealo.Droid.Services
{
    [Service(Name = "la.athe.MealoService",
        IsolatedProcess = true,
        Process = "la.athe.MealoService_process",
        Exported = true)]
    public class CookingForegroundService : Service
    {
        private CancellationTokenSource cancellationTokenSource;
        private Notification.Builder progressBuilder;
        private NotificationManager notificationManager;

        private const int ForegroundNotificationId = 10000;
        private const int MealReadyNotificationId = 10001;
        private const double OneMinuteAndFiveSeconds = 1 + (5 / 60);

        public override IBinder OnBind(Intent intent)
        {
            return new CookingBinder(this);
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            if (intent.Extras == null)
            {
                StopForeground(true);
                return StartCommandResult.NotSticky;
            }

            cancellationTokenSource?.Cancel();
            var milestones = JsonConvert.DeserializeObject<List<Milestone>>(intent.GetStringExtra("milestones"));
            cancellationTokenSource = new CancellationTokenSource();
            notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            Task.Factory.StartNew(() => { RunNotificationProgressBar(milestones, cancellationTokenSource.Token); }, cancellationTokenSource.Token);

            return StartCommandResult.Sticky;
        }

        private void RunNotificationProgressBar(List<Milestone> milestones, CancellationToken token)
        {
            SetProgressBuilder(milestones.First(), milestones.ElementAt(1));
            for (var i = 0; i < milestones.Count - 1; i++)
            {
                var milestone = milestones[i];
                ShowActionNotification(milestone, notificationManager, i);

                UpdateProgressBuilderText(milestone, milestones[i + 1]);
                while (milestone.EndTime.ToUniversalTime() > DateTime.UtcNow)
                {
                    var currentCookingTime = DateTime.Now - milestone.StartTime;
                    var percentage = (currentCookingTime.TotalMilliseconds / milestone.CookingTime.TotalMilliseconds) *
                                     100;

                    progressBuilder.SetProgress(100, (int)percentage, false);

                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    notificationManager.Notify(ForegroundNotificationId, progressBuilder.Build());

                    Thread.Sleep((milestone.EndTime - DateTime.Now).TotalMinutes <= OneMinuteAndFiveSeconds ? 1000 : 5000);
                }
            }

            ShowFinishedNotification(notificationManager);
            StopForeground(true);
        }

        private void ShowFinishedNotification(NotificationManager notificationManager)
        {
            var resultIntent = new Intent(MainActivity.Instance, typeof(MainActivity));
            resultIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);

            var builder = new Notification.Builder(this)
                .SetContentTitle("Your meal is ready!")
                .SetSmallIcon(Resource.Drawable.Icon_24)
                .SetContentIntent(PendingIntent.GetActivity(MainActivity.Instance, 0, resultIntent, PendingIntentFlags.OneShot))
                .SetAutoCancel(true)
                .SetPriority((int)NotificationPriority.High);

            if (!MainActivity.Instance.IsVisible)
            {
                builder
                    .SetDefaults(NotificationDefaults.All)
                    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Alarm));
            }

            notificationManager.Notify(MealReadyNotificationId, builder.Build());
        }

        private void ShowActionNotification(Milestone milestone, NotificationManager notificationManager, int i)
        {
            var resultIntent = new Intent(MainActivity.Instance, typeof(MainActivity));
            resultIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);

            var builder = new Notification.Builder(this)
                .SetContentTitle($"Put the {milestone.ItemsToStartCooking} in at {milestone.Temperature}°")
                .SetSmallIcon(Resource.Drawable.Icon_24)
                .SetContentIntent(PendingIntent.GetActivity(MainActivity.Instance, 0, resultIntent, PendingIntentFlags.OneShot))
                .SetAutoCancel(true)
                .SetPriority((int)NotificationPriority.High);

            if (!MainActivity.Instance.IsVisible)
            {
                builder
                    .SetDefaults(NotificationDefaults.All)
                    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Alarm));
            }

            notificationManager.Notify(i, builder.Build());
        }

        private void SetProgressBuilder(Milestone milestone, Milestone nextMilestone)
        {
            var resultIntent = new Intent(MainActivity.Instance, typeof(MainActivity));
            resultIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);

            progressBuilder = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.Icon_24)
                .SetContentIntent(PendingIntent.GetActivity(MainActivity.Instance, 0, resultIntent, PendingIntentFlags.OneShot))
                .SetOnlyAlertOnce(true)
                .SetShowWhen(false)
                .SetOngoing(true)
                .SetPriority((int)NotificationPriority.Min)
                .SetProgress(100, 0, false);

            UpdateProgressBuilderText(milestone, nextMilestone);
        }

        private void UpdateProgressBuilderText(Milestone milestone, Milestone nextMilestone)
        {
            progressBuilder
                .SetContentTitle($"Cooking {milestone.ItemsToStartCooking} at {milestone.Temperature}°")
                .SetContentText($"Next in: {nextMilestone.ItemsToStartCooking} at {nextMilestone.StartTime:HH:mm}");
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();
            StopForeground(true);
        }
    }
}