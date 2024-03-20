using System.Reactive;
using System.Reactive.Disposables;
using TwitchEverywhere.Core.Types.Messages.Interfaces;

namespace SimpleTwitch.Observables;

internal class ChatMsgObservable : IObservable<IPrivMsg> {

    private readonly List<IObserver<IPrivMsg>> m_observers = [];

    public ChatMsgObservable(
        IObservable<IPrivMsg>? privMsgObservable
    ) {
        privMsgObservable?.Subscribe( Notify );
    }
    
    public IDisposable Subscribe(
        IObserver<IPrivMsg> observer
    ) {
        if( !m_observers.Contains( observer ) ) {
            m_observers.Add( observer );
        }

        return Disposable.Create( () => Unsubscribe( observer ) );
    }

    public void Dispose() {
        foreach (IObserver<IPrivMsg> observer in m_observers) {
            observer.OnCompleted();
        }
    }

    private void Notify(
        IPrivMsg value
    ) {
        foreach (IObserver<IPrivMsg> observer in m_observers) {
            observer.OnNext( value );
        }
    }

    private void Unsubscribe(
        IObserver<IPrivMsg> observer
    ) {
        m_observers.Remove( observer );
    }
}