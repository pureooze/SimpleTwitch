using TwitchEverywhere.Core.Types;
using TwitchEverywhere.Core.Types.Messages.Interfaces;
using TwitchEverywhere.Core.Types.RestApi.Wrappers;

namespace SimpleTwitch.Services;

public interface ITwitchService {
    
    void ConnectToIrcChannel( 
        string channel,
        Action<IPrivMsg> messageCallback
    );

    Task DisconnectFromIrcChannel();
    
    Task<GetUsersResponse> GetUsers( 
        string username
    );
    
    Task<GetChannelInfoResponse> GetChannelInfo( 
        string broadcasterId
    );
}