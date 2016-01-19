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
            (sender as TextBox).Text = null;
            (sender as TextBox).Foreground = Brushes.Black;
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                var temp = (sender as TextBox).Resources.Values.GetEnumerator();
                temp.MoveNext();
                (sender as TextBox).Text = temp.Current as string;
                (sender as TextBox).Foreground = Brushes.Gray;
            }
        }
        private void NextLogin(object sender, RoutedEventArgs e)
        {
            switch (SignInButton.Content.ToString())
            {
                case "Next":
                    #region Next
                    user = BL.FactoryBL.getBL().GetUser(InputBox.Text);
                    if (user == null)
                    {
                        if (MessageBox.Show("Sorry, There isn't such username in our datdbase\n\n would you like to create a new client with that username?", "Incorrect username", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
                            new NewClient(InputBox.Text).ShowDialog();
                    }
                    else
                    {
                        this.DataContext = user;
                        ChangeToLogin();
                    }
                    InputBox.Clear();
                    TextBox_LostFocus(InputBox, null);
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
            passwordLabelBox.Visibility = Visibility.Visible;
        }

        private void passwordLabelBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as PasswordBox).Password == "" || backArrow.Visibility==Visibility.Hidden)
            {
                (sender as PasswordBox).Visibility = Visibility.Hidden;
                passwordLabelBox.Text = null;
                TextBox_LostFocus(passwordLabelBox, null);
                passwordLabelBox.Visibility = Visibility;
                InputPassword.Password = null;

            }
            else
            {
                passwordLabelBox.Text = InputPassword.Password;
            }
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
            passwordLabelBox_LostFocus(InputPassword, null);
            passwordLabelBox.Visibility = Visibility.Hidden;
        }

        private void InputPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                NextLogin(sender, e);

        }

        private void passwordLabelBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Visibility = Visibility.Hidden;
            InputPassword.Visibility = Visibility.Visible;
            FocusManager.SetFocusedElement(this, InputPassword);
        }
    }
}

