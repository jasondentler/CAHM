using System.Collections.Generic;

namespace CAHM.Models
{
    public class Game
    {

        public Game()
        {
            Players = new List<AccountReference>();
        }

        public string Id { get; set; }
        public List<AccountReference> Players { get; private set; }
        public bool GameStarted { get; set; }

    }
}
