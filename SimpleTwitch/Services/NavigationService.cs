using System.Reactive;
using Microsoft.AspNetCore.Components;

namespace SimpleTwitch.Services;

public class NavigationService {
    private readonly IObserver<string> _observer;
    
    public NavigationService(
        NavigationManager navigationManager
    ) {
        _observer = Observer.Create<string>(
            onNext: (input) => navigationManager.NavigateTo( $"/stream/{input}" )
        );
    }
    
}