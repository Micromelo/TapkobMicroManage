using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Forms;
using Tapkob.Services;
using System.Collections.Generic;
using Tapkob.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Windows.Input;
using Tapkob.ViewModel;

namespace Tapkob
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<TaskModel> Tasks { get; private set; } = new List<TaskModel>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM();
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

        #region Windows Events
        private void Grid_TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void StackPanel_SideBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        #endregion

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
