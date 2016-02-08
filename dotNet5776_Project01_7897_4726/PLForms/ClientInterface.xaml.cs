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
using System.Threading;
using System.Windows.Threading;

namespace PLForms
{

    /// <summary>
    /// Interaction logic for ClientInterface.xaml
    /// </summary>
    public partial class ClientInterface : Window
    {
        public event EventHandler<BE.EventValue> Sending;
        List<Window> subWin = new List<Window>();
        int numOfOrders = 0;
        BE.User user;
        bool IsCtrlDown = false;
        /// <summary>
        /// constructor
        /// </summary>
        public ClientInterface()
        {
            InitializeComponent();
            //error
            throw new Exception("You cant open a user interface without a user!");
        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="user"></param>
        public ClientInterface(BE.User user)
        {
            InitializeComponent();
            Sending += OrderDetails_Sending;
            Sending += OpenOrder;
            if (user.Type != BE.UserType.Client)
                throw new Exception("You cant open client interface with a user that isnt a client!");
            this.user = user;
            MainTitle.Content = "Hello " + user.Name + "!";
        }
        /// <summary>
        /// restarts the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            foreach (object grid in stackPanel.Children)
            {
                if (grid.GetType() == typeof(Grid))
                {
                    foreach (object item in (grid as Panel).Children)
                    {
                        if (item.GetType() == typeof(OrderDeiltes))
                        {
                            (item as OrderDeiltes).Opacity = 0.7;
                            //(item as OrderDeiltes).BorderThickness = new Thickness(1);
                            //(item as OrderDeiltes).BorderBrush = Brushes.Black;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// open when the window is loaded 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //UnsentButton.IsChecked = true;
            if (addBtn.Parent != null)
                (addBtn.Parent as Grid).Children.Remove(addBtn);
            LittleTitle.Content = "";
        }
        /// <summary>
        /// clears the window
        /// </summary>
        void Clear_window()
        {
            SelectAtLeastOne(false);
            MainTitle.Content = user.Name + "'s account:";
            MainTitle.FontSize = 35;
            stackPanel.Children.RemoveRange(1, stackPanel.Children.Count - 1);
            numOfOrders = 0;
            addBtn.Visibility = Visibility.Hidden;
            if (addBtn.Parent != null)
                (addBtn.Parent as Grid).Children.Remove(addBtn);

        }
        /// <summary>
        /// refresh the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="predicate"></param>
        private void RefreshWindow(RadioButton sender, Func<BE.Order, bool> predicate)
        {
            Grid g;
            ColumnDefinition a, b, c, d;
            foreach (var item in BL.FactoryBL.getBL().GetAllOrders(item => item.ClientID == user.ItemID && predicate(item)).OrderBy(item => -BL.FactoryBL.getBL().PriceOfOrder(item.ID)))
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
                var orderD = new OrderDeiltes(item, false);
                orderD.HorizontalAlignment = HorizontalAlignment.Center;
                //orderD.Deleted += Restart;
                //orderD.Sended += Restart;
                //orderD.Updated += Restart;
                //orderD.Arived += Restart;
                orderD.Opacity = 0.7;
                orderD.PreviewMouseDown += MouseClick;
                var ChildEnumrator = stackPanel.Children.GetEnumerator();
                for (int i = 0; i < ((int)(numOfOrders / 4)) * 2 + 3; i++)
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
                addBtn.Visibility = Visibility.Visible;
                (tempEnumrator.Current as Grid).Children.Add(addBtn);
                Grid.SetColumn(addBtn, 3);
            }
        }
        /// <summary>
        /// enter when the UnsentButton is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnsentButton_Checked(object sender, RoutedEventArgs e)
        {
            Clear_window();
            LittleTitle.Content = "Unsent orders";
            DeleteBtn.Visibility = Visibility.Visible;
            EditBtn.Visibility = Visibility.Visible;
            EditBtn.ToolTip = "Edit The Orders";
            SendBtn.Visibility = Visibility.Visible;
            ArivedBtn.Visibility = Visibility.Collapsed;
            RefreshWindow(sender as RadioButton, item => item.Date == DateTime.MinValue && !item.Delivered);
        }
        /// <summary>
        ///enter when the ActiveButton is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveButton_Checked(object sender, RoutedEventArgs e)
        {
            Clear_window();
            LittleTitle.Content = "Active orders";
            DeleteBtn.Visibility = Visibility.Collapsed;
            EditBtn.Visibility = Visibility.Collapsed;
            SendBtn.Visibility = Visibility.Collapsed;
            ArivedBtn.Visibility = Visibility.Visible;
            RefreshWindow(sender as RadioButton, item => item.Date != DateTime.MinValue && !item.Delivered);
        }
        /// <summary>
        /// enter when the DekuveredButton is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeliveredButton_Checked(object sender, RoutedEventArgs e)
        {
            Clear_window();
            LittleTitle.Content = "Delivered orders";
            DeleteBtn.Visibility = Visibility.Visible;
            EditBtn.Visibility = Visibility.Visible;
            EditBtn.ToolTip = "Look at the specific of the order";
            SendBtn.Visibility = Visibility.Collapsed;
            ArivedBtn.Visibility = Visibility.Collapsed;
            RefreshWindow(sender as RadioButton, item => item.Delivered);
        }
        /// <summary>
        /// enter when the Edit Profile button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            Clear_window();
            LittleTitle.Content = "Profile editing is in progress";
            new ClientEditor(user).ShowDialog();
            if (UnsentButton.IsChecked != true)
                UnsentButton.IsChecked = true;
            else
                UnsentButton_Checked(UnsentButton, null);
        }
        /// <summary>
        /// enter when the expender is expanded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                foreach (var item in BL.FactoryBL.getBL().GetAllOrders(item => item.ClientID == user.ItemID && GetPredicte()(item)).OrderBy(item => -BL.FactoryBL.getBL().PriceOfOrder(item.ID)))
                {
                    exp = new Expander();
                    text = new TextBox();
                    exp.Expanded += CloseOthersExpenders;
                    text.Foreground = Brushes.Black;
                    text.FontFamily = new FontFamily("Comic Sans MS");
                    text.Background = Brushes.DarkRed;
                    text.Text = item.ToString();
                    exp.Header = BL.FactoryBL.getBL().GetAllBranchs(item2 => item2.ID == item.BranchID).First().Name + " " + item.Address;
                    exp.ToolTip = ((item.Date == DateTime.MinValue) ? "Not sended" : item.Date.ToShortDateString()) + " - " + BL.FactoryBL.getBL().PriceOfOrder(item.ID).ToString() + "$";
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
        /// <summary>
        /// closes all other expanders except for the sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="s"></param>
        void CloseOthersExpenders(object sender, EventArgs s)
        {
            foreach (object item in ((sender as Expander).Parent as StackPanel).Children)
                if (item.GetType() == typeof(Expander))
                    if (sender.GetHashCode() != item.GetHashCode())
                        (item as Expander).IsExpanded = false;
        }
        /// <summary>
        /// opens an order window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OpenOrder(object sender, EventArgs e)
        {
            var temp = (sender as Button).Resources.Values.GetEnumerator();
            temp.MoveNext();
            var us = new OrderDeiltes(BL.FactoryBL.getBL().GetAllOrders(item => item.ID == Convert.ToInt32(temp.Current)).First());
            us.Deleted += Restart;
            us.Sended += Restart;
            us.Updated += Restart;
            us.Arived += Restart;
            subWin.Add(new ShowUserControl(us));
            subWin.Last().Show();
        }
        /// <summary>
        /// returns the macthing predicate (test) for each case
        /// </summary>
        /// <returns></returns>
        Func<BE.Order, bool> GetPredicte()
        {
            if (UnsentExpender.IsExpanded)
                return item => item.Date == DateTime.MinValue && !item.Delivered;
            else if (ActiveExpender.IsExpanded)
                return item => item.Date != DateTime.MinValue && !item.Delivered;
            else if (DeliveredExpender.IsExpanded)
                return item => item.Delivered;
            return item => false;
        }
        /// <summary>
        /// returns the radio button by the expanded menu
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// deals with mouse clicking 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MouseClick(object sender, MouseButtonEventArgs e)
        {
            bool WasSelected = (sender as OrderDeiltes).Opacity == 1;
            if (!IsCtrlDown)
                foreach (object grid in stackPanel.Children)
                    if (grid.GetType() == typeof(Grid))
                        foreach (object item in (grid as Panel).Children)
                            if (item.GetType() == typeof(OrderDeiltes))
                                (item as OrderDeiltes).Opacity = 0.7;
            if (!WasSelected)
                (sender as OrderDeiltes).Opacity = 1;
            else if (IsCtrlDown)
                (sender as OrderDeiltes).Opacity = 0.7;
            if (!DoesSelect())
                SelectAtLeastOne(false);
            else if (DeleteBtn.IsEnabled == false)
                SelectAtLeastOne(true);
        }
        /// <summary>
        /// enter and acts in the cases that there is at least one(IsSelected=true) and that there isnt(IsSelected=false);
        /// </summary>
        /// <param name="IsSelected"></param>
        void SelectAtLeastOne(bool IsSelected)
        {
            if (IsSelected)
            {
                DeleteBtn.IsEnabled = true;
                EditBtn.IsEnabled = true;
                SendBtn.IsEnabled = true;
                ArivedBtn.IsEnabled = true;
                DeleteBtn.Opacity = 1;
                EditBtn.Opacity = 1;
                SendBtn.Opacity = 1;
                ArivedBtn.Opacity = 1;
            }
            else
            {
                DeleteBtn.IsEnabled = false;
                EditBtn.IsEnabled = false;
                SendBtn.IsEnabled = false;
                ArivedBtn.IsEnabled = false;
                DeleteBtn.Opacity = 0.7;
                EditBtn.Opacity = 0.7;
                SendBtn.Opacity = 0.7;
                ArivedBtn.Opacity = 0.7;
            }

        }
        /// <summary>
        /// check and report that ctrl is being held down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDownCheck(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                IsCtrlDown = true;
        }
        /// <summary>
        /// check and report that ctrl was released
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyUpCheck(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                IsCtrlDown = false;
        }
        /// <summary>
        /// checks if something is selected(at least one)
        /// </summary>
        /// <returns></returns>
        bool DoesSelect()
        {
            foreach (object grid in stackPanel.Children)
            {
                if (grid.GetType() == typeof(Grid))
                {
                    foreach (object item in (grid as Panel).Children)
                    {
                        if (item.GetType() == typeof(OrderDeiltes))
                        {
                            if ((item as OrderDeiltes).Opacity == 1)
                                return true;

                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// logs out when the log out button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to log out?", "Log out?", MessageBoxButton.YesNo))
            {
                new MainInterface().Show();
                this.Close();
            }
        }
        /// <summary>
        /// when the add order button is clicked it opens the window for that
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            new OrderEditorStep1(BL.FactoryBL.getBL().GetAllClients(item => item.ID == user.ItemID).First()).ShowDialog();
            UnsentButton_Checked(UnsentButton, null);
        }
        /// <summary>
        /// deletes selected orders when the delete button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(((DeliveredButton.IsChecked == true) ? "It is recommended not to delete deliverd orders! without them you will not get the full exprince the resturant has to offer\n" : "") + "Are you sure you want to delete this orders?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No))
                foreach (object grid in stackPanel.Children)
                {
                    if (grid.GetType() == typeof(Grid))
                    {
                        foreach (object item in (grid as Panel).Children)
                        {
                            if (item.GetType() == typeof(OrderDeiltes))
                            {
                                if ((item as OrderDeiltes).Opacity == 1)
                                    (item as OrderDeiltes).DeleteOrder();
                                //(item as OrderDeiltes).BorderThickness = new Thickness(1);
                                //(item as OrderDeiltes).BorderBrush = Brushes.Black;
                            }
                        }
                    }
                }
            Restart(this, null);
        }
        /// <summary>
        /// edits all orders selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (object grid in stackPanel.Children)
            {
                if (grid.GetType() == typeof(Grid))
                {
                    foreach (object item in (grid as Panel).Children)
                    {
                        if (item.GetType() == typeof(OrderDeiltes))
                        {
                            if ((item as OrderDeiltes).Opacity == 1)
                                (item as OrderDeiltes).UpdateOrder();
                            //(item as OrderDeiltes).BorderThickness = new Thickness(1);
                            //(item as OrderDeiltes).BorderBrush = Brushes.Black;
                        }
                    }
                }
            }
            Restart(this, null);
        }
        /// <summary>
        /// sends all orders selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to send all of this orders?", "Send Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes))
            {
                foreach (object grid in stackPanel.Children)
                {
                    if (grid.GetType() == typeof(Grid))
                    {
                        foreach (object item in (grid as Panel).Children)
                        {
                            if (item.GetType() == typeof(OrderDeiltes))
                            {
                                if ((item as OrderDeiltes).Opacity == 1)
                                {
                                    //Sending(sender, new BE.EventValue((item as OrderDeiltes).Content));
                                    (item as OrderDeiltes).SendOrder();
                                }
                            }
                        }
                    }
                }
            }
            Restart(this, null);
        }
        /// <summary>
        /// states that specific orders were delivered when the user presses the delivered button for the orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArivedBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (object grid in stackPanel.Children)
            {
                if (grid.GetType() == typeof(Grid))
                {
                    foreach (object item in (grid as Panel).Children)
                    {
                        if (item.GetType() == typeof(OrderDeiltes))
                        {
                            if ((item as OrderDeiltes).Opacity == 1)
                                (item as OrderDeiltes).ArivedOrder();
                            //(item as OrderDeiltes).BorderThickness = new Thickness(1);
                            //(item as OrderDeiltes).BorderBrush = Brushes.Black;
                        }
                    }
                }
            }
            Restart(this, null);
        }
        private void OrderDetails_Sending(object sender, BE.EventValue value)
        {
            Thread.Sleep(5000);
            if(MessageBox.Show("An order has arrived!\nWould you like to open it now?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                OpenOrder(sender, value);
        }
    }
}
