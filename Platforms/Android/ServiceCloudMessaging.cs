using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using Firebase.Messaging;

namespace NoteApp.Platforms.Android
{
    [Service(Exported = true)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class ServiceCloudMessaging : FirebaseMessagingService
    {
        private const string ChannelId = "default";
        private const string ChannelName = "General Notifications";
        const string channelDescription = "The default channel for notifications.";
        bool channelInitialized = false;

        public override async void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            CreateNotificationChannel();
            var title = message.GetNotification()?.Title;
            var body = message.GetNotification()?.Body;

            PermissionStatus status = await Permissions.RequestAsync<Permissions.PostNotifications>();
            CreateNotificationChannel();
            // Muestra la notificación
            NotificationCompat.Builder builder = new NotificationCompat.Builder(Platform.AppContext, ChannelId)
                .SetContentTitle(title)
                .SetContentText(body)
                .SetSmallIcon(Resource.Drawable.notificacion);
            Notification notification = builder.Build();
            NotificationManagerCompat.From(Platform.AppContext).Notify(0, notification);
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(ChannelName);
                var channel = new NotificationChannel(ChannelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                // Register the channel
                NotificationManager manager = (NotificationManager)Platform.AppContext.GetSystemService(Context.NotificationService);
                manager.CreateNotificationChannel(channel);
                Console.WriteLine(manager.NotificationChannels.FirstOrDefault());
                channelInitialized = true;
            }
        }
    }
}
