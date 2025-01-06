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
        private Color Colour;
        public int PlayerNumber;
        public int Wins { get; }
        public Player(int playerNumber, Color colourChoice)
        {
           PlayerNumber = playerNumber;
           Colour = System.Windows.Media.Color.FromArgb(colourChoice.A, colourChoice.R, colourChoice.G, colourChoice.B);
           Wins = 0;
        }

        public Color GetColour()
        {
            return Colour;
        }

    }

}
