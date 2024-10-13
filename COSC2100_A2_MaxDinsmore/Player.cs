// Player Object Class
// Contains information about players
// Such as their color and associated Ring assets
// based off their number
// used color object can reference when adding to main screen
using System.Drawing;

namespace COSC2100_A2_MaxDinsmore
{

    public class Player
    {
        private System.Windows.Media.Color colour;
        private string[] images;
        public Player(int playerNumber)
        {

            if (playerNumber == 1)
            {
                this.colour = System.Windows.Media.Colors.Red;
                images = ["/Assets/redRing1.png", "/Assets/redRing2.png", "/Assets/redRing3.png"];
            }
            else if (playerNumber == 2)
            {
                this.colour = System.Windows.Media.Colors.Purple;
                images = ["/Assets/purpleRing1.png", "/Assets/purpleRing2.png", "/Assets/purpleRing3.png"];
            }
            else if (playerNumber == 3)
            {
                this.colour = System.Windows.Media.Colors.Blue;
                images = ["/Assets/blueRing1.png", "/Assets/blueRing2.png", "/Assets/blueRing3.png"];
            }
            else
            {
                this.colour = System.Windows.Media.Colors.Green;
                images = ["/Assets/greenRing1.png", "/Assets/greenRing2.png", "/Assets/greenRing3.png"];
            }



        }
        // Accessor method to get the image associated with the player and the size
        public string getRing(int size) 
        { 
        return images[size];
        }

        public System.Windows.Media.Color GetColour()
        {
            return colour;
        }

    }

}
