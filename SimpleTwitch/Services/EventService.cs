namespace SimpleTwitch.Services {
    internal class EventService {
        public event EventHandler<string> OnEnterPressed;
        private string name = "";
        
        public void HandleChangeUser(
            string user
        ) {
            name = user;
            OnEnterPressed?.Invoke(this, name);
        }
    }
}
