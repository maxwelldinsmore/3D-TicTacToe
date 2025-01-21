
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
        // Todo: declare constants
        // Constants for the game board, some are decided when the game is started 
        // based on user choice in the main window

        /// <summary>
        /// Thickness of the game border grid
        /// </summary>
        const int GameBorderThickness = 2;
        /// <summary>
        /// Thickness of the game pieces in pixels
        /// </summary>
        const int PieceThickness = 10;
        /// <summary>
        /// Size of the game board
        /// </summary>
        int BoardSize;
        /// <summary>
        /// Number of players in the game
        /// </summary>
        int PlayerCount;
        /// <summary>
        /// Number of circles per tile
        /// </summary>
        int CirclesPerTile;
        /// <summary>
        /// Background color of the game board
        /// </summary>
        string BoardBackground = "Brown";
        /// <summary>
        /// Length of individual tiles which are using canvases
        /// </summary>
        double TileLength;
        /// <summary>
        /// 
        /// </summary>
        private Player[] players;

        ///
        /// Variables for the game
        /// 

        /// <summary>
        /// Current Players Turn
        /// </summary>
        private int currentPlayerTurn;
        /// <summary>
        /// Random number generator for deciding who goes first
        /// </summary>
        private Random random = new Random();
        /// <summary>
        /// Scoring info for the game used to store and check for wins
        /// </summary>
        private int[,,] ScoringInfo;

        public Game(Player[] playerInfo, int gameBoardSize = 3, int gameCirclesPerTile = 3)
        {
            BoardSize = gameBoardSize;
            PlayerCount = playerInfo.Length;
            CirclesPerTile = gameCirclesPerTile;
            InitializeComponent();
            currentPlayerTurn = random.Next(0, PlayerCount);
            LoadGameBoard();
            int[,] boardInfo = new int[BoardSize, CirclesPerTile];
            players = playerInfo;

        }

        public void LoadGameBoard()
        {
            ScoringInfo = new int[BoardSize, BoardSize, CirclesPerTile];
            ReloadGameBoard(ScoringInfo);
        }

        public void ReloadGameBoard(int[,,] ScoringInfo)
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
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {   // Top left corner of the tile
                    Point currentTileLocation = new Point(
                        x * TileLength + (GameBorderThickness * x), 
                        y * TileLength + (GameBorderThickness * y)
                        );
                    AddTile(currentTileLocation, x,y);
                }
                   
            }
        }
        public void AddTile(Point tilePoint, int tileNumberX, int tileNumberY)
        {
            // ADD GAP BETWEEN PIECES TO GET PROPER SIZING
            Canvas canvasTile = new Canvas
            {
                Width = TileLength,
                Height = TileLength,
                ClipToBounds = true,
                Background = Brushes.White
            };
            Canvas.SetLeft(canvasTile, tilePoint.X);
            Canvas.SetTop(canvasTile, tilePoint.Y);
            double gapBetweenPieces = (TileLength - PieceThickness * CirclesPerTile) / (CirclesPerTile + 1);

            double pieceSize = TileLength;
            // Next smallest pieces width is 
            for (int i = 0; i < CirclesPerTile; i++)
            {
                // Create a new circlePiece
                Ellipse circlePiece = new Ellipse // Fix this
                {
                    Width = pieceSize,
                    Height = pieceSize,
                    Stroke = Brushes.Black,
                    StrokeThickness = PieceThickness,
                    Name = "piece_" + tileNumberX + "_" + tileNumberY + "_" + i,
                    Uid = "0" // 0 is empty, 1 is player 1, 2 is player 2, etc.
                };
                pieceSize -= gapBetweenPieces * 2;

                circlePiece.MouseDown += PieceClick;
                // Set the position of the circlePiece
                Canvas.SetLeft(circlePiece, gapBetweenPieces * (i));
                Canvas.SetTop(circlePiece, gapBetweenPieces * (i));

                // Add the circlePiece to the canvas
                canvasTile.Children.Add(circlePiece);
            }
            canvasGameBoard.Children.Add(canvasTile);
        }

        /// <summary>
        /// Controls the click event for the game pieces
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PieceClick(object sender, MouseButtonEventArgs e)
        {
            Ellipse clickedPiece = (Ellipse)sender;
            Debug.WriteLine("TileX " + clickedPiece.Name.Split("_")[1] 
                + "\nTileY " + clickedPiece.Name.Split("_")[2]
                + "\nPiece " + clickedPiece.Name.Split("_")[3]
                );
            int tilePosX = int.Parse(clickedPiece.Name.Split("_")[1]);
            int tilePosY = int.Parse(clickedPiece.Name.Split("_")[2]);
            int piecePos = int.Parse(clickedPiece.Name.Split("_")[3]);
            // Tiles are top down, left to right, and pieces listed from biggest to smallest
            if (clickedPiece.Uid == "0")
            {
                clickedPiece.Uid = (currentPlayerTurn + 1).ToString();
                clickedPiece.Stroke = players[currentPlayerTurn].PlayerColor;
                currentPlayerTurn = (currentPlayerTurn + 1) % PlayerCount;
                ScoringInfo[tilePosX, tilePosY, piecePos] = currentPlayerTurn;
                //CheckForWin(ScoringInfo);
                NextPlayerTurn();
            }


        }

        private void NextPlayerTurn()
        {
            if (currentPlayerTurn + 1 == PlayerCount)
            {
                currentPlayerTurn = 0;
            }
            else
            {
                currentPlayerTurn++;
            }
        }

        public void CheckForWin(int[,,] ScoringInfo)
        {
            // Check for win
            throw new NotImplementedException();
        }

        /// <summary>
        /// Used to resize game board when window is resized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double value;

            Double.TryParse(Game.ActualHeightProperty.ToString(), out value);
            canvasGameBoard.Height = value;
            canvasGameBoard.Width = canvasGameBoard.Height;
            ReloadGameBoard(ScoringInfo);
        }
    }
}
