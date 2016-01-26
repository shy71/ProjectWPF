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
        public event EventHandler<BE.EventValue> TryDelete;
        int ID;
        public OrderDeiltes()
        {
            InitializeComponent();
        }
        public OrderDeiltes(BE.Order order)
        {
            InitializeComponent();//error if branch does not exsist
            BE.Branch temp=BL.FactoryBL.getBL().GetAllBranchs(item=> item.ID==order.BranchID).FirstOrDefault();
            if(temp==null)
                throw new Exception("There isnt a branch that matches this Order!");
            ShortAddress.Text =temp.Name;
            ShortAddress.ToolTip = temp.Address;
            FullAddress.Text = order.Address;
            if (order.Date == DateTime.MinValue)
            {
                Date.Content = "Not sended";
                Date.ToolTip = "Prees Send to send it now!";
                
                
            }
            else if(!order.Delivered)
            {
                Date.Content = order.Date.ToShortDateString();
                Date.ToolTip = order.Date.ToShortTimeString();
                SendBtn.Visibility = Visibility.Hidden;
                    Delete.Visibility = Visibility.Hidden;
                timeLeft.Content = "The order is on the way!";
                timeLeft.Visibility = Visibility.Visible;

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
                SendBtn.Visibility = Visibility.Hidden;
            }
            ID = order.ID;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (TryDelete != null)
                TryDelete(this,new BE.EventValue(ID));
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to delete this order?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No))
                BL.FactoryBL.getBL().DeleteOrder(ID);
            if (Deleted != null)
                Deleted(this, new BE.EventValue(ID));
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            //Open window update order
            if (Updated != null)
                Updated(this, new BE.EventValue(ID));
        }
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to send this order?", "Send Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes))
            {
                var temp = BL.FactoryBL.getBL().GetAllOrders(item => item.ID == ID).First();
                temp.Date = DateTime.Now;
                BL.FactoryBL.getBL().UpdateOrder(temp);
            }
            if (Sended != null)
                Sended(this, new BE.EventValue(ID));
        }

        private void Arrived_Click(object sender, RoutedEventArgs e)
        {
            var temp = BL.FactoryBL.getBL().GetAllOrders(item => item.ID == ID).First();
            BL.FactoryBL.getBL().DeliveredOrder(temp);
            temp = BL.FactoryBL.getBL().GetAllOrders(item => item.ID == ID).First();
            if (Arived != null)
                Arived(this, new BE.EventValue(ID));
        }
    }
}
