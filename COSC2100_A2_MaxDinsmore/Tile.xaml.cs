using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace COSC2100_A2_MaxDinsmore
{
    /// <summary>
    /// Interaction logic for Tile.xaml
    /// </summary>
    public partial class Tile : UserControl
    {
       private int[] circles = [0, 0, 0];
        public Tile()
        {
            InitializeComponent();
        }

        private void screenClick(object sender, MouseButtonEventArgs e)
        {
            Point p = Mouse.GetPosition(this);
            circleSize(p);
        }

        private void circleSize(Point point)
        {
            double distanceFromCenter;

            point.X -= 168 / 2;
            point.Y -= 168 / 2;

            distanceFromCenter = Math.Sqrt(Math.Pow(point.X, 2) + Math.Pow(point.Y, 2));
            
            if (distanceFromCenter >= 27 && distanceFromCenter <= 43)
            {
                //MessageBox.Show("Ring 1!");
                if (circles[0] == 0 )
                {
                    circles[0] = -1;
                }
                
                
            } else if (distanceFromCenter >= 46 && distanceFromCenter <= 61)
            {
                //MessageBox.Show("Ring 2!");
                if (circles[1] == 0)
                {
                    circles[1] = -1;
                }
            } else if (distanceFromCenter >= 65 && distanceFromCenter <= 79)
            {
                //MessageBox.Show("Ring 3!");
                if (circles[2] == 0)
                {
                    circles[2] = -1;
                }
            }
            
        }
        public int getRingValue(int size)
        {
            return circles[size];
        }

        public void setRingValue(int circle, int playerNum)
        {
            this.circles[circle] = playerNum;
        }
    }
}
