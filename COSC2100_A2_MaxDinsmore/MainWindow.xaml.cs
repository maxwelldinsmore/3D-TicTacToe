using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        // variables
        int gridSize;
        public MainWindow()
        {
            InitializeComponent();
        }

        // either hides all main menu items or makes them visible
        private void hideMenuItems()
        {
            if (buttonStartGame.Visibility == Visibility.Visible)
            {
                this._3SizeRadioButton.Visibility = Visibility.Hidden;
                this._4SizeRadioButton.Visibility = Visibility.Hidden;
                this.playerCountTextBox.Visibility = Visibility.Hidden;
                this.buttonStartGame.Visibility = Visibility.Hidden;
                this.titleLabel.Visibility = Visibility.Hidden;
                this.playerLabel.Visibility = Visibility.Hidden;
                this.gridSizeLabel.Visibility = Visibility.Hidden;
            } else
            {
                this._3SizeRadioButton.Visibility = Visibility.Visible;
                this._4SizeRadioButton.Visibility = Visibility.Visible;
                this.playerCountTextBox.Visibility = Visibility.Visible;
                this.buttonStartGame.Visibility = Visibility.Visible;
                this.titleLabel.Visibility = Visibility.Visible;
                this.playerLabel.Visibility = Visibility.Visible;
                this.gridSizeLabel.Visibility = Visibility.Visible;
            }
            
        }

        private void buttonStartGame_Click(object sender, RoutedEventArgs e)
        {
            int players = 0;
            int.TryParse(this.playerCountTextBox.Text, out players);
            if (players < 2 && players > 4)
            {
                MessageBox.Show("Error invalid amount of players!");
                return;
            }


            hideMenuItems();
            if (this._4SizeRadioButton.IsChecked == true)
            {
                gridSize = 4;
            }
            else {
                gridSize = 3;
            }
            this.Width = gridSize * 168 + 400;
            this.Height = gridSize * 168 + 40;

            

            // Thickness referenced from stack overflow
            // https://stackoverflow.com/questions/1003772/setting-margin-properties-in-code
            
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {

                    Tile tile = new Tile
                    {
                        Margin = new Thickness(j * 168, i * 168, 0, 0),
                        Name = "tile" + (i),
                        Visibility = Visibility.Visible,
                        Height = 168,
                        Width = 168,
                        VerticalAlignment = 0, // Need alignment otherwise will appear in center of screen
                        HorizontalAlignment = 0,
                    };

                    // Add the tile to the grid
                    grid.Children.Add(tile);

                }
            }
        }



    }
}