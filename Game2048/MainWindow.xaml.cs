using Game2048.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Game2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel viewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
            this.Width = 400;
            this.Height = 450;
            this.ResizeMode = ResizeMode.NoResize;
            this.KeyDown += Window_KeyDown;
            
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                viewModel.Right();

            }
            else if (e.Key == Key.Left)
            {

                viewModel.Left();
            }
            else if (e.Key == Key.Up)
            {

                viewModel.Up();
            }
            else if (e.Key == Key.Down)
            {
                viewModel.Down();

            }
        }
    }
}
