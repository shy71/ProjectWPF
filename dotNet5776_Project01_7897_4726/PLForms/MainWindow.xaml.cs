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
    public partial class MainWindow : Window
    {
        BE.User user;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                BL.FactoryBL.getBL().Inti();
                if (!BL.FactoryBL.getBL().GetAllUsers(item => item.Type == BE.UserType.NetworkManger).Any())
                    MessageBox.Show("Hello! and wellcom to Shy and Ezra program for manging Fred's BBQ Joint\n you will now be redirected to create the First Network Manger account \n please pay attention to this process","Hello World!",MessageBoxButton.OK,MessageBoxImage.Information);
                    //Open a new Window manger window
            }
            catch (Exception exp)
            {
            }
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Clear();
            (sender as TextBox).Foreground = Brushes.Black;
        }
        private void NextLogin(object sender, RoutedEventArgs e)
        {
            switch (SignInButton.Content.ToString())
            {
                case "Next":
                    #region Next
                    user = BL.FactoryBL.getBL().GetUser(InputBox.Text);
                    InputBox.Clear();
                    this.DataContext = user;
                    if (user == null)
                        MessageBox.Show("Sorry, There isn't such username in our datdbase", "Incorrect username", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                        ChangeToLogin();
                    #endregion
                    break;
                case "Sign In":
                    #region Sign In
                    if (InputPassword.Password == user.Password)
                    {
                        switch (user.Type)
                        {
                            case BE.UserType.Client:
                                //open client window
                                break;
                            case BE.UserType.BranchManger:
                                //open branch manger window
                                break;
                            case BE.UserType.NetworkManger:
                                new NetworkManagerWindow(user).Show();
                                break;
                        }
                        this.Close();
                    }
                    //enter Type Window
                    else
                        MessageBox.Show("The username and password you entered don't match.", "Incorrect password", MessageBoxButton.OK, MessageBoxImage.Error);
                    #endregion
                    break;
            }

        }
        private void SignIn(object sender, RoutedEventArgs e)
        {
        }
        private void ChangeToLogin()
        {
            backArrow.Visibility = Visibility.Visible;
            InputPassword.Password = "******";
        }

        private void InputPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            InputPassword.Password = "";
            InputPassword.Foreground = Brushes.Black;
        }

        private void createAccountButton_Click(object sender, RoutedEventArgs e)
        {
            //open new client window
            new NewClient().ShowDialog();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
            backArrow.Visibility = Visibility.Hidden;
            InputBox.Foreground = Brushes.Gray;
        }
    }
}

