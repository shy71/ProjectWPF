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
    /// Interaction logic for ShowBest.xaml
    /// </summary>
    public partial class ShowBest : Window
    {
        public string Type { get; set; }
        public ShowBest()
        {
            InitializeComponent();
        }
        public ShowBest(string type)
        {
            InitializeComponent();
            Type = type;

        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as Window).Title = "Best in the database";
            try
            {
                if (Type == "Dish")
                    Best.Text = BL.FactoryBL.getBL().MostOrderedDish().ToString();
                else if (Type == "Client")
                    Best.Text = BL.FactoryBL.getBL().BestCustomer().ToString();
                else if (Type == "Branch")
                {
                    BE.Branch temp = BL.FactoryBL.getBL().BestBranch();
                    Best.Text = temp.ToString();
                    //Best.Text += "\nThe best Dish in the branch:\n" + BL.FactoryBL.getBL().BestDishInBranch(temp).ToString();
                }

            }

            catch (Exception Exp)
            {
                Best.Text = Exp.ToString();
            }
        }
    }
}
