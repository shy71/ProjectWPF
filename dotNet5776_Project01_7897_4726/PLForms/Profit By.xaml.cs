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
    /// Interaction logic for Profit_By.xaml
    /// </summary>
    public partial class Profit_By : Window
    {
        public Profit_By()
        {
            InitializeComponent();
            BE.ProfitWindowReturnValue.str = "Address";
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ByAddress_Checked(object sender, RoutedEventArgs e)
        {
            BE.ProfitWindowReturnValue.str = "Address";
        }

        private void ByDate_Checked(object sender, RoutedEventArgs e)
        {
            BE.ProfitWindowReturnValue.str = "Date";
        }

        private void ByDishes_Checked(object sender, RoutedEventArgs e)
        {
            BE.ProfitWindowReturnValue.str = "Dishes";
        }
    }
}
