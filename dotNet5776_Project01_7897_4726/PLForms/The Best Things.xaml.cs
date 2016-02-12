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
    /// Interaction logic for The_Best_Things.xaml
    /// </summary>
    public partial class The_Best_Things : Window
    {
        public The_Best_Things()
        {
            InitializeComponent();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as Window).Title = "Best Of Something";
        }

        private void TypeComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = new ComboBoxItem();
            temp.Content = "Best Dish";
            temp.ToolTip = "Shows the best dish in the database";
            TypeComboBox.Items.Add(temp);
            temp = new ComboBoxItem();
            temp.Content = "Best Branch";
            temp.ToolTip = "Shows the best branch in the database";
            TypeComboBox.Items.Add(temp);
            temp = new ComboBoxItem();
            temp.Content = "Best Client";
            temp.ToolTip = "Shows the best client in the database";
            TypeComboBox.Items.Add(temp);
        }
        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch ((TypeComboBox.SelectedItem as ComboBoxItem).Content as string)
                {
                    case "Best Dish":
                        Details.Text = BL.FactoryBL.getBL().MostOrderedDish().ToString();
                        break;
                    case "Best Client":
                        Details.Text = BL.FactoryBL.getBL().BestCustomer().ToString();
                        break;
                    case "Best Branch":
                        Details.Text = BL.FactoryBL.getBL().BestBranch().ToString();
                        break;
                    default:
                        Details.Text = "";
                        break;
                }
            }
            catch (Exception Exp)
            {
                MessageBox.Show(Exp.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
