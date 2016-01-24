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
    /// Interaction logic for Profit_Details.xaml
    /// </summary>
    public partial class Profit_Details : Window
    {
        public Profit_Details()
        {
            InitializeComponent();
            try
            {
                switch(BE.ProfitWindowReturnValue.str)
                {
                    case "Address":
                        //finish address grouping print
                        break;
                    case "Date":
                        //finish date grouping print
                        break;
                    case "Dishes":
                        //finish dishes grouping print
                        break;
                    default:
                        throw new Exception("Problem with str.");
                }

            }
            catch(Exception Exp)
            {
                ProfitDetails.Text = Exp.ToString();
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
