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

            //rings = new Image[3*playerCount];
            //for (int z = 0; z < playerCount; z++)
            //{
            //    // Image source is referenced from miscrosoft documentation
            //    // Need to convert to get image source
            //    // https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.image.source?view=windowsdesktop-8.0

            //    for (int y=0;y<3;y++)
            //    {
            //        BitmapImage bitmap = new BitmapImage();
            //        bitmap.BeginInit();
            //        bitmap.UriSource = new Uri(players[playersTurn - 1].getRing(z), UriKind.Relative);
            //        bitmap.EndInit();


            //        rings[y + z * playerCount] = new Image()
            //        {
            //            HorizontalAlignment = 0,
            //            VerticalAlignment = 0,
            //            Source = bitmap,
            //            Width = 168,
            //            Height = 168,
            //            Visibility = Visibility.Hidden

            //        };
            ////        grid.Children.Add(rings[y + z * playerCount]);
            //    }
                

                
            //}

            // Thickness referenced from stack overflow
            // https://stackoverflow.com/questions/1003772/setting-margin-properties-in-code

            tiles = new Tile[gridSize * gridSize];
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
                for (int gridCount = 0; gridCount < gridSize * gridSize; gridCount++)
                {
                    successfulHit = checkTileInfo(tiles[gridCount]);
                    
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
                if (tile.circles[i] == -1)
                {
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
                        int scale = 0;
                        if (i == 0)
                        {
                            scale = 78;
                            ring.Margin = new Thickness(tile.Margin.Left+ 50, tile.Margin.Top + 50 , 0, 0);

                        }
                        else if (i == 1)
                        {
                            scale = 120;
                            ring.Margin = new Thickness(tile.Margin.Left + 20 , tile.Margin.Top + 20, 0, 0);

                        }
                        else
                        {
                            scale = 158;
                            ring.Margin = new Thickness(tile.Margin.Left + 5, tile.Margin.Top + 5, 0, 0);

                        }
                        ring.Height = scale;
                        ring.Width = scale;
                        grid.Children.Add(ring);
                        //grid.Children.Remove(tile);
                        //MessageBox.Show(tile.circles[i].ToString());
                        tile.circles[i] = playersTurn;
                        //MessageBox.Show(tile.circles[i].ToString());

                    });
                    

                    // tile ring is now marked as clicked by player
                    
                    nextTurn();
                }
            }
            return false;
        }
        

    }
}