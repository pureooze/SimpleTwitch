using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTwitch.Services {
    internal class EventService {
        public event Action<string> OnEnterPressed;
        
        public void HandleChangeUser(
            string user
        ) {
            OnEnterPressed?.Invoke(user);
        }
    }
}
