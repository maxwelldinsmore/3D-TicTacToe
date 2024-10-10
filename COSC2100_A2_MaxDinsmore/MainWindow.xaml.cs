using System.Reflection;
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
        // Default grid size
        int gridSize = 3;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

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