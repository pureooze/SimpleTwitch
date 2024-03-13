using System.Reactive.Disposables;

namespace SimpleTwitch.Observables;

public class NavigationObservable : IObservable<string> {
    
    private readonly List<IObserver<string>> _observers = [];
    
    public IDisposable Subscribe(IObserver<string> observer) {
        
        if (!_observers.Contains(observer)) {
            _observers.Add(observer);
        }
        
        return Disposable.Create( () => Unsubscribe(observer) );
    }
    
    public void Notify(string value) {
        foreach (var observer in _observers) {
            observer.OnNext(value);
        }
    }
    
    private void Unsubscribe(IObserver<string> observer) {
        _observers.Remove(observer);
    }
    
}