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
        bool IsReadOnly = false;
        public OrderEditor()
        {
            InitializeComponent();
        }
        public OrderEditor(BE.Order order,bool ReadOnly=false)
        {
            InitializeComponent();
            this.client = BL.FactoryBL.getBL().GetAllClients(item=>item.ID==order.ClientID).First();
            tempOrder = order;
            if (order.Address == client.Address)
                HomeCheckBox.IsChecked = true;
            else
                HomeCheckBox_UnChecked(this,null);
            if (ReadOnly)
            {
                AddImg.Visibility = Visibility.Collapsed;
                SendImg.Visibility = Visibility.Collapsed;
                HomeCheckBox.Visibility = Visibility.Collapsed;
                addressBox.IsEnabled = false;
            }
            IsReadOnly = ReadOnly;
        }

        private void HomeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            addressBox.SetText(client.Address);
        }
        private void HomeCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            if (tempOrder.Address == client.Address)
                addressBox.Clear();
            else
                addressBox.SetText(tempOrder.Address);
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
                us = new DishOrder(item,IsReadOnly);
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

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            if (addressBox.ForeG == Brushes.Gray)
            {
                MessageBox.Show("You must peek an Address!", "Problem with order");
                return;
            }
            tempOrder.Address = addressBox.GetText();
            BL.FactoryBL.getBL().UpdateOrder(tempOrder);
            this.Close();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
           var picker= new DishPicker(Max(tempOrder.Kosher,BL.FactoryBL.getBL().GetAllBranchs(item=>item.ID==tempOrder.BranchID).First().Kosher), tempOrder.ID);
           picker.Added += AddedDishs;
           picker.Show();
        }
        BE.Kashrut Max(BE.Kashrut kosher1, BE.Kashrut kosher2)
        {
            if (kosher1 > kosher2)
                return kosher1;
            return kosher2;
        }
        void AddedDishs(object sender, BE.EventValue e)
        {
            if(e.pName==tempOrder.ID.ToString())
            {
                try
                {
                    foreach (int item in (e.Value as List<int>))
                    {

                        BL.FactoryBL.getBL().AddDishOrder(new BE.DishOrder(tempOrder.ID, item));

                    }
                }
                catch (Exception)
                {
                    if (MessageBoxResult.Yes == (MessageBox.Show("Only some of the new dishs you added were added.\n all of the nw dishs togther with the old one,  would have resluted a price above the aproved limit.\n\n You can delete some dishs from your order and then to add the other dishs or you can split your order\n would like to delete all of the dishs that were added in this?", "Not all dishs were added", MessageBoxButton.YesNo, MessageBoxImage.Warning)))
                    {
                        try
                        {
                            foreach (int item in (e.Value as List<int>))
                            {
                                BL.FactoryBL.getBL().DeleteDishOrder(new BE.DishOrder(tempOrder.ID, item));

                            }
                        }
                        catch
                        {

                        }
                    }
                }
                RefreshStacks(this, null);
            }
        }


    }
}
