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
        public MainInterface()
        {
            try
            {
                InitializeComponent();
                if (MessageBox.Show("would you like to reset the database?", "DataBase Restart", MessageBoxButton.YesNo, MessageBoxImage.Error, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    BL.FactoryBL.getBL().DeleteDataBase();
                    BL.FactoryBL.getBL().Inti();
                }
                if (!BL.FactoryBL.getBL().GetAllUsers(item => item.Type == BE.UserType.NetworkManger).Any())
                    MessageBox.Show("Hello! and wellcom to Shy and Ezra program for manging Fred's BBQ Joint\n you will now be redirected to create the First Network Manger account \n please pay attention to this process", "Hello World!", MessageBoxButton.OK, MessageBoxImage.Information);
                //Open a new Window manger window
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Error");
            }
        }
        private void NextLogin(object sender, RoutedEventArgs e)
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
                                MessageBox.Show("The username and CreditCard you entered don't match. Please try again or contact a staff member");
                        }
                        if ((!IsLostPassword) && MessageBoxResult.Yes == MessageBox.Show("The username and password you entered don't match.\nDid you forgot your password?", "Incorrect password", MessageBoxButton.YesNo, MessageBoxImage.Error))
                        {
                            if (user.Type == BE.UserType.Client)
                            {
                                MessageBox.Show("Enter your credit card number in the password filed below", "Password Recovry");
                                IsLostPassword = true;
                            }
                            else
                                MessageBox.Show("We cant do anything for you, please try to contact your superior");
                        }

                        InputPassword.Clear();
                    }
                    #endregion
                    break;
            }

        }
        private void ChangeToLogin()
        {
            backArrow.Visibility = Visibility.Visible;
            InputPassword.Visibility = Visibility.Visible;
            FocusManager.SetFocusedElement(this, InputPassword.textBox);
        }

        //private void passwordLabelBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if ((sender as PasswordBox).Password == "" || backArrow.Visibility==Visibility.Hidden)
        //    {
        //        (sender as PasswordBox).Visibility = Visibility.Hidden;
        //        passwordLabelBox.Text = null;
        //        //TextBox_LostFocus(passwordLabelBox, null);
        //        passwordLabelBox.Visibility = Visibility;
        //        InputPassword.Password = null;

        //    }
        //    else
        //    {
        //        passwordLabelBox.Text = InputPassword.Password;
        //    }
        //}

        private void createAccountButton_Click(object sender, RoutedEventArgs e)
        {
            //open new client window
            new ClientEditor().ShowDialog();
        }

        private void backArrow_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
            backArrow.Visibility = Visibility.Hidden;
            InputPassword.Clear();
            InputPassword.Visibility = Visibility.Hidden;
            Keyboard.Focus(SignInButton);
            IsLostPassword = false;
        }

        private void InputPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                NextLogin(sender, e);
            if (e.Key == Key.Back && (InputPassword.ForeG == Brushes.Gray && backArrow.Visibility == Visibility.Visible))
                backArrow_Click(backArrow, null);

        }
    }
}

