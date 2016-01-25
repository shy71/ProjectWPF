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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int num = 0;
            Grid g;
            ColumnDefinition a, b, c, d;
            foreach (var item in BL.FactoryBL.getBL().GetAllOrders(item2 => item2.Delivered == false))
            {
                if (num % 4 == 0)
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
                var ChildEnumrator = stackPanel.Children.GetEnumerator();
                for (int i = 0; i < ((int)(num / 4))*2 + 3; i++)
                    ChildEnumrator.MoveNext();
                (ChildEnumrator.Current as Grid).Children.Add(orderD);
                Grid.SetColumn(orderD, num % 4);
                num++;
            }
             if (num % 4 == 0)
            {
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
             for (int i = 0; i < ((int)(num / 4)) * 2 + 3; i++)
                 tempEnumrator.MoveNext();
             BigGrid.Children.Remove(add);
             (tempEnumrator.Current as Grid).Children.Add(add);
             Grid.SetColumn(add, 3);
        }
    }
}
