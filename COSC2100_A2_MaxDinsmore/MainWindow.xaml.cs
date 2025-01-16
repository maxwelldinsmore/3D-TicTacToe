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

        // TODO: Implement this method
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void buttonStartGame_Click(object sender, RoutedEventArgs e)
        {
            bool errorsFound = false;
            int playerCount;
            Int32.TryParse(playerCountTextBox.Text, out playerCount);


            Player player = new Player(textBoxPlayer1.Text, new SolidColorBrush(clrPickerPlayer1.SelectedColor.Value));

            if (playerCount == 2)
            {

            }

            if (errorsFound == false)
            {
                Game newGame = new Game();
                newGame.Closed += NewGame_Closed;
                newGame.Show();
                this.Hide();
            }
        }

        private void NewGame_Closed(object? sender, EventArgs e)
        {
            this.Show();
        }

        private void playerCountTextBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (playerCountTextBox.Value == 2)
            {
                if (gridPlayer3Info != null) gridPlayer3Info.Visibility = Visibility.Hidden;
                if (gridPlayer4Info != null) gridPlayer4Info.Visibility = Visibility.Hidden;
            }
            else if (playerCountTextBox.Value == 3)
            {
                if (gridPlayer3Info != null) gridPlayer3Info.Visibility = Visibility.Visible;
                if (gridPlayer4Info != null) gridPlayer4Info.Visibility = Visibility.Hidden;
            }
            else if (playerCountTextBox.Value == 4)
            {
                if (gridPlayer3Info != null) gridPlayer3Info.Visibility = Visibility.Visible;
                if (gridPlayer4Info != null) gridPlayer4Info.Visibility = Visibility.Visible;
            }
        }
    }
}