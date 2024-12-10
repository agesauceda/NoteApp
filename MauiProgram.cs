using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Plugin.Firebase.Auth;
using Plugin.Firebase.Bundled.Shared;
using Plugin.Firebase.Crashlytics;
using Plugin.Firebase.Core;
using NoteApp.Controllers.Auth;
using Microsoft.Maui.Handlers;

namespace NoteApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .RegisterFirebaseServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<Client.Client>(s, "http://35.202.249.88:15000/api/v1/"));
            builder.Services.AddSingleton<QueueLogin>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                events.AddAndroid(android => android.OnCreate((activity, _) =>
                {
                    CrossFirebaseCrashlytics.Current.SetCrashlyticsCollectionEnabled(true);
                    Firebase.FirebaseApp.InitializeApp(activity);
                }));
#else
#endif
                // Enable Firebase Crashlytics
            });

            // Register Firebase Auth as a Singleton Service
            builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);

            return builder;
        }
    }
}
