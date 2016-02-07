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
using System.Windows.Threading;
using System.Threading;

namespace PLForms
{
    /// <summary>
    /// Interaction logic for OrderDeiltes.xaml
    /// </summary>
    public partial class OrderDeiltes : UserControl
    {
        public event EventHandler<BE.EventValue> Deleted;
        public event EventHandler<BE.EventValue> Updated;
        public event EventHandler<BE.EventValue> Sended;
        public event EventHandler<BE.EventValue> Arived;
        int ID;
        bool IsDeliverd = false;
        bool IsWindowMode = true;
        public OrderDeiltes()
        {
            InitializeComponent();
        }
        public OrderDeiltes(BE.Order order, bool IsWindowMode = true)
        {
            InitializeComponent();//error if branch does not exsist
            BE.Branch temp = BL.FactoryBL.getBL().GetAllBranchs(item => item.ID == order.BranchID).FirstOrDefault();
            if (temp == null)
                throw new Exception("There isn't a branch that matches this Order!");
            ShortAddress.Text = temp.Name;
            ShortAddress.ToolTip = temp.Address;
            FullAddress.Text = order.Address;
            if (order.Date == DateTime.MinValue)
            {
                Date.Content = "Not sent";
                Date.ToolTip = "Prees Send to send it now!";
            }
            else if (!order.Delivered)
            {
                Date.Content = order.Date.ToShortDateString();
                Date.ToolTip = order.Date.ToShortTimeString();
                SendBtn.Visibility = Visibility.Collapsed;
                DeleteBtn.Visibility = Visibility.Collapsed;
                timeLeft.Content = "The order is on the way!";
                timeLeft.Visibility = Visibility.Visible;
                EditBtn.Visibility = Visibility.Collapsed;
                //timeLeft.Visibility = Visibility.Visible;
                //new Thread(() =>
                //    {
                //        while ((DateTime.Now - order.Date).Seconds >= 0)
                //            Dispatcher.BeginInvoke((Action)(() => timeLeft.Content = "-" + (DateTime.Now - order.Date).Seconds));
                //        timeLeft.Content = "Arived!";
                //    }).Start();            
            }
            else
            {
                Date.Content = order.Date.ToShortDateString();
                Date.ToolTip = order.Date.ToShortTimeString();
                SendBtn.Visibility = Visibility.Collapsed;
                EditBtn.ToolTip = "See the dishs in the order";
                IsDeliverd = true;
            }
            if (!IsWindowMode)
            {
                DeleteBtn.Visibility = Visibility.Collapsed;
                EditBtn.Visibility = Visibility.Collapsed;
                SendBtn.Visibility = Visibility.Collapsed;
                timeLeft.Visibility = Visibility.Collapsed;

            }
            ID = order.ID;
            this.IsWindowMode = IsWindowMode;
            priceOrder.Text = BL.FactoryBL.getBL().PriceOfOrder(ID).ToString() + "$";
            if (IsWindowMode)
            {
                this.ToolTip = priceOrder.Text;
            }

        }
        public void DeleteOrder()
        {
            Delete_Click(this, null);
        }
        public void UpdateOrder()
        {
            Update_Click(this, null);
        }
        public void SendOrder()
        {
            Send_Click(this, null);
        }
        public void ArivedOrder()
        {
            Arrived_Click(this, null);
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if ((!IsWindowMode) || MessageBoxResult.Yes == MessageBox.Show((IsDeliverd ? "It is recommended not to delete deliverd orders! without them you will not get the full exprince the resturant has to offer\n" : "") + "Are you sure you want to delete this order?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No))
                BL.FactoryBL.getBL().DeleteOrder(ID);
            if (IsWindowMode && Deleted != null)
                Deleted(this, new BE.EventValue(ID));
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            new OrderEditorStep2(BL.FactoryBL.getBL().GetAllOrders(item => item.ID == ID).First(), IsDeliverd).ShowDialog();
            if (IsWindowMode && Updated != null)
                Updated(this, new BE.EventValue(ID));
            priceOrder.Text = BL.FactoryBL.getBL().PriceOfOrder(ID).ToString() + "$";
        }
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var price = BL.FactoryBL.getBL().PriceOfOrder(ID);
            if (price == 0)
            {
                MessageBox.Show("You cant send an order with a total price of 0!", "Empty Order");
                return;
            }
            if ((!IsWindowMode) || MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to send this order? Cost - " + price.ToString() + "$", "Send Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes))
            {
                var temp = BL.FactoryBL.getBL().GetAllOrders(item => item.ID == ID).First();
                temp.Date = DateTime.Now;
                BL.FactoryBL.getBL().UpdateOrder(temp);
            }
            if (IsWindowMode && Sended != null)
                Sended(this, new BE.EventValue(ID));
        }

        private void Arrived_Click(object sender, RoutedEventArgs e)
        {
            var temp = BL.FactoryBL.getBL().GetAllOrders(item => item.ID == ID).First();
            BL.FactoryBL.getBL().DeliveredOrder(temp);
            temp = BL.FactoryBL.getBL().GetAllOrders(item => item.ID == ID).First();
            if (IsWindowMode && Arived != null)
                Arived(this, new BE.EventValue(ID));
        }

        private void MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Height *= 1.5;
            (sender as Button).Width *= 1.5;
        }
        private void MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Height /= 1.5;
            (sender as Button).Width /= 1.5;
        }
    }
}
