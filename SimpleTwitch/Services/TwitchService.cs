using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using TwitchEverywhere.Core;
using TwitchEverywhere.Core.Types;
using TwitchEverywhere.Irc;

namespace SimpleTwitch.Services;

public class TwitchService : ITwitchService {
    private IrcClient m_ircClient;
    private IConfiguration m_configuration;
    
    public TwitchService( IConfiguration configuration ) {
        m_configuration = configuration;
    }

    async Task ITwitchService.ConnectToIrcChannel(
        string channel,
        Action<IMessage> messageCallback
    ) {
        TwitchConnectionOptions options = new(
            Channel: channel,
            AccessToken: m_configuration.GetValue<string>( "AccessToken" ),
            RefreshToken: m_configuration.GetValue<string>( "RefreshToken" ),
            ClientId: m_configuration.GetValue<string>( "ClientId" ),
            ClientSecret: m_configuration.GetValue<string>( "ClientSecret" ),
            ClientName: m_configuration.GetValue<string>( "ClientName" )
        );

        m_ircClient = new IrcClient( options );
        await m_ircClient.ConnectToChannel( messageCallback );
    }

    async Task ITwitchService.DisconnectFromIrcChannel() {
        await m_ircClient.Disconnect();
    }
}