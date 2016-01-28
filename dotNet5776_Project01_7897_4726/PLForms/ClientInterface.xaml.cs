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
        List<Window> subWin=new List<Window>();
        int numOfOrders = 0;
        BE.User user;
        public ClientInterface()
        {
            InitializeComponent();
            //erroe
        }
        public ClientInterface(BE.User user)
        {
            InitializeComponent();
            this.user = user;
            MainTitle.Content = "Hello " + user.Name+"!";
        }

        private void Button_Drop(object sender, DragEventArgs e)
        {
            int a;
        }
        private void Restart(object sender, BE.EventValue e)
        {
            foreach (Window item in subWin)
                item.Close();//שיסגר רק החלון שעשה את הריסטרט
            Clear_window();
            if (DeliveredButton.IsChecked == true)
                DeliveredButton_Checked(DeliveredButton, null);
            else if (ActiveButton.IsChecked == true)
                ActiveButton_Checked(ActiveButton, null);
            else
                UnsentButton_Checked(UnsentButton, null);
            foreach (object item in MenuStack.Children)
                if (item.GetType() == typeof(Expander))
                        (item as Expander).IsExpanded = false;    
        }
        private void Window_Loaded(object sender,RoutedEventArgs e)
        {
            //UnsentButton.IsChecked = true;
            if (add.Parent != null)
                (add.Parent as Grid).Children.Remove(add);
            LittleTitle.Content = "";
        }
        void Clear_window()
        {
            MainTitle.Content = user.Name + "'s account:";
            MainTitle.FontSize = 35;
            stackPanel.Children.RemoveRange(1, stackPanel.Children.Count - 1);
            numOfOrders = 0;
            add.Visibility = Visibility.Hidden;
            if (add.Parent != null)
                (add.Parent as Grid).Children.Remove(add);

        }
        void DeleteMsg(object sender, EventArgs e)
        {
            MessageBox.Show("It is recommended not to delete deliverd orders! without them you will not get the full exprince the resturant has to offer","Not recommended");
        }
        private void Window_Loaded_Active(RadioButton sender,Func<BE.Order,bool> predicate)
        {
            Grid g;
            ColumnDefinition a, b, c, d;
            foreach (var item in BL.FactoryBL.getBL().GetAllOrders(item=>item.ClientID==user.ItemID&&predicate(item)))
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
                if (sender.Name == "DeliveredButton")
                    orderD.TryDelete += DeleteMsg;
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
            LittleTitle.Content = "Unsent orders";
            Window_Loaded_Active(sender as RadioButton, item => item.Date == DateTime.MinValue && !item.Delivered);
        }
        private void ActiveButton_Checked(object sender, RoutedEventArgs e)
        {
            Clear_window();
            LittleTitle.Content = "Active orders";
            Window_Loaded_Active(sender as RadioButton, item => item.Date != DateTime.MinValue && !item.Delivered);
        }
        private void DeliveredButton_Checked(object sender, RoutedEventArgs e)
        {
            Clear_window();
            LittleTitle.Content = "Delivered orders";
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

        private void Deliverd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeliveredButton.IsChecked = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clear_window();
            LittleTitle.Content = "Profile editing is in progress";
            new ClientEditor(user).ShowDialog();
            if (UnsentButton.IsChecked != true)
                UnsentButton.IsChecked = true;
            else
                UnsentButton_Checked(UnsentButton, null);
            

        }

        private void Expender_Expanded(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == sender)
            {
                CloseOthersExpenders(sender, null);
                GetRadioChecked().IsChecked = true;
                (((sender as Expander).Content as ScrollViewer).Content as StackPanel).Children.RemoveRange(0, (((sender as Expander).Content as ScrollViewer).Content as StackPanel).Children.Count);
                Expander exp;
                StackPanel temp;
                TextBox text;
                Button btn;
                foreach (var item in BL.FactoryBL.getBL().GetAllOrders(item => item.ClientID == user.ItemID && GetPredicte()(item)))
                {
                    exp = new Expander();
                    text = new TextBox();
                    exp.Expanded += CloseOthersExpenders;
                    text.Foreground = Brushes.Black;
                    text.FontFamily=new FontFamily("Comic Sans MS");
                    text.Background =Brushes.DarkRed;
                    text.Text = item.ToString();
                    exp.Header = BL.FactoryBL.getBL().GetAllBranchs(item2 => item2.ID == item.BranchID).First().Name + " " + item.Address;
                    exp.ToolTip = (item.Date == DateTime.MinValue) ? "Not sended" : item.Date.ToShortDateString();
                    btn = new Button();
                    btn.Content = "Open Order";
                    btn.Click += OpenOrder;
                    btn.Resources.Add("ID", item.ID);
                    temp = new StackPanel();
                    temp.Children.Add(text);
                    temp.Children.Add(btn);
                    exp.Content = temp;
                    (((sender as Expander).Content as ScrollViewer).Content as StackPanel).Children.Add(exp);
                }
            }
        }
        void CloseOthersExpenders(object sender, EventArgs s)
        {
            foreach (object item in ((sender as Expander).Parent as StackPanel).Children)
                if (item.GetType() == typeof(Expander))
                    if (sender.GetHashCode() != item.GetHashCode())
                        (item as Expander).IsExpanded = false;
        }
        void OpenOrder(object sender, EventArgs e)
        {
            var temp=(sender as Button).Resources.Values.GetEnumerator();
            temp.MoveNext();
            var us = new OrderDeiltes(BL.FactoryBL.getBL().GetAllOrders(item => item.ID == Convert.ToInt32(temp.Current)).First());
            us.Deleted += Restart;
            us.Sended += Restart;
            us.Updated += Restart;
            us.Arived += Restart;
            if (DeliveredExpender.IsExpanded)
                us.TryDelete += DeleteMsg;
            subWin.Add(new ShowUserControl(us));
            subWin.Last().Show();
        }
       Func<BE.Order,bool> GetPredicte()
        {
             if(UnsentExpender.IsExpanded) 
                return item => item.Date == DateTime.MinValue && !item.Delivered;
             else if (ActiveExpender.IsExpanded)
                 return item => item.Date != DateTime.MinValue && !item.Delivered;
           else if(DeliveredExpender.IsExpanded)
               return item =>item.Delivered;
             return item=>false;
        }
       RadioButton GetRadioChecked()
       {
           if (UnsentExpender.IsExpanded)
               return UnsentButton;
           else if (ActiveExpender.IsExpanded)
               return ActiveButton;
           else if (DeliveredExpender.IsExpanded)
               return DeliveredButton;
           return null;
       }
    }
}
