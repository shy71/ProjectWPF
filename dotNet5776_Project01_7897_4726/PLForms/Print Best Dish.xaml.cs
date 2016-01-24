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
    /// Interaction logic for Print_Best_Dish.xaml
    /// </summary>
    public partial class Print_Best_Dish : Window
    {
        public Print_Best_Dish()
        {
            InitializeComponent();
            try
            {
                BE.Dish bestDish = BL.FactoryBL.getBL().MostOrderedDish();
                BestDish.Text = bestDish.ToString();
            }
            catch(Exception Exp)
            {
                BestDish.Text = Exp.ToString();
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
