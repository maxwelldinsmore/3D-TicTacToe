
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace COSC2100_A2_MaxDinsmore
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        // Todo: Accomodate size of borders in calculations
        int BoardSize;
        int PlayerCount;
        int CirclesPerTile;
        string BoardBackground = "Brown";
        double TileLength;
        private int currentPlayerTurn; 
        private Random random = new Random();
        const int GameBorderThickness = 2;

        public Game(int gameBoardSize = 3, int gamePlayerCount = 2, int gameCirclesPerTile = 3)
        {
            BoardSize = gameBoardSize;
            PlayerCount = gamePlayerCount;
            CirclesPerTile = gameCirclesPerTile;
            InitializeComponent();
            currentPlayerTurn = random.Next(0, PlayerCount);
            CanvasRepaint();
            int[,] boardInfo = new int[BoardSize, CirclesPerTile];
            

        }

        public void CanvasRepaint()
        {
            // Clear the canvas
            canvasGameBoard.Children.Clear();
            // Brown background
            canvasGameBoard.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(BoardBackground));

            // Calculate the size of each tile
            TileLength = (canvasGameBoard.Height - GameBorderThickness * 2 ) / BoardSize;
            // Draw the grid

                for (int j = 0; j < 2; j++)
            {
                // Vertical Lines
                canvasGameBoard.Children.Add(new Line
                {
                    X1 = TileLength * (j + 1),
                    X2 = TileLength * (j + 1),
                    Y1 = 0,
                    Y2 = canvasGameBoard.Height,
                    Stroke = Brushes.Black,
                    StrokeThickness = GameBorderThickness
                });
                // Horizontal Lines
                canvasGameBoard.Children.Add(new Line
                {
                    Y1 = TileLength * (j + 1),
                    Y2 = TileLength * (j + 1),
                    X1 = 0,
                    X2 = canvasGameBoard.Height,
                    Stroke = Brushes.Black,
                    StrokeThickness = GameBorderThickness
                });
            }
            int tileNumber = 0;
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {   // Top left corner of the tile
                    Point currentTileLocation = new Point(
                        x * TileLength + (GameBorderThickness * x), 
                        y * TileLength + (GameBorderThickness * y)
                        );
                    AddTile(currentTileLocation, tileNumber);
                    tileNumber++;
                }
                   
            }
        }
        public void AddTile(Point tilePoint, int tileNumber)
        {
            Canvas canvasTile = new Canvas
            {
                Width = TileLength,
                Height = TileLength,
                ClipToBounds = true,
                Background = Brushes.White
            };
            Canvas.SetLeft(canvasTile, tilePoint.X);
            Canvas.SetTop(canvasTile, tilePoint.Y);

            for (int i = 0; i < CirclesPerTile; i++)
            {
                // Create a new circlePiece
                Ellipse circlePiece = new Ellipse
                {
                    Width = TileLength / (i + 1),
                    Height = TileLength / (i + 1),
                    Stroke = Brushes.Black,
                    StrokeThickness = 10,
                    Name = "piece_" + tileNumber + "_" + i,
                    Uid = "0" // 0 is empty, 1 is player 1, 2 is player 2, etc.
                };
                circlePiece.MouseDown += PieceClick;
                // Set the position of the circlePiece
                Canvas.SetLeft(circlePiece, tilePoint.X + (TileLength * (i / 3) ) );
                Canvas.SetTop(circlePiece, tilePoint.Y + (TileLength * (i / 3) ) );

                // Add the circlePiece to the canvas
                canvasTile.Children.Add(circlePiece);
            }
            canvasGameBoard.Children.Add(canvasTile);

        }

        private void PieceClick(object sender, MouseButtonEventArgs e)
        {
            Ellipse clickedPiece = (Ellipse)sender;
            Debug.WriteLine("Tile " + clickedPiece.Name.Split("_")[1]
                + "\nPiece " + clickedPiece.Name.Split("_")[2]
                );
            // Tiles are top down, left to right, and pieces listed from biggest to smallest
            if (clickedPiece.Uid == "0")
            {
                clickedPiece.Uid = (currentPlayerTurn + 1).ToString();
                clickedPiece.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red"));
                currentPlayerTurn = (currentPlayerTurn + 1) % PlayerCount;
            }
        }

        private void UserClick(object sender, MouseButtonEventArgs e)
        {
            Point userClick = e.GetPosition(canvasGameBoard);

        }

        ///
        /// BELOW IS THE ORIGINAL CODE THAT MAY BE REUSED AS APPROPRIATE
        ///

        ///// <summary>
        ///// Starts game and generates neccesary assets
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void buttonStartGame_Click(object sender, RoutedEventArgs e)
        //{
        //    playerCount = 0;
        //    int.TryParse(this.playerCountTextBox.Text, out playerCount);
        //    if (playerCount < 2 || playerCount > 4)
        //    {
        //        MessageBox.Show("Error invalid amount of players!");
        //        return;
        //    }
        //    players = new Player[playerCount];
        //    for (int i = 1; i < playerCount + 1; i++)
        //    {
        //        players[i - 1] = new Player(i);
        //    }

        //    hideMenuItems();
        //    if (this._4SizeRadioButton.IsChecked == true)
        //    {
        //        gridSize = 4;
        //    }
        //    else
        //    {
        //        gridSize = 3;
        //    }
        //    this.Width = gridSize * 168 + 400;
        //    this.Height = gridSize * 168 + 40;

        //    // Thickness referenced from stack overflow
        //    // https://stackoverflow.com/questions/1003772/setting-margin-properties-in-code

        //    // Generates grid based off tile object class
        //    tiles = new Tile[gridSize, gridSize];
        //    for (int y = 0; y < gridSize; y++)
        //    {
        //        for (int x = 0; x < gridSize; x++)
        //        {

        //            tiles[x, y] = new Tile
        //            {
        //                Margin = new Thickness(x * 168, y * 168, 0, 0),
        //                Name = "tile" + (x + y * gridSize),
        //                Visibility = Visibility.Visible,
        //                Height = 168,
        //                Width = 168,
        //                VerticalAlignment = 0, // Need alignment otherwise will appear in center of screen
        //                HorizontalAlignment = 0,
        //            };

        //            // Add the tile to the grid
        //            grid.Children.Add(tiles[x, y]);

        //        }
        //    }
        //    playerTurnLabel = new System.Windows.Controls.Label
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        Margin = new Thickness(700, 20, 0, 0),
        //        Height = 40,
        //        Width = 100,
        //        Content = "Player 1's Turn",
        //        BorderBrush = new SolidColorBrush(Colors.Maroon),
        //        BorderThickness = new Thickness(4, 4, 4, 4)
        //    };
        //    grid.Children.Add(playerTurnLabel);



        //    // Threads referenced from
        //    // https://www.c-sharpcorner.com/UploadFile/1c8574/threads-in-wpf/
        //    Thread thread = new Thread(new ThreadStart(gameRun));
        //    thread.SetApartmentState(ApartmentState.STA);

        //    thread.Start();
        //}





        ///// <summary>
        ///// Game loop starts after start button click
        ///// runs until game ends
        ///// </summary>
        //private void gameRun()
        //{

        //    bool successfulHit;
        //    while (!gameFinished)
        //    {

        //        Thread.Sleep(100);
        //        for (int y = 0; y < gridSize; y++)
        //        {
        //            for (int x = 0; x < gridSize; x++)
        //            {
        //                lastAccessedPiece = [x, y, -1];
        //                successfulHit = checkTileInfo(tiles[x, y]);
        //            }
        //        }

        //    }



        //}

        ///// <summary>
        ///// Changes players turn and changes label appriopriately
        ///// </summary>
        //private void nextTurn()
        //{
        //    if (playersTurn < playerCount)
        //    {
        //        playersTurn++;
        //    }
        //    else
        //    {
        //        playersTurn = 1;
        //    }

        //    try
        //    {
        //        Dispatcher.BeginInvoke(new Action(() =>
        //        {
        //            playerTurnLabel.Content = $"Player {playersTurn}'s Turn";
        //            playerTurnLabel.BorderBrush = new SolidColorBrush(players[playersTurn - 1].GetColour());
        //            playerTurnLabel.BorderThickness = new Thickness(4, 4, 4, 4);
        //        }));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error updating UI: {ex.Message}");
        //    }

        //}



        //private bool checkTileInfo(Tile tile)
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        // Negative 1 means tile was just clicked
        //        // and if true loads in a ring representing a players click
        //        if (tile.getRingValue(i) == -1)
        //        {
        //            lastAccessedPiece[2] = i;
        //            // Load copy of circlePiece
        //            Dispatcher.Invoke(() =>
        //            {

        //                BitmapImage bitmap = new BitmapImage();
        //                bitmap.BeginInit();
        //                bitmap.UriSource = new Uri(players[playersTurn - 1].getRing(i), UriKind.Relative);
        //                bitmap.EndInit();

        //                Image ring = new Image()
        //                {
        //                    HorizontalAlignment = 0,
        //                    VerticalAlignment = 0,
        //                    Source = bitmap,
        //                    Visibility = Visibility.Visible,
        //                    Margin = new Thickness(tile.Margin.Left + 0.7, tile.Margin.Top + 0.7, 0, 0),
        //                    IsHitTestVisible = false,
        //                    Height = 168.1,
        //                    Width = 168.1,
        //                };


        //                grid.Children.Add(ring);
        //                tile.setRingValue(i, playersTurn);

        //            });
        //            // tile ring is now marked as clicked by player

        //            //Checks if player won
        //            int[,] check = checkForWin(tiles, gridSize, lastAccessedPiece);
        //            if (check[0, 0].ToString() != "-3")
        //            {

        //                MessageBox.Show("Player " + playersTurn.ToString() + " Won!");
        //                gameFinished = true;
        //                // Message Box For Debugging
        //                //try
        //                //{
        //                //    MessageBox.Show("Player " + playersTurn.ToString() + " Won at tiles [" 
        //                //        + check[0][0].ToString() + ", " + check[0][1].ToString() + ", " +  check[0][2].ToString() + "], [" +
        //                //        check[1][0].ToString() + ", " + check[1][1].ToString() + ", " + check[1][2].ToString() + "], [" +
        //                //        check[2][0].ToString() + ", " + check[2][1].ToString() + ", " + check[2][2].ToString() + "]" );
        //                //} catch
        //                //{
        //                //    MessageBox.Show(check[0][0].ToString());
        //                //    MessageBox.Show("Error win is false");
        //                //}

        //            }

        //            // Changes players Turn
        //            nextTurn();
        //        }
        //    }
        //    return false;
        //}
    }
}
