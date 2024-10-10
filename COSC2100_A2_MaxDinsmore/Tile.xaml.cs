using System;
using System.Collections.Generic;
using System.Linq;
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
        int bigCircle = 0;
        int mediumCircle = 0;
        int smallCircle = 0;
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
            MessageBox.Show(distanceFromCenter.ToString());
            if (distanceFromCenter >= 27 && distanceFromCenter <= 43)
            {

            } else if (distanceFromCenter >= 46 && distanceFromCenter <= 61)
            {

            } else if (distanceFromCenter >= 65 && distanceFromCenter <= 79)
            {

            }
        }
    }
}
