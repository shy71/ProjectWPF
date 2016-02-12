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
        /// <summary>
        /// constructor
        /// </summary>
        public BranchMangerInterface()
        {
            InitializeComponent();
        }
        /// <summary>
        /// the used construtcor
        /// </summary>
        /// <param name="user"></param>
        public BranchMangerInterface(BE.User user)
            : this()
        {
            branch = BL.FactoryBL.getBL().GetAllBranchs(item => user.ItemID == item.ID).FirstOrDefault();
            if (branch == null)
                throw new Exception("Sorry, you don't have a branch assinged to you at the moment");
            this.user = user;
            details_Loaded(this, null);

        }
        /// <summary>
        /// when the window is fully loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void details_Loaded(object sender, RoutedEventArgs e)
        {
            details.Content = branch.Name;
            details.ToolTip = branch.Address;
        }
        /// <summary>
        /// logs out when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to log out?", "Log out?", MessageBoxButton.YesNo))
            {
                new MainInterface().Show();
                this.Close();
            }
        }
        /// <summary>
        /// opens the editor window when the edit button was clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            new UserEditor(user).ShowDialog();
        }
        /// <summary>
        /// Adds a dish when the AddDish button was clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addDish_Click(object sender, RoutedEventArgs e)
        {
            new DishEditor().ShowDialog();
        }
        /// <summary>
        /// opens the managing part when the button reffering to that is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mangeBtn_Click(object sender, RoutedEventArgs e)
        {
            new BranchEditor(branch, false).ShowDialog();
        }
        /// <summary>
        /// opens the statitics when its button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statisticsBtn_Click(object sender, RoutedEventArgs e)
        {
            new Profit_Details(x => x == branch.ID).ShowDialog();
        }
    }
}
