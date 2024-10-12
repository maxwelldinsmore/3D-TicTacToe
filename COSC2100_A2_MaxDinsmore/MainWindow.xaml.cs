using System.Reflection;
using System.Reflection.Emit;
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
using System.Xml.Linq;
using static COSC2100_A2_MaxDinsmore.Functions;

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
        public Tile[] tiles;
        //public Image[] rings;
        public int playerCount;
        public int[] lastAccessedPiece;
        System.Windows.Controls.Label playerTurnLabel;
        
        
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
            playerCount = 0;
            int.TryParse(this.playerCountTextBox.Text, out playerCount);
            if (playerCount < 2 && playerCount > 4)
            {
                MessageBox.Show("Error invalid amount of players!");
                return;
            }
            players = new Player[playerCount];
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

            tiles = new Tile[gridSize * gridSize];
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {

                    tiles[x + y * gridSize] = new Tile
                    {
                        Margin = new Thickness(x * 168, y * 168, 0, 0),
                        Name = "tile" + (x + y * gridSize),
                        Visibility = Visibility.Visible,
                        Height = 168,
                        Width = 168,
                        VerticalAlignment = 0, // Need alignment otherwise will appear in center of screen
                        HorizontalAlignment = 0,
                    };

                    // Add the tile to the grid
                    grid.Children.Add(tiles[x + y * gridSize]);

                }
            }
            playerTurnLabel = new System.Windows.Controls.Label
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                Margin = new Thickness(700, 20, 0, 0),
                Height = 40,
                Width = 100,
                Content = "Player 1's Turn",
            };
            grid.Children.Add(playerTurnLabel);



            // Threads referenced from
            // https://www.c-sharpcorner.com/UploadFile/1c8574/threads-in-wpf/
            Thread thread = new Thread(new ThreadStart(gameRun));
            thread.SetApartmentState(ApartmentState.STA);

            thread.Start();
        }
   





        private void gameRun()
        {
            
            bool successfulHit = false;
            while (true)
            {

                Thread.Sleep(100);
                for (int y = 0; y < gridSize; y++)
                {
                    for (int x = 0; x < gridSize; x++)
                    {
                        lastAccessedPiece = [x, y, -1];
                        successfulHit = checkTileInfo(tiles[x + y * gridSize]);
                    }
                }

            }
        }

        private void nextTurn()
        {
            if (playersTurn < playerCount)
            {
                playersTurn++;
            } else
            {
                playersTurn = 1;
            }

            try
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    playerTurnLabel.Content = $"Player {playersTurn}'s Turn";
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating UI: {ex.Message}");
            }

        }



        private bool checkTileInfo(Tile tile)
        {
            for (int i = 0; i < 3; i++)
            {
                // Negative 1 means tile was just clicked
                if (tile.getRingValue(i) == -1)
                {
                    lastAccessedPiece[2] = i;
                    // Load copy of circle
                    Dispatcher.Invoke(() =>
                    {
                        
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(players[playersTurn - 1].getRing(i), UriKind.Relative);
                        bitmap.EndInit();

                        Image ring = new Image()
                        {
                            HorizontalAlignment = 0,
                            VerticalAlignment = 0,
                            Source = bitmap,
                            Visibility = Visibility.Visible,
                            Margin = tile.Margin,
                            IsHitTestVisible = false,

                        };
                        double scale = 0;
                        if (i == 0)
                        {
                            scale = 83;
                            ring.Margin = new Thickness(tile.Margin.Left+ 43, tile.Margin.Top + 43 , 0, 0);

                        }
                        else if (i == 1)
                        {
                            scale = 123;
                            ring.Margin = new Thickness(tile.Margin.Left + 23 , tile.Margin.Top + 23, 0, 0);

                        }
                        else
                        {
                            scale = 157.5;
                            ring.Margin = new Thickness(tile.Margin.Left + 5.9, tile.Margin.Top + 5.9, 0, 0);

                        }
                        ring.Height = scale;
                        ring.Width = scale;
                        grid.Children.Add(ring);
                        tile.setRingValue(i, playersTurn);

                    });
                    // tile ring is now marked as clicked by player

                    //Checks if player won
                    int[][] check = checkForWin(tiles, gridSize, lastAccessedPiece);
                    if (check[0][0].ToString() != "-3")
                    {
                        // Message Box For Debugging
                        try
                        {
                            MessageBox.Show("Player " + playersTurn.ToString() + " Won at tiles [" 
                                + check[0][0].ToString() + ", " + check[0][1].ToString() + ", " +  check[0][2].ToString() + "], [" +
                                check[1][0].ToString() + ", " + check[1][1].ToString() + ", " + check[1][2].ToString() + "], [" +
                                check[2][0].ToString() + ", " + check[2][1].ToString() + ", " + check[2][2].ToString() + "]" );

                        } catch
                        {
                            
                            MessageBox.Show(check[0][0].ToString());
                            MessageBox.Show("Error win is false");
                        }
                        
                    }
                    MessageBox.Show(" ["
                                + lastAccessedPiece[0].ToString() + ", " + lastAccessedPiece[1].ToString() + ", " + lastAccessedPiece[2].ToString() + "]");

                    // Changes players Turn
                    nextTurn();
                }
            }
            return false;
        }
        

    }
}