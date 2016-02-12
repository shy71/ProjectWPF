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
        Predicate<int> IsBranch;
        bool IsOnlyBranch = false;
        public Profit_Details()
        {
            InitializeComponent();
            IsBranch = x => true;
            BranchLabel.Content = "Statics from all the branchs:";
            BranchLabel.ToolTip = BL.FactoryBL.getBL().GetAllBranchs().Count();
        }
        public Profit_Details(Predicate<int> predicate)
        {
            InitializeComponent();
            IsBranch = predicate;
            IsOnlyBranch = true;
            BE.Branch temp = BL.FactoryBL.getBL().GetAllBranchs(item => predicate(item.ID)).First();
            this.DataContext = new { Name = temp.Name, toolTip = temp.ToString() };
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ProfitByChoice_Loaded(object sender, RoutedEventArgs e)
        {
            var arr = new List<string> { "Profit by dishes","Dish amount ordered by dish","Profit by Kashrut of the dish", 
                                         "Profit by date", "Dish amount ordered by date",
                                         "Profit by day of the week","Dish amount by day of the week",
                                         "Profit by address"};
            if (!IsOnlyBranch)
            {
                arr.Add("Dish amount ordered by branchs");
                arr.Add("Profit by Kashrut of the branchs");
                arr.Add("Profit by branches");
            }
            (sender as ComboBox).ItemsSource = arr;
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
            switch ((sender as ComboBox).SelectedItem as String)
            {
                case "Profit by day of the week":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByWeekDay(IsBranch)
                                                 select new BE.GroupSum(item.Key, item.Sum(), item.Key.ToString(), item.Key.ToString())).OrderBy(item => item.LowerHeadr).ToArray());
                    break;
                case "Dish amount by day of the week":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetDishAmountByWeekDay(IsBranch)
                                                 select new BE.GroupSum(item.Key, item.Sum(), item.Key.ToString(), item.Key.ToString())).OrderBy(item => item.LowerHeadr).ToArray());
                    break;
                case "Profit by Kashrut of the branchs":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByBranchsKashrut().OrderBy(item => item.Key)
                                                 select new BE.GroupSum(item.Key, item.Sum(), item.Key.ToString(), item.Key.ToString())).OrderBy(item => BE.Extensions.ToKashrut(item.LowerHeadr)).ToArray());
                    break;
                case "Profit by Kashrut of the dish":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDishKashrut(IsBranch)
                                                 select new BE.GroupSum(item.Key, item.Sum(), item.Key.ToString(), item.Key.ToString())).OrderBy(item => BE.Extensions.ToKashrut(item.LowerHeadr)).ToArray());
                    break;
                case "Profit by date":
                    ///
                    DateCombo.Visibility = Visibility.Visible;
                    if (DateCombo.SelectedIndex == 0)
                        DateCombo_SelectionChanged(DateCombo, null);
                    else
                        DateCombo.SelectedIndex = 0;
                    return;
                case "Dish amount ordered by date":
                    ///
                    DateCombo.Visibility = Visibility.Visible;
                    if (DateCombo.SelectedIndex == 0)
                        DateCombo_SelectionChanged(DateCombo, null);
                    else
                        DateCombo.SelectedIndex = 0;


                    return;
                case "Profit by address":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByAddress(IsBranch)
                                                 select new BE.GroupSum(item.Key, item.Sum(), item.Key, item.Key)).OrderBy(item => item.LowerHeadr).ToArray());
                    break;
                case "Dish amount ordered by dish":
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDishs(IsBranch)
                                                 let dish = BL.FactoryBL.getBL().GetAllDishs().First(item2 => item.Key == item2.ID)
                                                 select new BE.GroupSum(item.Key, (int)item.Sum() / dish.Price, dish.Name, dish.ToString())).OrderBy(item => item.LowerHeadr).ToArray());
                    break;
                case "Dish amount ordered by branchs":
                    var temp1 = BL.FactoryBL.getBL().GetAllBranchs();
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetDishAmountByBranchs()
                                                 let branch=temp1.First(item2=>item2.ID==item.Key)
                                                 select new BE.GroupSum(item.Key
                                                     , item.Sum(), branch.Name, branch.ToString())).OrderBy(item => item.LowerHeadr).ToArray());
                    break;
                case "Profit by branches":
                    temp1 = BL.FactoryBL.getBL().GetAllBranchs();
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByBranches()
                                                 let branch=temp1.First(item2=>item2.ID==item.Key)
                                                 select new BE.GroupSum(item.Key, item.Sum(), branch.Name, branch.ToString())).OrderBy(item => item.LowerHeadr).ToArray());
                    break;
                default://"Dishes"
                    var temp = BL.FactoryBL.getBL().GetAllDishs();
                    Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDishs(IsBranch)
                                                 let dish=temp.First(item2=>item2.ID==item.Key)
                                                 select new BE.GroupSum(item.Key, item.Sum(), dish.Name, dish.ToString())).OrderBy(item => item.LowerHeadr).ToArray());
                    break;
            }
            Diagram.Height = 670;
            Diagram.Width = 992;
            MainGrid.Children.Add(Diagram);
            Grid.SetRow(Diagram, 2);
        }

        private void DateCombo_Loaded(object sender, RoutedEventArgs e)
        {
            DateCombo.ItemsSource = new String[] { "Days", "Months", "Years" };
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
                    case "Months":
                        Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDates(IsBranch)
                                                     group item.Sum() by DateTime.Parse(item.Key).Month + "/" + DateTime.Parse(item.Key).Year into item3
                                                     select new BE.GroupSum(item3.Key, item3.Sum(), item3.Key, item3.Key)).OrderBy(item =>DateTime.Parse(item.LowerHeadr.Insert(0,"01/"))).ToArray());
                        break;
                    case "Years":
                        Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDates(IsBranch)
                                                     group item.Sum() by DateTime.Parse(item.Key).Year into item3
                                                     select new BE.GroupSum(item3.Key, item3.Sum(), item3.Key.ToString(), item3.Key.ToString())).OrderBy(item => item.LowerHeadr).ToArray());
                        break;
                    default://"Days(last 30 days)"
                        Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetProfitByDates(IsBranch)
                                                     select new BE.GroupSum(item.Key, item.Sum(), item.Key, item.Key)).OrderBy(item => DateTime.Parse(item.LowerHeadr)).ToArray());
                        break;
                }
            else
                switch ((sender as ComboBox).SelectedItem as String)
                {
                    case "Months":
                        Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetDishAmountByDate(IsBranch)
                                                     group item.Sum() by DateTime.Parse(item.Key).Month + "/" + DateTime.Parse(item.Key).Year into item3
                                                     select new BE.GroupSum(item3.Key, item3.Sum(), item3.Key, item3.Key)).OrderBy(item => DateTime.Parse(item.LowerHeadr.Insert(0, "01/"))).ToArray());
                        break;
                    case "Years":
                        Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetDishAmountByDate(IsBranch)
                                                     group item.Sum() by DateTime.Parse(item.Key).Year into item3
                                                     select new BE.GroupSum(item3.Key, item3.Sum(), item3.Key.ToString(), item3.Key.ToString())).OrderBy(item => item.LowerHeadr).ToArray());
                        break;
                    default://"Days(last 30 days)"
                        Diagram = new Stick_Diagram((from item in BL.FactoryBL.getBL().GetDishAmountByDate(IsBranch)
                                                     select new BE.GroupSum(item.Key, item.Sum(), item.Key, item.Key)).OrderBy(item => DateTime.Parse(item.LowerHeadr)).ToArray());
                        break;
                }

            Diagram.Height = 670;
            Diagram.Width = 992;
            MainGrid.Children.Add(Diagram);
            Grid.SetRow(Diagram, 2);
        }
    }
}
