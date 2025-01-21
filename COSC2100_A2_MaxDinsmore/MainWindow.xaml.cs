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
            Player[] players = new Player[4];
            bool errorsFound = false;
            int playerCount = (int)playerCountTextBox.Value;

            // First Player
            if (textBoxPlayer1.Text != null)
            {
                players[0] = new Player(textBoxPlayer1.Text, new SolidColorBrush(clrPickerPlayer1.SelectedColor.Value));
            }
            else
            {
                MessageBox.Show("Please enter a name for Player 1");
                errorsFound = true;
            }
            // Second Player
            if (textBoxPlayer2.Text != null)
            {
                players[1] = new Player(textBoxPlayer2.Text, new SolidColorBrush(clrPickerPlayer2.SelectedColor.Value));
            }
            else
            {
                MessageBox.Show("Please enter a name for Player 2");
                errorsFound = true;
            }

            // If third Player
            if (playerCount > 2 && textBoxPlayer3.Text != null)
            {
                players[2] = new Player(textBoxPlayer3.Text, new SolidColorBrush(clrPickerPlayer3.SelectedColor.Value));
            }
            else
            {
                MessageBox.Show("Please enter a name for Player 3");
                errorsFound = true;
            }

            // If fourth Player
            if (playerCount == 4 && textBoxPlayer4.Text != null)
            {
                players[3] = new Player(textBoxPlayer4.Text, new SolidColorBrush(clrPickerPlayer4.SelectedColor.Value));

            }
            else
            {
                MessageBox.Show("Please enter a name for Player 4");
                errorsFound = true;
            }
            if (errorsFound == false)
            {
                Game newGame = new Game(players);
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