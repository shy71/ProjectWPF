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
            InitializeComponent();
            BL.FactoryBL.getBL().Inti();
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
                (sender as TextBox).Text = null;
                (sender as TextBox).Foreground = Brushes.Black;
        }

        private void NextLogin(object sender, RoutedEventArgs e)
        {
            if (InputBox.Text == "" || InputBox.Foreground==Brushes.Gray)
            {
                return;
            }
            user = BL.FactoryBL.getBL().getUser(InputBox.Text);
            if (user == null)
                MessageBox.Show("Sorry, There isn't such username in our datdbase", "Incorrect username", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                ChangeToLogin();
        }
        private void SignIn(object sender, RoutedEventArgs e)
        {
            if (InputPassword.Password.Length == 0 || InputBox.Foreground == Brushes.Gray)
            {
                return;
            }
            else if (InputPassword.Password == user.Password);
            //enter Type Window
            else
                MessageBox.Show("The username and password you entered don't match.", "Incorrect password", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void ChangeToLogin()
        {
            InputPassword.Visibility = Visibility.Visible;
            SignInButton.Content = "Sign In";
            typeLabel.Content = user.Type;
            nameLabel.Content = user.Name;
            UsernameLabel.Content = user.UserName;
            SignInButton.Click -= NextLogin;
            SignInButton.Click += SignIn;
            backArrow.Visibility = Visibility.Visible;
            InputPassword.Password = "******";
            InputPassword.Foreground = Brushes.Gray;
        }

        private void InputPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            InputPassword.Password = "";
            InputPassword.Foreground = Brushes.Black;
        }

        private void createAccountButton_Click(object sender, RoutedEventArgs e)
        {          
            //open new client window
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            InputPassword.Visibility = Visibility.Hidden;
            SignInButton.Content = "Next";
            typeLabel.Content = null;
            nameLabel.Content = null;
            UsernameLabel.Content =null;
            SignInButton.Click -= SignIn;
            SignInButton.Click += NextLogin;
            backArrow.Visibility = Visibility.Hidden;
            backButton.Visibility = Visibility.Hidden;
            InputBox.Text = "Enter your Username";
            InputBox.Foreground = Brushes.Gray;
            createAccountButton.Visibility = Visibility.Visible;
        }
    }
}

