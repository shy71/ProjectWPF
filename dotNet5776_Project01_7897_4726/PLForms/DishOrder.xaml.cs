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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLForms
{
    /// <summary>
    /// Interaction logic for DishOrder.xaml
    /// </summary>
    public partial class DishOrder : UserControl
    {
        static EventHandler<BE.EventValue> Refresh;
        public EventHandler<BE.EventValue> AmountChanged;
        BE.Dish dish;
        int DsID;
        BE.Order order;
        public DishOrder()
        {
            InitializeComponent();
        }
        public DishOrder(BE.DishOrder ds)
        {
            InitializeComponent();//eror dish doesnt exist
            dish = BL.FactoryBL.getBL().GetAllDishs(item => item.ID == ds.DishID).First();
            DsID = ds.ID;    
            this.DataContext = dish;
            order = BL.FactoryBL.getBL().GetAllOrders(item => item.ID == ds.OrderID).First();
            Amount.Text = ds.DishAmount.ToString();
            //AmountGrid.DataContext = ds; doesnt work like that
            Details.Text = dish.ToString().Substring(dish.ToString().IndexOf("\n")+2).Replace("\t","");
            Refresh += RefreshBtn;
            RefreshBtn(this, new BE.EventValue(BL.FactoryBL.getBL().PriceOfOrder(order), order.ID.ToString()));
        }
        private void RefreshBtn(object sender , BE.EventValue e)
        {
            if (e.pName != order.ID.ToString())
                return;
            if ((Convert.ToInt32(e.Value)) + dish.Price > BL.FactoryBL.getBL().MAX_PRICE)
            {
                (PlusBtn).IsEnabled = false;
                (PlusBtn).ToolTip = "You cant add more from this dish! you will be above the aprooved price of an order";
            }
            else
            {
                (PlusBtn).IsEnabled = true;
                (PlusBtn).ToolTip = "Add one dish to the order";
            }
        }
        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            //put price in temp integer?
            //if(BL.FactoryBL.getBL().PriceOfOrder(order)+dish.Price>BL.FactoryBL.getBL().MAX_PRICE)
            //    MessageBox.Show("Your price will exceed the Max price! please remove some dishs to add this one, or split your order.","Prcie above Max",MessageBoxButton.OK,MessageBoxImage.Error);
            Amount.Text = (Convert.ToInt32(Amount.Text) + 1).ToString(); ;
            if (!MinusBtn.IsEnabled)
                MinusBtn.IsEnabled = true;
            if (AmountChanged != null)
                AmountChanged(this, new BE.EventValue(Convert.ToInt32(Amount.Text), DsID.ToString()));
            if (Refresh != null)
            Refresh(this, new BE.EventValue(BL.FactoryBL.getBL().PriceOfOrder(order), order.ID.ToString()));
        }
        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            //put price in temp integer?
            //if(BL.FactoryBL.getBL().PriceOfOrder(order)+dish.Price>BL.FactoryBL.getBL().MAX_PRICE)
            //    MessageBox.Show("Your price will exceed the Max price! please remove some dishs to add this one, or split your order.","Prcie above Max",MessageBoxButton.OK,MessageBoxImage.Error);
            Amount.Text =(Convert.ToInt32( Amount.Text)-1).ToString();
            if (Convert.ToInt32(Amount.Text) == 0)
                MinusBtn.IsEnabled = false;
            if (AmountChanged != null)
                AmountChanged(this, new BE.EventValue(Convert.ToInt32(Amount.Text), DsID.ToString()));
            if(Refresh!=null)
            Refresh(this, new BE.EventValue(BL.FactoryBL.getBL().PriceOfOrder(order), order.ID.ToString()));
        }
        private void WindowMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (PlusBtn.IsEnabled&&e.Delta > 0)
                Plus_Click(this, null);
            else if (MinusBtn.IsEnabled&& e.Delta < 0)
                Minus_Click(this, null);
            return;
        }
        public void Close()
        {
            Refresh-=RefreshBtn;
        }
    }
}
