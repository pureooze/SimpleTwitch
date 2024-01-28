using TwitchEverywhere.Core.Types;
using TwitchEverywhere.Core.Types.RestApi.Wrappers;

namespace SimpleTwitch.Services;

public interface ITwitchService {
    
    Task ConnectToIrcChannel( 
        string channel,
        Action<IMessage> messageCallback
    );

    Task DisconnectFromIrcChannel();
    
    Task<GetUsersResponse> GetUsers( 
        string username
    );
    
    Task<GetChannelInfoResponse> GetChannelInfo( 
        string broadcasterId
    );
}