using Microsoft.Extensions.Configuration;
using SimpleTwitch.Observables;
using TwitchEverywhere.Core;
using TwitchEverywhere.Core.Types.Messages.Interfaces;
using TwitchEverywhere.Core.Types.RestApi.Wrappers;
using TwitchEverywhere.Irc;
using TwitchEverywhere.Irc.Types;
using TwitchEverywhere.Rest;

namespace SimpleTwitch.Services;

internal class TwitchService {
    private readonly IConfiguration m_configuration;
    private readonly List<IObserver<IPrivMsg>> m_privMsgObservers = [];
    private readonly List<IObserver<IJoinMsg>> m_joinMsgObservers = [];
    private IrcClient? m_ircClient;
    private RestClient? m_restClient;
    private IrcClientObservable? m_ircObservable;

    public TwitchService( IConfiguration configuration ) {
        m_configuration = configuration;
        
        InitializeRestClient();
    }

    public async Task ConnectToIrcChannel(
        string channel
    ) {
        TwitchConnectionOptions options = new(
            AccessToken: m_configuration.GetValue<string>( "AccessToken" ),
            RefreshToken: m_configuration.GetValue<string>( "RefreshToken" ),
            ClientId: m_configuration.GetValue<string>( "ClientId" ),
            ClientSecret: m_configuration.GetValue<string>( "ClientSecret" ),
            ClientName: m_configuration.GetValue<string>( "ClientName" )
        );
        
        if (m_ircClient == null) {
            m_ircClient = new IrcClient( options );
        } else {
            await m_ircClient.Disconnect();
        }

        m_ircObservable = m_ircClient?.ConnectToChannelRx( channel: channel );
    }
    
    public void SubscribeToPrivMsg( 
        IObserver<IPrivMsg> observer 
    ) {
        if (m_ircObservable == null) {
            throw new InvalidOperationException( "IRC client is not connected to a channel" );
        }
        
        if (m_privMsgObservers.Contains( observer )) {
            return;
        }

        m_privMsgObservers.Add(observer);
        m_ircObservable.PrivMsgObservable.Subscribe( observer );
    }
    
    public void SubscribeToJoinMsg( 
        IObserver<IJoinMsg> observer 
    ) {
        if (m_ircObservable == null) {
            throw new InvalidOperationException( "IRC client is not connected to a channel" );
        }
        
        if (m_joinMsgObservers.Contains( observer )) {
            return;
        }

        m_joinMsgObservers.Add(observer);
        m_ircObservable.JoinObservable.Subscribe( observer );
    }

    public Task<GetUsersResponse> GetUsers(
        string username
    ) {
        InitializeRestClient();
        return m_restClient.GetUsersByLogin( [username] );
    }
    
    public Task<GetChannelInfoResponse> GetChannelInfo(
        string broadcasterId
    ) {
        InitializeRestClient();
        return m_restClient.GetChannelInfo( broadcasterId );
    }
    
    public Task<GetChannelSearchResponse> SearchForChannel(
        string query
    ) {
        InitializeRestClient();
        return m_restClient.SearchForChannel( query, 6 );
    }
    
    public Task<GetStreamsResponse> GetLiveStreams(
        string[] logins
    ) {
        InitializeRestClient();
        return m_restClient.GetStreams( logins );
    }
    
    private void InitializeRestClient() {
        TwitchConnectionOptions options = new(
            AccessToken: m_configuration.GetValue<string>( "AccessToken" ),
            RefreshToken: m_configuration.GetValue<string>( "RefreshToken" ),
            ClientId: m_configuration.GetValue<string>( "ClientId" ),
            ClientSecret: m_configuration.GetValue<string>( "ClientSecret" ),
            ClientName: m_configuration.GetValue<string>( "ClientName" )
        );
        
        m_restClient ??= new RestClient( options );
    }
}