﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimpleTwitch.Services;

namespace SimpleTwitch {
    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {
            MauiAppBuilder? builder = MauiApp.CreateBuilder();
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

            builder.Services.AddSingleton<ITwitchService , TwitchService>();
            builder.Services.AddSingleton<ResourceService , ResourceService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
