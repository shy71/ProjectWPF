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
        BE.User manger;
        public NetworkManagerInterface(BE.User manger)
        {
            InitializeComponent();
            this.manger = manger;
            NetWorkCombo.ItemsSource = BL.FactoryBL.getBL().GetAllUsers(item => item.Type == BE.UserType.NetworkManger && item.UserName != manger.UserName);
            NetWorkCombo.SelectedValuePath = "UserName";
            BranchMangerCombo.ItemsSource = BL.FactoryBL.getBL().GetAllUsers(item => item.Type == BE.UserType.BranchManger && item.ItemID == 0);
            BranchMangerCombo.SelectedValuePath = "UserName";
            UserCombo.ItemsSource = from item in BL.FactoryBL.getBL().GetAllUsers(item => item.Type == BE.UserType.Client)
                                    where !BL.FactoryBL.getBL().GetAllOrders(item2 => item2.ClientID == item.ItemID && (item2.Delivered == false && item2.Date != DateTime.MinValue)).Any()
                                    select item;
            UserCombo.SelectedValuePath = "UserName";
            DishCombo.ItemsSource = BL.FactoryBL.getBL().GetAllDishs();
            DishCombo.SelectedValuePath = "ID";
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to log out?", "Log out?", MessageBoxButton.YesNo))
            {
                new MainInterface().Show();
                this.Close();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            new UserEditor(manger).ShowDialog();
        }

        private void Static_Click(object sender, RoutedEventArgs e)
        {
            new Profit_Details().Show();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to Quit?", "Log out?", MessageBoxButton.YesNo))
            {
                BL.FactoryBL.getBL().RemoveUser(manger);
                this.Hide();
                new MainInterface().Show();
                this.Close();   
            }


        }

        private void AddNetworkManger_Click(object sender, RoutedEventArgs e)
        {
            new UserEditor(BE.UserType.NetworkManger).ShowDialog();
            NetWorkCombo.ItemsSource = BL.FactoryBL.getBL().GetAllUsers(item => item.Type == BE.UserType.NetworkManger && item.UserName != manger.UserName);

        }

        private void DeleteBranch_Click(object sender, RoutedEventArgs e)
        {
            new BranchPicker(delegate(int branchID)
                {
                    var temp = BL.FactoryBL.getBL().GetAllUsers(item => item.ItemID == branchID).First();
                    temp.ItemID = 0;
                    BL.FactoryBL.getBL().UpdateUser(temp);
                    BL.FactoryBL.getBL().DeleteBranch(branchID);
                }, "Delete").Show();
        }
        private void EditBranch(object sender, RoutedEventArgs e)
        {
            new BranchPicker(delegate(int branchID)
                {
                    new BranchEditor(BL.FactoryBL.getBL().GetAllBranchs(item => item.ID == branchID).First(), true).ShowDialog();
                }, "Edit"
                ).Show();
            BranchMangerCombo.ItemsSource = BL.FactoryBL.getBL().GetAllUsers(item => item.Type == BE.UserType.BranchManger && item.ItemID == 0);
        }
        private void NetWorkCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NetWorkCombo.SelectedItem == null)
                return;
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to delete this Network Manger?", "Delete Conformtion", MessageBoxButton.YesNo))
                BL.FactoryBL.getBL().RemoveUser(NetWorkCombo.SelectedValue as string);
            NetWorkCombo.ItemsSource = BL.FactoryBL.getBL().GetAllUsers(item => item.Type == BE.UserType.NetworkManger && item.UserName != manger.UserName);
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            new BranchEditor().Show();
            BranchMangerCombo.ItemsSource = BL.FactoryBL.getBL().GetAllUsers(item => item.Type == BE.UserType.BranchManger && item.ItemID == 0);
        }

        private void BranchMangerCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BranchMangerCombo.SelectedItem == null)
                return;
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to delete this Branch Manger?", "Delete Conformtion", MessageBoxButton.YesNo))
                BL.FactoryBL.getBL().RemoveUser(BranchMangerCombo.SelectedValue as string);
            BranchMangerCombo.ItemsSource = BL.FactoryBL.getBL().GetAllUsers(item => item.Type == BE.UserType.BranchManger && item.ItemID == 0);
        }

        private void BranchStatic_Click(object sender, RoutedEventArgs e)
        {
            new BranchPicker(delegate(int branchID)
                {
                    new Profit_Details(x => x == branchID).Show();
                }, "See Statics").Show();
        }

        private void DishCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (DishCombo.SelectedItem == null)
                    return;
                if (MessageBoxResult.Yes == MessageBox.Show("Do you want to delete this order?", "Delete Conformtion", MessageBoxButton.YesNo))
                    BL.FactoryBL.getBL().DeleteDish(int.Parse(DishCombo.SelectedValue.ToString()));
                else
                    new DishEditor(BL.FactoryBL.getBL().GetAllDishs(item => item.ID == int.Parse(DishCombo.SelectedValue.ToString())).First()).ShowDialog();
                DishCombo.ItemsSource = BL.FactoryBL.getBL().GetAllDishs(item=>item.Active);
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void AddDish(object sender, RoutedEventArgs e)
        {
            new DishEditor().ShowDialog();
            DishCombo.ItemsSource = BL.FactoryBL.getBL().GetAllDishs();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new The_Best_Things().Show();
        }

        private void DataBasePrintClickBtn(object sender, RoutedEventArgs e)
        {
            new Print_Data_Base().Show();
        }

        private void UserCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserCombo.SelectedItem == null)
                return;
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to delete this Client?", "Delete Conformtion", MessageBoxButton.YesNo))
                BL.FactoryBL.getBL().RemoveUser(UserCombo.SelectedValue.ToString(),true);
            UserCombo.ItemsSource = from item in BL.FactoryBL.getBL().GetAllUsers(item => item.Type == BE.UserType.Client)
                                    where !BL.FactoryBL.getBL().GetAllOrders(item2 => item2.ClientID == item.ItemID && (item2.Delivered == false && item2.Date != DateTime.MinValue)).Any()
                                    select item;
        }
    }
}
