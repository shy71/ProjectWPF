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
    /// Interaction logic for BranchMangerInterface.xaml
    /// </summary>
    public partial class BranchMangerInterface : Window
    {
        BE.Branch branch;
        BE.User user;
        public BranchMangerInterface()
        {
            InitializeComponent();
        }
        public BranchMangerInterface(BE.User user)
            : this()
        {
            branch = BL.FactoryBL.getBL().GetAllBranchs(item => user.ItemID == item.ID).FirstOrDefault();
            if (branch == null)
                throw new Exception("Sorry, you dont have a branch assinged to you at the moment");
            this.user = user;
            details_Loaded(this, null);

        }

        private void details_Loaded(object sender, RoutedEventArgs e)
        {
            details.Content = branch.Name;
            details.ToolTip = branch.Address;
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
            new UserEditor(user).ShowDialog();
        }

        private void addDish_Click(object sender, RoutedEventArgs e)
        {
            new DishEditor().ShowDialog();
        }

        private void mangeBtn_Click(object sender, RoutedEventArgs e)
        {
            new BranchEditor(branch, false).ShowDialog();
        }

        private void staticBtn_Click(object sender, RoutedEventArgs e)
        {
            new Profit_Details(x => x == branch.ID).ShowDialog();
        }
    }
}
