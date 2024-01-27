﻿using Microsoft.Extensions.Configuration;
using TwitchEverywhere.Core;
using TwitchEverywhere.Core.Types;
using TwitchEverywhere.Core.Types.RestApi.Wrappers;
using TwitchEverywhere.Irc;
using TwitchEverywhere.Rest;

namespace SimpleTwitch.Services;

public class TwitchService : ITwitchService {
    private IrcClient? m_ircClient;
    private RestClient? m_restClient;
    private IConfiguration m_configuration;
    
    public TwitchService( IConfiguration configuration ) {
        m_configuration = configuration;
        
        InitializeRestClient();
    }

    async Task ITwitchService.ConnectToIrcChannel(
        string channel,
        Action<IMessage> messageCallback
    ) {
        InitializeIrcClient( channel );
        await m_ircClient.ConnectToChannel( messageCallback );
    }

    async Task ITwitchService.DisconnectFromIrcChannel() {
        if ( m_ircClient is null ) {
            return;
        }
        
        await m_ircClient.Disconnect();
        m_ircClient = null;
    }
    
    Task<GetUsersResponse> ITwitchService.GetUsers(
        string username
    ) {
        InitializeRestClient();
        return m_restClient.GetUsersByLogin( [username] );
    }

    private void InitializeIrcClient( string channel ) {
        TwitchConnectionOptions options = new(
            Channel: channel,
            AccessToken: m_configuration.GetValue<string>( "AccessToken" ),
            RefreshToken: m_configuration.GetValue<string>( "RefreshToken" ),
            ClientId: m_configuration.GetValue<string>( "ClientId" ),
            ClientSecret: m_configuration.GetValue<string>( "ClientSecret" ),
            ClientName: m_configuration.GetValue<string>( "ClientName" )
        );
        
        m_ircClient ??= new IrcClient( options );
    }
    
    private void InitializeRestClient() {
        TwitchConnectionOptions options = new(
            Channel: "",
            AccessToken: m_configuration.GetValue<string>( "AccessToken" ),
            RefreshToken: m_configuration.GetValue<string>( "RefreshToken" ),
            ClientId: m_configuration.GetValue<string>( "ClientId" ),
            ClientSecret: m_configuration.GetValue<string>( "ClientSecret" ),
            ClientName: m_configuration.GetValue<string>( "ClientName" )
        );
        
        m_restClient ??= new RestClient( options );
    }
}