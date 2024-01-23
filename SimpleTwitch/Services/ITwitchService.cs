using TwitchEverywhere.Core.Types;

namespace SimpleTwitch.Services;

public interface ITwitchService {
    
    Task ConnectToIrcChannel( 
        string channel,
        Action<IMessage> messageCallback
    );

    Task DisconnectFromIrcChannel();
}