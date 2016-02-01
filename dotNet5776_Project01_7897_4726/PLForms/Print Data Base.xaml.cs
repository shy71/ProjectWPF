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
            Dishes.Text = "";
            Branches.Text = "";
            Orders.Text = "";
            Clients.Text = "";
            BL.IBL myBL = BL.FactoryBL.getBL();
            Orders.Text += "\nOrders\n";
            IEnumerable<BE.Order> OrderList = myBL.GetAllOrders();
            foreach (BE.Order item in OrderList)
            {
                Orders.Text += (item.ToString() + "\n");
            }
            Dishes.Text += "\nDishes:\n";
            IEnumerable<BE.Dish> DishList = myBL.GetAllDishs();
            foreach (BE.Dish item in DishList)
            {
                Dishes.Text += (item.ToString() + "\n");
            }
            Branches.Text += "\nBranches:\n";
            IEnumerable<BE.Branch> BranchList = myBL.GetAllBranchs();
            foreach (BE.Branch item in BranchList)
            {
                Branches.Text += (item.ToString() + "\n");
            }
            Clients.Text += "\nClients:\n";
            IEnumerable<BE.Client> ClientList = myBL.GetAllClients();
            foreach (BE.Client item in ClientList)
            {
                Clients.Text += (item.ToString() + "\n");
            }
        }
        public Print_Data_Base()
        {
            InitializeComponent();
            SearchBing.Str = "Search Bing for specifics";
            try
            {
                AddText();
            }
            catch (Exception Exp)
            {
                MessageBox.Show(Exp.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        internal void PrintSearch<T>(object sender)
        {
            IEnumerable<BE.InterID> list = from item1 in BL.FactoryBL.getBL().Search(SearchBing.GetText())
                                           from item2 in (item1 as IEnumerable<BE.InterID>)
                                           where item2.GetType().Name == typeof(T).Name
                                           select item2;
            foreach (BE.InterID var in list)
            {
                (sender as TextBlock).Text += var.ToString() + "\n";
            }
        }
        private bool firstRound = true;
        private void SearchBing_Changed(object sender, BE.EventValue e)
        {
            if (firstRound)
                firstRound = false;
            else
            {
                if (SearchBing.GetText() == null || SearchBing.GetText() == "Search Bing for specifics")
                    AddText();
                else
                {
                    Dishes.Text = "";
                    Orders.Text = "";
                    Branches.Text = "";
                    Clients.Text = "";
                    PrintSearch<BE.Branch>(Branches);
                    PrintSearch<BE.Dish>(Dishes);
                    PrintSearch<BE.Client>(Clients);
                    PrintSearch<BE.Order>(Orders);
                }
            }
        }
    }
}
