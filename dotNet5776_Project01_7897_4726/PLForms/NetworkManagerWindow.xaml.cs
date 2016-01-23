using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PLForms
{
    /// <summary>
    /// Interaction logic for NetworkManagerWindow.xaml
    /// </summary>
    public partial class NetworkManagerWindow : Window
    {
        BE.CurrentPlacing? placing = null;
        BE.User manger;
        public NetworkManagerWindow(BE.User manger)
        {
            InitializeComponent();
            this.manger = manger;
            this.DataContext = placing;
        }

        private void AddBranchButton_Click(object sender, RoutedEventArgs e)
        {
            //open window for creating new branch.
            new NewBranch().ShowDialog();

        }

        private void AddNewBranchManagerButton_Click(object sender, RoutedEventArgs e)
        {
            //open window for adding branch manager user.
        }

        private void AddNewNetworkManagerButton_Click(object sender, RoutedEventArgs e)
        {
            //open window for adding network manager user.
        }

        private void UpdateUserButton_Click(object sender, RoutedEventArgs e)
        {
            //open window for updating the manager's details
        }

        private void DeleteBranchButton_Click(object sender, RoutedEventArgs e)
        {
            //open small window 4 checking which branch is being deleted
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            PrintAllClientsButton.Visibility = Visibility.Hidden;
            UpdateUserButton.Visibility = Visibility.Hidden;
            AddNewNetworkManagerButton.Visibility = Visibility.Visible;
            placing = BE.CurrentPlacing.Action;
            //Bind between placing and AddNewNetworkManagerButton visibilty
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            placing = BE.CurrentPlacing.Info;
            PrintAllClientsButton.Visibility = Visibility.Visible;
            UpdateUserButton.Visibility = Visibility.Hidden;
            AddNewNetworkManagerButton.Visibility = Visibility.Hidden;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            placing = BE.CurrentPlacing.Edit;
            PrintAllClientsButton.Visibility = Visibility.Hidden;
            UpdateUserButton.Visibility = Visibility.Visible;
            AddNewNetworkManagerButton.Visibility = Visibility.Hidden;
        }

        private void PrintAllClientsButton_Click(object sender, RoutedEventArgs e)
        {
            new Print_All_Clients().Show();
        }

        private void PrintBestDishButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrintBestClientButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrintBestBranchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GetProfitDetails_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrintAllDishesButton_Click(object sender, RoutedEventArgs e)
        {
            new Print_All_Dishes().Show();
        }

        private void PrintAllBranchesButton_Click(object sender, RoutedEventArgs e)
        {
            new Print_All_Branches().Show();
        }
    }
}
