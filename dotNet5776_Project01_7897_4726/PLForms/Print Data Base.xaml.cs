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
    /// Interaction logic for Print_Data_Base.xaml
    /// </summary>
    public partial class Print_Data_Base : Window
    {
        public Print_Data_Base()
        {
            InitializeComponent();
            try
            {
                ClearToBase();
            }
            catch (Exception Exp)
            {
                MessageBox.Show(Exp.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        internal void ClearToBase()
        {
            BL.IBL myBL = BL.FactoryBL.getBL();
            RefreshDishs(myBL.GetAllDishs());
            RefreshBranchs(myBL.GetAllBranchs());
            RefreshClients(myBL.GetAllClients());
            RefreshOrders(myBL.GetAllOrders());
            Refresh();
        }
        void RefreshDishs(IEnumerable<BE.Dish> list)//לנסות להפוך לגנרי
        {
            Expander exp;
            DishExp.Header = "Dishs";
            DishStack.Children.Clear();
            foreach (BE.Dish item in list)
            {
                exp = new Expander();
                exp.Content = new TextBox
                {
                    Foreground = Brushes.Black,
                    Text = item.ToString(),
                    TextAlignment = TextAlignment.Left,
                    TextWrapping = TextWrapping.Wrap,
                    IsReadOnly = true,
                    Background = Brushes.Transparent,
                    BorderThickness = new Thickness()
                    {
                        Top = 0,
                        Bottom = 0,
                        Left = 0,
                        Right = 0
                    }
                };
                exp.Header = item.Name;
                DishStack.Children.Add(exp);
            }
            if (DishStack.Children.Count == 0)
                DishExp.Visibility = Visibility.Collapsed;
            else
                DishExp.Visibility = Visibility.Visible;
        }
        void RefreshOrders(IEnumerable<BE.Order> list)
        {
            Expander exp;
            OrderExp.Header = "Orders";
            OrderStack.Children.Clear();
            foreach (BE.Order item in list)//פתרון לא הכי יעיל
            {
                exp = new Expander();
                exp.Content = new TextBox
                {
                    Foreground = Brushes.Black,
                    Text = item.ToString(),
                    TextAlignment = TextAlignment.Left,
                    TextWrapping = TextWrapping.Wrap,
                    IsReadOnly = true,
                    Background = Brushes.Transparent,
                    BorderThickness = new Thickness()
                    {
                        Top = 0,
                        Bottom = 0,
                        Left = 0,
                        Right = 0
                    }
                };
                exp.Header = item.Address + " - " + ((item.Date == DateTime.MinValue) ? "Not Sent" : item.Date.ToShortDateString());
                OrderStack.Children.Add(exp);
            }
            if (OrderStack.Children.Count == 0)
                OrderExp.Visibility = Visibility.Collapsed;
            else
                OrderExp.Visibility = Visibility.Visible;
        }
        void RefreshBranchs(IEnumerable<BE.Branch> list)
        {
            Expander exp;
            BranchExp.Header = "Branchs";
            BranchStack.Children.Clear();
            foreach (BE.Branch item in list)
            {
                exp = new Expander();
                exp.Header = item.Name;
                exp.Content = new TextBox
                {
                   Foreground = Brushes.Black,
                   Text = item.ToString(),
                   TextAlignment = TextAlignment.Left,
                   TextWrapping = TextWrapping.Wrap,
                   IsReadOnly = true,
                   Background = Brushes.Transparent,
                   BorderThickness = new Thickness()
                         {
                             Top = 0,
                             Bottom = 0,
                             Left = 0,
                             Right = 0
                         }
                };
                //Background="Transparent" BorderThickness="0" Foreground="Black" IsReadOnly="True" TextWrapping="Wrap" FontFamily="Comic Sans MS"
                BranchStack.Children.Add(exp);
            }
            if (BranchStack.Children.Count == 0)
                BranchExp.Visibility = Visibility.Collapsed;
            else
                BranchExp.Visibility = Visibility.Visible;
        }
        void RefreshClients(IEnumerable<BE.Client> list)
        {
            Expander exp;
            ClientExp.Header = "Clients";
            ClientStack.Children.Clear();
            foreach (BE.Client item in list)
            {
                exp = new Expander();
                exp.Content = new TextBox
                {
                    Foreground = Brushes.Black,
                    Text = item.ToString(),
                    TextAlignment = TextAlignment.Left,
                    TextWrapping = TextWrapping.Wrap,
                    IsReadOnly = true,
                    Background = Brushes.Transparent,
                    BorderThickness = new Thickness()
                    {
                        Top = 0,
                        Bottom = 0,
                        Left = 0,
                        Right = 0
                    }
                };
                exp.Header = item.Name;
                ClientStack.Children.Add(exp);
            }
            if (ClientStack.Children.Count == 0)
                ClientExp.Visibility = Visibility.Collapsed;
            else
                ClientExp.Visibility = Visibility.Visible;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SearchBing_Changed(object sender, BE.EventValue e)
        {
            string str = SearchBing.GetText();
            if (SearchBing.ForeG != Brushes.Gray)
            {
                if (SearchBing.GetText() == null || SearchBing.GetText() == "" || SearchBing.GetText() == SearchBing.Str)
                    ClearToBase();
                else
                {
                    RefreshBranchs(BL.FactoryBL.getBL().SearchBranchs(str));
                    RefreshClients(BL.FactoryBL.getBL().SearchClients(str));
                    RefreshDishs(BL.FactoryBL.getBL().SearchDishs(str));
                    RefreshOrders(BL.FactoryBL.getBL().SearchOrders(str));
                    Refresh();
                }
            }
            else
                ClearToBase();
        }
        void Refresh()
        {
            if (DishStack.Children.Count + OrderStack.Children.Count + BranchStack.Children.Count + ClientStack.Children.Count <= 5)
                foreach (Expander item in MainStack.Children)
                {
                    item.IsExpanded = true;
                    foreach (Expander item2 in (item.Content as StackPanel).Children)
                        item2.IsExpanded = true;
                }
            if (SearchBing.ForeG != Brushes.Gray && SearchBing.GetText() != null && SearchBing.GetText() != "" && SearchBing.GetText() != SearchBing.Str)
                foreach (Expander item in MainStack.Children)
                    item.IsExpanded = true;
            else
                foreach (Expander item in MainStack.Children)
                    item.IsExpanded = false;
        }
    }
}
