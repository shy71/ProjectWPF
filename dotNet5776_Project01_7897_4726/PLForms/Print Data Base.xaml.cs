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
    /// Interaction logic for Print_Data_Base.xaml
    /// </summary>
    public partial class Print_Data_Base : Window
    {
        internal void AddText()
        {
            DataBase.Text = "";
            BL.IBL myBL = BL.FactoryBL.getBL();
            if (collapseDishes.IsChecked == false)
            {
                DataBase.Text += "\nDishes:\n";
                IEnumerable<BE.Dish> DishList = myBL.GetAllDishs();
                foreach (BE.Dish item in DishList)
                {
                    DataBase.Text += (item.ToString() + "\n");
                }
            }
            if (collapseBranches.IsChecked == false)
            {
                DataBase.Text += "\nBranches:\n";
                IEnumerable<BE.Branch> BranchList = myBL.GetAllBranchs();
                foreach (BE.Branch item in BranchList)
                {
                    DataBase.Text += (item.ToString() + "\n");
                }
            }
            if (collapseClients.IsChecked == false)
            {
                DataBase.Text += "\nClients:\n";
                IEnumerable<BE.Client> ClientList = myBL.GetAllClients();
                foreach (BE.Client item in ClientList)
                {
                    DataBase.Text += (item.ToString() + "\n");
                }
            }
        }
        public Print_Data_Base()
        {
            InitializeComponent();
            try
            {
                AddText();
            }
            catch(Exception Exp)
            {
                DataBase.Text = Exp.ToString();
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddText();
            }
            catch (Exception Exp)
            {
                DataBase.Text = Exp.ToString();
            }
        }
    }
}
