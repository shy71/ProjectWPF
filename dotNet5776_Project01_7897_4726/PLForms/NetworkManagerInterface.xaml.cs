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
    public partial class NetworkManagerInterface : Window
    {
        BE.CurrentPlacing? placing = null;
        BE.User manger;
        public NetworkManagerInterface(BE.User manger)
        {
            InitializeComponent();
            this.manger = manger;
            this.DataContext = placing;
        }

        private void AddBranchButton_Click(object sender, RoutedEventArgs e)
        {
            new BranchEditor().ShowDialog();
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
            new Delete_Branch().Show();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            new MainInterface().Show();
            this.Close();
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDB.Visibility = Visibility.Hidden;
            UpdateUserButton.Visibility = Visibility.Hidden;
            AddNewNetworkManagerButton.Visibility = Visibility.Visible;
            placing = BE.CurrentPlacing.Action;
            //Bind between placing and AddNewNetworkManagerButton visibilty
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            placing = BE.CurrentPlacing.Info;
            PrintDB.Visibility = Visibility.Visible;
            UpdateUserButton.Visibility = Visibility.Hidden;
            AddNewNetworkManagerButton.Visibility = Visibility.Hidden;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            placing = BE.CurrentPlacing.Edit;
            PrintDB.Visibility = Visibility.Hidden;
            UpdateUserButton.Visibility = Visibility.Visible;
            AddNewNetworkManagerButton.Visibility = Visibility.Hidden;
        }

        #region Information Functions


        private void PrintBestDishButton_Click(object sender, RoutedEventArgs e)
        {
            new ShowBest("Dish").Show();
        }

        private void PrintBestClientButton_Click(object sender, RoutedEventArgs e)
        {
            new ShowBest("Client").Show();
        }

        private void PrintBestBranchButton_Click(object sender, RoutedEventArgs e)
        {
            new ShowBest("Branch").Show();
        }

        private void GetProfitDetails_Click(object sender, RoutedEventArgs e)
        {
        }

        /*
        private void PrintAllClientsButton_Click(object sender, RoutedEventArgs e)
        {
            new Print_All_Clients().Show();
        }
        private void PrintAllDishesButton_Click(object sender, RoutedEventArgs e)
        {
            new Print_All_Dishes().Show();
        }

        private void PrintAllBranchesButton_Click(object sender, RoutedEventArgs e)
        {
            new Print_All_Branches().Show();
        }*/
        private void PrintDB_Click(object sender, RoutedEventArgs e)
        {
            //new PrintSegments().ShowDialog();
            new Print_Data_Base().Show();
        }
        #endregion

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            new Search_Bing().Show();
        }

    }
}
