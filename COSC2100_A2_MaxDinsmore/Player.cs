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
        public Brush PlayerColor { get; set; }
        public int PlayerNumber;
        public string playerName { get; }
        public int Wins { get; }
        public Player(string playerNameChoice, Brush colourChoice)
        {
            playerName = playerNameChoice;
            PlayerColor = colourChoice;
            Wins = 0;
        }

    }

}
