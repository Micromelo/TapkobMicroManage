using System.Windows;

namespace Tapkob
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

        #region Menu Item Buttons
        private void MenuItemHome_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItemQuests_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItemMarket_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItemCrafting_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItemAmmo_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItemSettings_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItemEscape_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Windows State Buttons
        private void BtnMinimise_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Minimized;
            }
        }

        private void BtnMaximise_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}
