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
        public int playersTurn = 1;
        Player[] players;
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
            int playerCount = 0;
            int.TryParse(this.playerCountTextBox.Text, out playerCount);
            if (playerCount < 2 && playerCount > 4)
            {
                MessageBox.Show("Error invalid amount of players!");
                return;
            }
            Player[] players = new Player[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                players[i] = new Player(i);
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

            Tile[] tiles = new Tile[gridSize * gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {

                    tiles[j + gridSize * i] = new Tile
                    {
                        Margin = new Thickness(j * 168, i * 168, 0, 0),
                        Name = "tile" + (j + gridSize * i),
                        Visibility = Visibility.Visible,
                        Height = 168,
                        Width = 168,
                        VerticalAlignment = 0, // Need alignment otherwise will appear in center of screen
                        HorizontalAlignment = 0,
                    };

                    // Add the tile to the grid
                    grid.Children.Add(tiles[j + gridSize * i]);

                }
            }
            Label playerTurnLabel = new Label
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                Margin = new Thickness(700, 20, 0, 0),
                Height = 40,
                Width = 100,
                Content = "Player 1's Turn",
            };
            grid.Children.Add(playerTurnLabel);


            //gameRun(tiles);
        }

        // Need a way to check if a piece has been updated
        private void gameRun(Tile[] tiles)
        {
            //while (true)
            //{
            //    System.Threading.Thread.Sleep(10);
            //    for (int gridCount  = 0; gridCount < gridSize * gridSize; gridCount++)
            //    {
            //        checkTileInfo(tiles[gridCount]);\\
            //    }
            //    
            //}
        }

        private Boolean checkTileInfo(Tile tile)
        {
            for (int i = 0; i <= 3; i++)
            {
                if (tile.circles[i] == -1)
                {
                    // Image source is referenced from miscrosoft documentation
                    // Need to convert to other types
                    Image ring = new Image()
                    {
                        // Source = players[playersTurn].images[1],
                        Width = 168,
                        Height = 168,


                    };
                    grid.Children.Add(ring);
                    
                }
            }
            return false;
        }
        

    }
}