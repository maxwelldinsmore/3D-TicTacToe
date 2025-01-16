// Player Object Class
// Contains information about players
// Such as their color and associated Ring assets
// based off their number
// used color object can reference when adding to main screen
using System.Windows.Media;

namespace COSC2100_A2_MaxDinsmore
{

    public class Player
    {
        private Brush playerColor;
        public int PlayerNumber;
        string playerName;
        public int Wins { get; }
        public Player(string playerNameChoice, Brush colourChoice)
        {
            playerName = playerNameChoice;
            playerColor = colourChoice;
            Wins = 0;
        }

    }

}
