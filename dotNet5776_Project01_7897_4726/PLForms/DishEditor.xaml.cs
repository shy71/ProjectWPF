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
        public DishEditor()
        {
            InitializeComponent();
        }
        //public DishEditor(BE.Kashrut kosher)
        //{
        //    InitializeComponent();
        //    KashrutCombo.ItemsSource = from item in typeof(BE.Kashrut).GetEnumValues().Cast<BE.Kashrut>()
        //                         where item >= kosher
        //                         select typeof(BE.Kashrut).GetEnumName(item);
        //    SizeCombo.ItemsSource = Enum.GetValues(typeof(BE.Size));
        //    dish = new BE.Dish();
        //    this.kosher = kosher;
        //}
        //public DishEditor(BE.Dish dish)
        //{
        //    InitializeComponent();
        //    KashrutCombo.ItemsSource =typeof(BE.Kashrut).GetEnumValues();
        //    SizeCombo.ItemsSource = Enum.GetValues(typeof(BE.Size));
        //    this.dish = dish;
        //    IsUpdated = true;
        //}

        //private void nameBox_Changed(object sender, BE.EventValue e)
        //{
        //    dish.Name = e.Value.ToString();
        //}

        //private void priceBox_Changed(object sender, BE.EventValue e)
        //{
        //    float num;
        //    if(priceBox.ForeG==Brushes.Gray)
        //    {
        //        return;
        //    }
        //    else if (float.TryParse(e.Value.ToString(), out num) && num > 0)
        //    {
        //        priceBox.SetText(num.ToString());
        //        dish.Price = num;
        //    }
        //    else
        //        priceBox.Clear();

        //}

        //private void dishCombo_Loaded(object sender, RoutedEventArgs e)
        //{
        //    dishCombo.ItemsSource = BL.FactoryBL.getBL().GetAllDishs(item2 => item2.Kosher >= kosher);
        //    dishCombo.DisplayMemberPath = "Name";
        //    dishCombo.SelectedValuePath = "ID";
        //    if (dishCombo.Items.Count == 0)
        //    {
        //        dishCombo.IsEnabled = false;
        //        dishCombo.ToolTip = "There isnt any dishs in this level to pick from!";
        //    }
        //}

        //private void idBox_Changed(object sender, BE.EventValue e)
        //{
        //    int num;
        //    if (idBox. ForeG == Brushes.Gray)
        //    {
        //        return;
        //    }
        //    else  if (priceBox.ForeG != Brushes.Gray && int.TryParse(e.Value.ToString(), out num) && num > 0)
        //    {
        //        idBox.SetText(num.ToString());
        //        dish.ID = num;
        //    }
        //    else
        //        idBox.Clear();

        //}

        //private void DoButton_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        dish.Size = (BE.Size)dishCombo.SelectedValue;
        //        dish.Kosher = (BE.Kashrut)KashrutCombo.SelectedValue;
        //        if (dish.Price == 0)
        //            throw new Exception("The price cant be 0!");
        //        if (idBox.IsEnabled == true && idBox.ForeG == Brushes.Gray)
        //            throw new Exception("The ID cant be empty! fill it or check it to be assingend autmiticaly");
        //        if (!IsUpdated)
        //        {
        //            BL.FactoryBL.getBL().AddDish(dish);
        //        }
        //        else
        //            BL.FactoryBL.getBL().UpdateDish(dish);
        //        MessageBox.Show("The dish " + dish.Name + " was " + ((IsUpdated) ? "Updated!" : "created!"), "Branch created", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    catch(Exception exp)
        //    {
        //        MessageBox.Show(exp.Message);
        //    }
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
