using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSC2100_A2_MaxDinsmore
{

    public class Player
    {
        public string colour = "";
        public string[] images;
        public Player() { }
        public Player(int playerNumber)
        {

            if (playerNumber == 1)
            {
                this.colour = "red";
                images = ["/Assets/redRing1", "/Assets/redRing2", "/Assets/redRing3"];
            }
            else if (playerNumber == 2)
            {
                this.colour = "purple";
                images = ["/Assets/purpleRing1", "/Assets/purpleRing2", "/Assets/purpleRing3"];
            }
            else if (playerNumber == 3)
            {
                this.colour = "blue";
                images = ["/Assets/blueRing1", "/Assets/blueRing2", "/Assets/blueRing3"];
            }
            else
            {
                this.colour = "green";
                images = ["/Assets/greenRing1", "/Assets/greenRing2", "/Assets/greenRing3"];
            }



        }




    }

}
