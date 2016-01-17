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
        BE.User manger;
        public NetworkManagerWindow(BE.User manger)
        {
            InitializeComponent();
            this.manger = manger;
        }

        private void AddBranchButton_Click(object sender, RoutedEventArgs e)
        {
            //open window for creating new branch.
            MessageBox.Show("The Branch has been added.", "Information");
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
    }
}
