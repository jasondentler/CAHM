using System.Collections.Generic;

namespace CAHM.ViewModels
{
    public class NewGameModel
    {


        public NewGameModel()
        {
            GravatarHashes = new HashSet<string>();
        }

        public string Id { get; set; }
        public HashSet<string> GravatarHashes { get; private set; }
        public bool GameStarted { get; set; }

    }
}
