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
        public BranchMangerInterface(BE.User user):this()
        {

            branch = BL.FactoryBL.getBL().GetAllBranchs(item => user.ItemID == item.ID).First();
            if(branch==null)
            {
                MessageBox.Show("Sorry, you dont have a branch assinged to you at the moment");
                new MainInterface().Show();
                this.Close();
            }
            this.user = user;
            deitles_Loaded(this, null);
        }

        private void deitles_Loaded(object sender, RoutedEventArgs e)
        {
            deitles.Content = branch.Name;
            deitles.ToolTip = branch.Address;
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
    }
}
