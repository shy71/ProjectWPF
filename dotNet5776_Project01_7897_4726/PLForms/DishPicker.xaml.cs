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
    /// Interaction logic for DishPicker.xaml
    /// </summary>
    public partial class DishPicker : Window
    {
        BE.Kashrut minKashrut;
        public event EventHandler<BE.EventValue> Added;
        int orderID;
        bool IsCtrlDown = false;
        public DishPicker()
        {
            InitializeComponent();
        }
        public DishPicker(BE.Kashrut MinKashrut, int OrderID)
        {
            InitializeComponent();
            minKashrut = MinKashrut;
            orderID = OrderID;
        }

        private void Kosher_Loaded(object sender, RoutedEventArgs e)
        {
            Kosher.ItemsSource = from item in typeof(BE.Kashrut).GetEnumValues().Cast<BE.Kashrut>()
                                 where item >= minKashrut
                                 select typeof(BE.Kashrut).GetEnumName(item);
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            TextBox text;
            int NumOfDishs = 0;
            foreach (StackPanel item in MainGrid.Children)
            {
                item.Children.RemoveRange(0, item.Children.Count);
            }
            var list = BL.FactoryBL.getBL().GetAllDishOrders(item => item.OrderID == orderID);
            var order = BL.FactoryBL.getBL().GetAllOrders(item => item.ID == orderID).First();
            BE.Dish RecomndedDish = BL.FactoryBL.getBL().SuggestedDish(BL.FactoryBL.getBL().GetAllClients(item => item.ID == order.ClientID).First().ID);
            foreach (BE.Dish item in BL.FactoryBL.getBL().GetAllDishs(item => !list.Any(item2 => item2.DishID == item.ID) && item.Kosher >= minKashrut&& item.Active==true))
            {
                text = new TextBox();
                if (RecomndedDish.ID == item.ID)
                {
                    text.Text = "Recommend Dish\n" + item.ToString().Replace("\t", "");//need checking
                    text.Foreground = Brushes.Green;
                }
                else
                    text.Text = item.ToString().Replace("\t", "");
                text.FontFamily = new FontFamily("Comic Sans MS");
                text.Opacity = 0.5;
                text.Width = 100;
                text.PreviewMouseUp += MouseClick;
                text.IsReadOnly = true;
                if (NumOfDishs % 3 == 0)
                    Stack1.Children.Add(text);
                else if (NumOfDishs % 3 == 1)
                    Stack2.Children.Add(text);
                else
                    Stack3.Children.Add(text);
                NumOfDishs++;
            }
        }

        void MouseClick(object sender, MouseButtonEventArgs e)
        {
            bool WasSelected = (sender as TextBox).Opacity == 1;
            if (!IsCtrlDown)
            {
                foreach (StackPanel stack in MainGrid.Children)
                {
                    foreach (TextBox item in stack.Children)
                    {
                        item.Opacity = 0.5;
                        item.BorderThickness = new Thickness(1);
                        item.BorderBrush = Brushes.Black;
                    }
                }
            }
            if (!WasSelected)
            {
                (sender as TextBox).Opacity = 1;
                (sender as TextBox).BorderThickness = new Thickness(2);
                (sender as TextBox).BorderBrush = Brushes.LightBlue;
            }
            else if (IsCtrlDown)
            {
                (sender as TextBox).Opacity = 0.5;
                (sender as TextBox).BorderThickness = new Thickness(1);
                (sender as TextBox).BorderBrush = Brushes.Black;
            }
        }

        private void KeyDownCheck(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                IsCtrlDown = true;
        }

        private void KeyUpCheck(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                IsCtrlDown = false;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)//לשנות!!!
        {
            List<int> listID = new List<int>();
            foreach (StackPanel stack in MainGrid.Children)
            {
                foreach (TextBox item in stack.Children)
                {
                    if (item.Opacity == 1)
                        listID.Add(Convert.ToInt32(item.Text.Substring(item.Text.IndexOf("ID: ") + 4, item.Text.IndexOf("\nName:") - item.Text.IndexOf("ID: ") - 4)));
                }
            }
            if (Added != null)
                Added(this, new BE.EventValue(listID, orderID.ToString()));
            this.Close();
        }

        private void Kosher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            minKashrut = BE.Extensions.ToKashrut(Kosher.SelectedItem.ToString());
            Refresh(this, null);
        }


    }
}
