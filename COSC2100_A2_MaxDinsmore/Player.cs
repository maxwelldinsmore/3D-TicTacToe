using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSC2100_A2_MaxDinsmore
{

    public class Player
    {
        private string colour = "";
        private string[] images;
        public Player() { }
        public Player(int playerNumber)
        {

            if (playerNumber == 1)
            {
                this.colour = "red";
                images = ["/Assets/redRing1.png", "/Assets/redRing2.png", "/Assets/redRing3.png"];
            }
            else if (playerNumber == 2)
            {
                this.colour = "purple";
                images = ["/Assets/purpleRing1.png", "/Assets/purpleRing2.png", "/Assets/purpleRing3.png"];
            }
            else if (playerNumber == 3)
            {
                this.colour = "blue";
                images = ["/Assets/blueRing1.png", "/Assets/blueRing2.png", "/Assets/blueRing3.png"];
            }
            else
            {
                this.colour = "green";
                images = ["/Assets/greenRing1.png", "/Assets/greenRing2.png", "/Assets/greenRing3.png"];
            }



        }

        public string getRing(int size) 
        { 
        return images[size];
        }



    }

}
