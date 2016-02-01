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
                                                              "Dish amount ordered by dish profit",
                                                              "Dish amount ordered by branch profit",
                                                              "Dish amount by day of the week"
                                                            };
        }

        private void ProfitByChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stick_Diagram Diagram;
            switch((sender as ComboBox).SelectedItem as String)
            {
                case "Profit by day of the week":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByWeekDay()
                                                 select new BE.GroupSum(BE.GroupingByType.WeekDays,item.Key,item.Sum())).ToArray());
                    break;
                case "Dish amount by day of the week":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetDishAmountByWeekDay()
                                                 select new BE.GroupSum(BE.GroupingByType.DishAmountbyWeekDays, item.Key, item.Sum())).ToArray());
                    break;
                case "Profit by Kashrut of the branch":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByBranchKashrut()
                                                 select new BE.GroupSum(BE.GroupingByType.BranchKashrut, item.Key, item.Sum())).ToArray());
                    break;
                case "Profit by Kashrut of the dish":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDishKashrut()
                                                 select new BE.GroupSum(BE.GroupingByType.DishKashrut, item.Key, item.Sum())).ToArray());
                    break;
                case "Profit by date":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDates()
                                                 select new BE.GroupSum(BE.GroupingByType.Date,item.Key, item.Sum())).ToArray());
                    break;
                case "Profit by address":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByAddress()
                                                 select new BE.GroupSum(BE.GroupingByType.Address, item.Key, item.Sum())).ToArray());
                    break;
                case "Dish amount ordered by dish profit":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDishs()
                                                 select new BE.GroupSum(BE.GroupingByType.DishesAmountbyDish,item.Key
                                                     , Convert.ToInt32(item.Sum() / (BL.FactoryBL.getBL().GetAllDishs(item2 => item.Key == item2.ID).First().Price)))).ToArray());
                    break;
                case "Dish amount ordered by branch profit":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetDishAmountByBranch()
                                                 select new BE.GroupSum(BE.GroupingByType.DishesAmountbyBranch, item.Key
                                                     , item.Sum())).ToArray());
                    break;
                case "Profit by branches":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByBranches()
                                                 select new BE.GroupSum(BE.GroupingByType.Branch, item.Key, item.Sum())).ToArray());
                    break;
                default://"Dishes"
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDishs()
                                                  select new BE.GroupSum(BE.GroupingByType.Dish,item.Key, item.Sum())).ToArray());
                    break;
            }
            Diagram.Height = 670;
            Diagram.Width = 992;
            MainGrid.Children.Add(Diagram);
            Grid.SetRow(Diagram, 1);
        }
    }
}
