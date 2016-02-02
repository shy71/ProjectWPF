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
            (sender as ComboBox).ItemsSource = new String[] { "Profit by dishes", 
                                                              "Profit by date", 
                                                              "Profit by address", 
                                                              "Profit by branches", 
                                                              "Profit by Kashrut of the dish", 
                                                              "Profit by Kashrut of the branch",
                                                              "Profit by day of the week",
                                                             "Dish amount ordered by date",
                                                              "Dish amount ordered by dish",
                                                              "Dish amount ordered by branch",
                                                              "Dish amount by day of the week",
                                                              "bbb"
                                                            };
        }

        private void ProfitByChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateCombo.Visibility = Visibility.Collapsed;
            Stick_Diagram Diagram;
            foreach (object item in MainGrid.Children)
            {
                if (item.GetType() == typeof(Stick_Diagram))
                {
                    MainGrid.Children.Remove(item as Stick_Diagram);
                    break;
                }
            }
            switch((sender as ComboBox).SelectedItem as String)
            {
                case "Profit by day of the week":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByWeekDay()
                                                 select new BE.GroupSum(item.Key,item.Sum(),item.Key.ToString())).ToArray());
                    break;
                case "Dish amount by day of the week":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetDishAmountByWeekDay()
                                                 select new BE.GroupSum( item.Key, item.Sum(),item.Key.ToString())).ToArray());
                    break;
                case "Profit by Kashrut of the branch":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByBranchKashrut().OrderBy(item=>item.Key)
                                                 select new BE.GroupSum(item.Key, item.Sum(),item.Key.ToString())).ToArray());
                    break;
                case "Profit by Kashrut of the dish":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDishKashrut()
                                                 select new BE.GroupSum( item.Key, item.Sum(),item.Key.ToString())).ToArray());
                    break;
                case "Profit by date":
                    ///
                    DateCombo.Visibility = Visibility.Visible;
                    DateCombo.SelectedIndex = 0;
                    return;
                case  "Dish amount ordered by date":
                    ///
                    DateCombo.Visibility = Visibility.Visible;
                    DateCombo.SelectedIndex = 0;
                    return;
                case "Profit by address":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByAddress()
                                                 select new BE.GroupSum( item.Key, item.Sum(),item.Key)).ToArray());
                    break;
                case "Dish amount ordered by dish":
                    var temp = BL.FactoryBL.getBL().GetAllDishs();
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDishs()
                                                 select new BE.GroupSum(item.Key
                                                     , Convert.ToInt32(item.Sum() / (temp.First(item2 => item.Key == item2.ID).Price)),temp.First(item2=>item2.ID==item.Key).Name)).ToArray());
                    break;
                case "Dish amount ordered by branch":
                    var temp1=BL.FactoryBL.getBL().GetAllBranchs();
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetDishAmountByBranch()
                                                 select new BE.GroupSum(item.Key
                                                     , item.Sum(),temp1.First(item2=>item2.ID==item.Key).Name)).ToArray());
                    break;
                case "Profit by branches":
                    temp1 = BL.FactoryBL.getBL().GetAllBranchs();
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByBranches()
                                                 select new BE.GroupSum(item.Key, item.Sum(),temp1.First(item2=>item2.ID==item.Key).Name)).ToArray());
                    break;
                default://"Dishes"
                    temp = BL.FactoryBL.getBL().GetAllDishs();
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDishs()
                                                 select new BE.GroupSum(item.Key, item.Sum(),temp.First(item2 => item2.ID == item.Key).Name)).ToArray());
                    break;
            }
            Diagram.Height = 670;
            Diagram.Width = 992;
            MainGrid.Children.Add(Diagram);
            Grid.SetRow(Diagram, 1);
        }

        private void DateCombo_Loaded(object sender, RoutedEventArgs e)
        {
            DateCombo.ItemsSource = new String[] {"Days(last 30 days)","Months","Years"};
        }

        private void DateCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stick_Diagram Diagram;
            foreach (object item in MainGrid.Children)
            {
                if (item.GetType() == typeof(Stick_Diagram))
                {
                    MainGrid.Children.Remove(item as Stick_Diagram);
                    break;
                }
            }
            if ((ProfitByChoice.SelectedItem as String) == "Profit by date")
                switch ((sender as ComboBox).SelectedItem as String)
                {
                    case "Days(last 30 days)":
                        Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDates()
                                                     select new BE.GroupSum(item.Key, item.Sum(), item.Key)).ToArray());
                        break;
                    case "Months":
                        Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDates()
                                                     group item.Sum() by DateTime.Parse(item.Key).Month + "/" + DateTime.Parse(item.Key).Year into item3
                                                     select new BE.GroupSum(item3.Key, item3.Sum(), item3.Key)).ToArray());
                        break;
                    case "Years":
                        Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDates()
                                                     group item.Sum() by DateTime.Parse(item.Key).Year into item3
                                                     select new BE.GroupSum(item3.Key, item3.Sum(), item3.Key.ToString())).ToArray());
                        break;
                    default://"Dishes"
                        return;
                }
            else
                switch ((sender as ComboBox).SelectedItem as String)
                {
                    case "Days(last 30 days)":
                        Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetDishAmountByDate()
                                                     select new BE.GroupSum( item.Key, item.Sum(), item.Key)).ToArray());
                        break;
                    case "Months":
                        Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetDishAmountByDate()
                                                     group item.Sum() by DateTime.Parse(item.Key).Month + "/" + DateTime.Parse(item.Key).Year into item3
                                                     select new BE.GroupSum(item3.Key, item3.Sum(), item3.Key)).ToArray());
                        break;
                    case "Years":
                        Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetDishAmountByDate()
                                                     group item.Sum() by DateTime.Parse(item.Key).Year into item3
                                                     select new BE.GroupSum(item3.Key,item3.Sum(), item3.Key.ToString())).ToArray());
                        break;
                    default://"Dishes"
                        return;
                }

            Diagram.Height = 670;
            Diagram.Width = 992;
            MainGrid.Children.Add(Diagram);
            Grid.SetRow(Diagram, 1);
        }
    }
}
