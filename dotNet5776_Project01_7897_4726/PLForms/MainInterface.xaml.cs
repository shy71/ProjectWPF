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
using System.Threading;

namespace PLForms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainInterface : Window
    {
        BE.User user;
        bool IsLostPassword;
        /// <summary>
        /// constructor
        /// </summary>
        public MainInterface()
        {
            try
            {
                
                InitializeComponent();
                try
                {
                    var ClientList = BL.FactoryBL.getBL().GetAllClients();
                    var UsertList = BL.FactoryBL.getBL().GetAllUsers();
                    var BranchList = BL.FactoryBL.getBL().GetAllBranchs();
                    var DishList=BL.FactoryBL.getBL().GetAllDishs();
                    var OrderList = BL.FactoryBL.getBL().GetAllOrders();
                    var DishOrderList = BL.FactoryBL.getBL().GetAllDishOrders();
                    foreach (BE.Client item in ClientList)
                        UsertList.First(item2 => item2.ItemID == item.ID&&item2.Type==BE.UserType.Client);
                    foreach (BE.User item in UsertList)
                        if (item.Type == BE.UserType.Client)
                            ClientList.First(item2 => item2.ID == item.ItemID);
                        else if (item.Type == BE.UserType.BranchManger)
                            BranchList.First(item2 => item2.ID == item.ItemID);
                    foreach (BE.Branch item in BranchList)
                        UsertList.First(item2=>item2.UserName==item.Boss.Substring(item.Boss.IndexOf('@')+1));
                    foreach (BE.Order item in OrderList)
                    {
                        ClientList.First(item2 => item2.ID == item.ClientID);
                        BranchList.First(item2 => item2.ID == item.BranchID);
                        if (BL.FactoryBL.getBL().PriceOfOrder(item) > BL.FactoryBL.getBL().MAX_PRICE)
                            throw new Exception();
                    }
                    foreach (BE.DishOrder item in DishOrderList)
                    {
                       var temp= OrderList.First(item2 => item2.ID == item.OrderID);
                        DishList.First(item2 => item2.ID == item.DishID &&( (temp.Delivered==false)?item2.Active==true:true));//
                    }

                  
                }
                catch
                {
                    MessageBox.Show("Somebody messed up with the XML files! please contact support");
                }
                if (!BL.FactoryBL.getBL().GetAllUsers(item => item.Type == BE.UserType.NetworkManger).Any())
                {
                    MessageBox.Show("Hello! and wellcom to Shy and Ezra program for manging Fred's BBQ Joint\n you will now be redirected to create the First Network Manger account \n please pay attention to this process", "Hello World!", MessageBoxButton.OK, MessageBoxImage.Information);
                    new UserEditor(BE.UserType.NetworkManger).ShowDialog();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Error");
            }
        }
        /// <summary>
        /// enters when the Next/(sign in) button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextLogin(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (SignInButton.Content.ToString())
                {
                    case "Next":
                        #region Next
                        user = BL.FactoryBL.getBL().GetUser(InputBox.GetText());
                        if (user == null)
                        {
                            if (MessageBox.Show("Sorry, There isn't such username in our datdbase\n\n would you like to create a new client with that username?", "Incorrect username", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
                                new ClientEditor(InputBox.GetText()).ShowDialog();
                            user = BL.FactoryBL.getBL().GetUser(InputBox.GetText());
                            if (user != null)
                            {
                                new ClientInterface(user).Show();
                                this.Close();
                            }
                            Keyboard.ClearFocus();
                        }
                        else
                        {
                            this.DataContext = user;
                            ChangeToLogin();
                        }
                        InputBox.Clear();
                        #endregion
                        break;
                    case "Sign In":
                        #region Sign In
                        if (InputPassword.GetPassword() == user.Password)
                        {
                            switch (user.Type)
                            {
                                case BE.UserType.Client:
                                    new ClientInterface(user).Show();
                                    break;
                                case BE.UserType.BranchManger:
                                    new BranchMangerInterface(user).Show();
                                    break;
                                case BE.UserType.NetworkManger:
                                    new NetworkManagerInterface(user).Show();
                                    break;
                            }
                            this.Close();
                        }
                        else
                        {
                            if (IsLostPassword == true)
                            {
                                if (InputPassword.GetPassword() == BL.FactoryBL.getBL().GetAllClients(item => item.ID == user.ItemID).First().CreditCard.ToString())
                                {
                                    MessageBox.Show("You will be redirect to save a new password, please try not to forget her");
                                    new UserEditor(user).ShowDialog();
                                    IsLostPassword = false;
                                    backArrow_Click(this, null);
                                    return;
                                }
                                else
                                    MessageBox.Show("The username and Credit Card you entered don't match. Please try again or contact a staff member");
                            }
                            if ((!IsLostPassword) && MessageBoxResult.Yes == MessageBox.Show("The username and password you entered don't match.\nDid you forgot your password?", "Incorrect password", MessageBoxButton.YesNo, MessageBoxImage.Error))
                            {
                                if (user.Type == BE.UserType.Client)
                                {
                                    MessageBox.Show("Enter your credit card number in the password filed below", "Password Recovry");
                                    IsLostPassword = true;
                                }
                                else
                                    MessageBox.Show("We can't do anything for you, please try to contact your superior");
                            }

                            InputPassword.Clear();
                        }
                        #endregion
                        break;
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                backArrow_Click(this, null);
            }
        }
        /// <summary>
        /// changes the screen to SignIn screen
        /// </summary>
        private void ChangeToLogin()
        {
            backArrow.Visibility = Visibility.Visible;
            InputPassword.Visibility = Visibility.Visible;
            FocusManager.SetFocusedElement(this, InputPassword.textBox);
            SignInButton.ToolTip = "Log in to the account";
        }
        /// <summary>
        /// opens the window for account creation when the create account button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createAccountButton_Click(object sender, RoutedEventArgs e)
        {
            //open new client window
            new ClientEditor().ShowDialog();
        }
        /// <summary>
        /// goes back to Username Screem when the back arrow is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backArrow_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
            backArrow.Visibility = Visibility.Hidden;
            InputPassword.Clear();
            InputPassword.Visibility = Visibility.Hidden;
            Keyboard.Focus(SignInButton);
            IsLostPassword = false;
            SignInButton.ToolTip = "Continue to the next step of the login";
        }
        /// <summary>
        /// checks for special keys being pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                NextLogin(sender, e);
            if (e.Key == Key.Back && (InputPassword.ForeG == Brushes.Gray && backArrow.Visibility == Visibility.Visible))
                backArrow_Click(backArrow, null);

        }
    }
}

