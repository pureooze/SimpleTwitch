using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimpleTwitch.Services;

namespace SimpleTwitch {
    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts( fonts => {
                    fonts.AddFont( "OpenSans-Regular.ttf", "OpenSansRegular" );
                } );

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddHttpClient( "LocalClient", client => {
                client.BaseAddress = new Uri( "app://localhost/" );
            } );

            builder.Configuration.AddJsonFile( "appsettings.json", true, true );
            builder.Configuration.AddJsonFile( "twitch.json", true, true );

            builder.Services.AddSingleton<EventService>();
            builder.Services.AddSingleton<ITwitchService , TwitchService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
