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
    /// Interaction logic for DishEditor.xaml
    /// </summary>
    public partial class DishEditor : Window
    {
        BE.Dish dish;
        BE.Kashrut kosher;
        bool IsUpdated=false;
        public DishEditor(BE.Kashrut kosher=BE.Kashrut.LOW)
        {
            InitializeComponent();
            KashrutCombo.ItemsSource = from item in typeof(BE.Kashrut).GetEnumValues().Cast<BE.Kashrut>()
                                       where item >= kosher
                                       select typeof(BE.Kashrut).GetEnumName(item);
            SizeCombo.ItemsSource = Enum.GetNames(typeof(BE.Size));
            dish = new BE.Dish();
            this.kosher = kosher;
            nameBox.SetBinding(dish, "Name", BindingMode.TwoWay);
            priceBox.SetBinding(dish, "Price",BindingMode.TwoWay);
            idBox.SetBinding(dish, "ID", BindingMode.TwoWay);
        }
        public DishEditor(BE.Dish dish,BE.Kashrut kosher=BE.Kashrut.LOW)
        {
            InitializeComponent();
            KashrutCombo.ItemsSource = from item in typeof(BE.Kashrut).GetEnumValues().Cast<BE.Kashrut>()
                                       where item >= kosher
                                       select typeof(BE.Kashrut).GetEnumName(item);
            SizeCombo.ItemsSource = Enum.GetNames(typeof(BE.Size));
            this.dish = dish;
            IsUpdated = true;
            this.kosher = kosher;
            DoButton.Content = "Update!";
            nameBox.SetBinding(dish, "Name", BindingMode.TwoWay);
            priceBox.SetBinding(dish, "Price", BindingMode.TwoWay);
            idBox.SetBinding(dish, "ID", BindingMode.TwoWay);
            nameBox.SetText(dish.Name);
            priceBox.SetText(dish.Price.ToString());
            idBox.SetText(dish.ID.ToString());
            checkBox.IsEnabled = false;
            checkBox.Foreground = Brushes.Black;
            idBox.ToolTip = "You cant change an ID of a dish!";
            KashrutCombo.SelectedItem = dish.Kosher.ToString();
            SizeCombo.SelectedItem = dish.Size.ToString();
        }
        private void priceBox_Changed(object sender, BE.EventValue e)
        {
            float num;
            if (!float.TryParse(e.Value.ToString(), out num))
                dish.Price = 0;
        }

        private void dishCombo_Loaded(object sender, RoutedEventArgs e)
        {
            dishCombo.ItemsSource = BL.FactoryBL.getBL().GetAllDishs(item => item.Kosher >= kosher && dish.ID != item.ID);
            dishCombo.DisplayMemberPath = "Name";
            dishCombo.SelectedValuePath = "ID";
            if (dishCombo.Items.Count == 0)
            {
                dishCombo.IsEnabled = false;
                dishCombo.ToolTip = "There isnt any dishs in this level to pick from!";
            }
        }
        private void DoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dish.Price <= 0)
                    throw new Exception("The price must be a number above zero, not letters or negitive number");
                if(dish.ID<0)
                    throw new Exception("The ID must be a number above zero, not letters or negitive number");
                if(IsUpdated)
                {
                    BL.FactoryBL.getBL().UpdateDish(dish);
                }
                else
                {
                    BL.FactoryBL.getBL().AddDish(dish);
                }
                
                MessageBox.Show("The dish " + dish.Name + " was " + ((IsUpdated) ? "Updated!" : "created!"), "Dish" + ((IsUpdated) ? "Updated!" : "created"), MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message,"Warning!");
            }
        }

        private void idBox_Changed(object sender, BE.EventValue e)
        {
            int num;
            if (!int.TryParse(e.Value.ToString(), out num))
                dish.ID = -1;
            if (idBox.ForeG == Brushes.Gray)
                dish.ID = 0;
        }

        private void SizeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dish.Size = BE.Extensions.ToSize(SizeCombo.SelectedItem.ToString());
        }

        private void KashrutCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dish.Kosher = BE.Extensions.ToKashrut(KashrutCombo.SelectedItem.ToString());
        }
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            BE.Dish copyDish = BL.FactoryBL.getBL().GetAllDishs(item => item.ID == (int)dishCombo.SelectedValue).First();
            KashrutCombo.SelectedItem = copyDish.Kosher.ToString();
            SizeCombo.SelectedItem = copyDish.Size.ToString();
            nameBox.SetText(copyDish.Name);
            priceBox.SetText(copyDish.Price.ToString());
        }
        ////}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
