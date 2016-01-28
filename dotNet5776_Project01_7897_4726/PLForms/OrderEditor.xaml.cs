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
    /// Interaction logic for MangeOrder.xaml
    /// </summary>
    public partial class OrderEditor : Window
    {
        BE.Client client;
        BE.Order tempOrder;
        public OrderEditor()
        {
            InitializeComponent();
        }
        public OrderEditor(BE.Client client)
        {
            InitializeComponent();
            this.client = client;
        }
        public OrderEditor(BE.Order order)
        {
            InitializeComponent();
            this.client = BL.FactoryBL.getBL().GetAllClients(item=>item.ID==order.ClientID).First();
            tempOrder = order;
            if (order.Address == client.Address)
                HomeCheckBox.IsChecked = true;
            addressBox.SetBinding(tempOrder, "Address", BindingMode.TwoWay);//check
        }

        private void HomeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            addressBox.SetText(client.Address);
        }
        private void HomeCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            addressBox.Clear();
        }
        private void RefreshStacks(object sender, RoutedEventArgs e)
        {
            int NumOfDishOrders=0;
            DishOrder us;
            foreach (StackPanel item in DishOrdersGrid.Children)
            {
                if (item.Children.Count != 0)
                    item.Children.RemoveRange(0, item.Children.Count);
            }
            foreach (BE.DishOrder item in BL.FactoryBL.getBL().GetAllDishOrders(item=>item.OrderID==tempOrder.ID))
            {
                us = new DishOrder(item);
                us.AmountChanged += DishOrderAmount;
                us.HorizontalAlignment = HorizontalAlignment.Center;
                us.HorizontalContentAlignment = HorizontalAlignment.Center;
                if(NumOfDishOrders%3==0)
                    Stack1.Children.Add(us);
                else if(NumOfDishOrders%3==1)
                    Stack2.Children.Add(us);
                else
                    Stack3.Children.Add(us);
                NumOfDishOrders++;
            }
        }
        private void DishOrderAmount(object sender, BE.EventValue e)
        {
            if (Convert.ToInt32(e.Value) == 0)
            { 
                BL.FactoryBL.getBL().DeleteDishOrder(Convert.ToInt32(e.pName));
                RefreshStacks(this, null);
                return;
            }
            BE.DishOrder ds= BL.FactoryBL.getBL().GetAllDishOrders(item => item.ID.ToString() == e.pName).FirstOrDefault(); //error if there isnt DishOrder
            ds.DishAmount =Convert.ToInt32(e.Value);
            BL.FactoryBL.getBL().UpdateDishOrder(ds);
        }
    }
}
