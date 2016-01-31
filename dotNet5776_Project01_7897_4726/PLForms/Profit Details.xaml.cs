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

        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ProfitByChoice_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as ComboBox).ItemsSource = new String[] { "Dishes", "Date", "Address" };
        }

        private void ProfitByChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stick_Diagram Diagram;
            switch((sender as ComboBox).SelectedItem as String)
            {
                case "Dishes":
                    IEnumerable<IGrouping<int, float>> groups1 = BL.FactoryBL.getBL().GetProfitByDishs();
                    Diagram = new Stick_Diagram((from item in groups1
                                                  select new BE.GroupSum(item.Key, item.Sum())).ToArray());
                    break;
                case "Date":
                    IEnumerable<IGrouping<string, float>> groups2 = BL.FactoryBL.getBL().GetProfitByDates();
                    Diagram = new Stick_Diagram((from item in groups2
                                                 select new BE.GroupSum(BE.GroupingByType.Date,item.Key, item.Sum())).ToArray());
                    break;
                case "Address":
                    IEnumerable<IGrouping<string, float>> groups3 = BL.FactoryBL.getBL().GetProfitByAddress();
                    Diagram = new Stick_Diagram((from item in groups3
                                                 select new BE.GroupSum(BE.GroupingByType.Address, item.Key, item.Sum())).ToArray());
                    break;
                default:
                    groups1 = BL.FactoryBL.getBL().GetProfitByDishs();
                    Diagram = new Stick_Diagram((from item in groups1
                                                  select new BE.GroupSum(item.Key, item.Sum())).ToArray());
                    break;
            }
            Diagram.Height = 264;
            Diagram.Width = 392;
            MainGrid.Children.Add(Diagram);
            Grid.SetRow(Diagram, 1);
        }
    }
}
