using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        Tile[,] tiles;
        int playerCount;
        int[] lastAccessedPiece;
        bool gameFinished = false;
        Label playerTurnLabel;
        
        
        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            // Template Code Below
            //if (e.NewValue.HasValue)
            //{
            //    Color selectedColor = e.NewValue.Value;
            //    this.Background = new SolidColorBrush(selectedColor);
            //}
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}