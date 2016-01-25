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
    /// Interaction logic for ClientInterface.xaml
    /// </summary>
    public partial class ClientInterface : Window
    {
        int numOfOrders = 0;
        BE.User user;
        public ClientInterface()
        {
            InitializeComponent();
        }
        public ClientInterface(BE.User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void Button_Drop(object sender, DragEventArgs e)
        {
            int a;
        }
        private void Restart(object sender, BE.EventValue e)
        {
            Clear_window();
            if (DeliveredButton.IsChecked == true)
                DeliveredButton_Checked(DeliveredButton, null);
            else if (ActiveButton.IsChecked == true)
                ActiveButton_Checked(ActiveButton, null);
            else
                UnsentButton_Checked(UnsentButton, null);
                
        }
        private void Window_Loaded(object sender,RoutedEventArgs e)
        {
            UnsentButton.IsChecked = true;
        }
        void Clear_window()
        {
            stackPanel.Children.RemoveRange(1, stackPanel.Children.Count - 1);
            numOfOrders = 0;
            add.Visibility = Visibility.Hidden;
            if (add.Parent != null)
                (add.Parent as Grid).Children.Remove(add);
        }
        private void Window_Loaded_Active(RadioButton sender,Func<BE.Order,bool> predicate)
        {
            Grid g;
            ColumnDefinition a, b, c, d;
            foreach (var item in BL.FactoryBL.getBL().GetAllOrders(predicate))
            {
                if (numOfOrders % 4 == 0)
                {
                    stackPanel.Children.Add(new Label());
                    g = new Grid();
                    a = new ColumnDefinition();
                    b = new ColumnDefinition();
                    c = new ColumnDefinition();
                    d = new ColumnDefinition();
                    c.Width = new GridLength(1, GridUnitType.Star);
                    a.Width = c.Width;
                    b.Width = c.Width;
                    d.Width = c.Width;
                    g.ColumnDefinitions.Add(a);
                    g.ColumnDefinitions.Add(b);
                    g.ColumnDefinitions.Add(c);
                    g.ColumnDefinitions.Add(d);
                    stackPanel.Children.Add(g);
                }
                var orderD = new OrderDeiltes(item);
                orderD.HorizontalAlignment = HorizontalAlignment.Center;
                orderD.Deleted += Restart;
                orderD.Sended += Restart;
                orderD.Updated += Restart;
                orderD.Arived += Restart;
                var ChildEnumrator = stackPanel.Children.GetEnumerator();
                for (int i = 0; i < ((int)(numOfOrders / 4))*2 + 3; i++)
                    ChildEnumrator.MoveNext();
                (ChildEnumrator.Current as Grid).Children.Add(orderD);
                Grid.SetColumn(orderD, numOfOrders % 4);
                numOfOrders++;
            }
            if (sender.Name == "UnsentButton")
            {
                if (numOfOrders % 4 == 0)
                {
                    stackPanel.Children.Add(new Label());
                    g = new Grid();
                    a = new ColumnDefinition();
                    b = new ColumnDefinition();
                    c = new ColumnDefinition();
                    d = new ColumnDefinition();
                    c.Width = new GridLength(1, GridUnitType.Star);
                    a.Width = c.Width;
                    b.Width = c.Width;
                    d.Width = c.Width;
                    g.ColumnDefinitions.Add(a);
                    g.ColumnDefinitions.Add(b);
                    g.ColumnDefinitions.Add(c);
                    g.ColumnDefinitions.Add(d);
                    stackPanel.Children.Add(g);
                }
                var tempEnumrator = stackPanel.Children.GetEnumerator();
                for (int i = 0; i < ((int)(numOfOrders / 4)) * 2 + 3; i++)
                    tempEnumrator.MoveNext();
                add.Visibility = Visibility.Visible;
                (tempEnumrator.Current as Grid).Children.Add(add);
                Grid.SetColumn(add, 3);
            }
        }

        private void UnsentButton_Checked(object sender, RoutedEventArgs e)
        {
            Clear_window();
            Title.Content = "Unsent orders";
            Window_Loaded_Active(sender as RadioButton, item => item.Date == DateTime.MinValue && !item.Delivered);
        }
        private void ActiveButton_Checked(object sender, RoutedEventArgs e)
        {
            Clear_window();
            Title.Content = "Active orders";
            Window_Loaded_Active(sender as RadioButton, item => item.Date != DateTime.MinValue && !item.Delivered);
        }
        private void DeliveredButton_Checked(object sender, RoutedEventArgs e)
        {
            Clear_window();
            Title.Content = "Delivered orders";
            Window_Loaded_Active(sender as RadioButton, item =>item.Delivered);
        }

        private void Unsent_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UnsentButton.IsChecked = true;
        }

        private void Active_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ActiveButton.IsChecked = true;
        }

        private void Deliverd_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            DeliveredButton.IsChecked = true;
        }
       
    }
}
