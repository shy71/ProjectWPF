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
    /// Interaction logic for Search_Bing.xaml
    /// </summary>
    public partial class Search_Bing : Window
    {
        public Search_Bing()
        {
            InitializeComponent();
            SearchText.Str = "Search";
            SearchText = new TextControl("Search");
            SearcgResults.Text = "";
            BL.IBL myBL = BL.FactoryBL.getBL();
            SearcgResults.Text += "\nDishes:\n";
            IEnumerable<BE.Dish> DishList = myBL.GetAllDishs();
            foreach (BE.Dish item in DishList)
            {
                SearcgResults.Text += (item.ToString() + "\n");
            }
            SearcgResults.Text += "\nBranches:\n";
            IEnumerable<BE.Branch> BranchList = myBL.GetAllBranchs();
            foreach (BE.Branch item in BranchList)
            {
                SearcgResults.Text += (item.ToString() + "\n");
            }
            SearcgResults.Text += "\nClients:\n";
            IEnumerable<BE.Client> ClientList = myBL.GetAllClients();
            foreach (BE.Client item in ClientList)
            {
                SearcgResults.Text += (item.ToString() + "\n");
            }
        }
        private bool firstRound = true;
        private void SearchText_Changed(object sender, BE.EventValue e)
        {
            if (firstRound)
                firstRound = false;
            else
            {
                SearcgResults.Text = "";
                List<IEnumerable<BE.InterID>> list = BL.FactoryBL.getBL().Search((sender as TextControl).GetText());
                foreach (IEnumerable<BE.InterID> item in list)
                {
                    foreach (var v in item)
                        SearcgResults.Text += v.ToString() + "\n";
                    SearcgResults.Text += "\n\n";
                }
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
