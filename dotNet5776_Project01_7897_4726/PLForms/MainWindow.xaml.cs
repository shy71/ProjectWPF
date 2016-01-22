﻿using System;
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
               // BL.FactoryBL.getBL().Inti();
                if (!BL.FactoryBL.getBL().GetAllUsers(item => item.Type == BE.UserType.NetworkManger).Any())
                    MessageBox.Show("Hello! and wellcom to Shy and Ezra program for manging Fred's BBQ Joint\n you will now be redirected to create the First Network Manger account \n please pay attention to this process","Hello World!",MessageBoxButton.OK,MessageBoxImage.Information);
                    //Open a new Window manger window
            }
            catch (Exception exp)
            {
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
                            new NewClient(InputBox.GetText()).ShowDialog();
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
                    else
                    {
                        //enter Type Window
                        MessageBox.Show("The username and password you entered don't match.", "Incorrect password", MessageBoxButton.OK, MessageBoxImage.Error);
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
            new NewClient().ShowDialog();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
            backArrow.Visibility = Visibility.Hidden;
            InputPassword.Clear();
            InputPassword.Visibility = Visibility.Hidden;
            Keyboard.Focus(SignInButton);
        }

        private void InputPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                NextLogin(sender, e);
            if (e.Key == Key.Back && (InputPassword.ForeG == Brushes.Gray && backArrow.Visibility == Visibility.Visible))
                backButton_Click(backButton, null);

        }

        private void Button_Click(object sender, RoutedEventArgs e)//delete
        {
            new NewBranch().Show();
            this.Close();

        }

        //private void passwordLabelBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    (sender as TextBox).Visibility = Visibility.Hidden;
        //    (sender as TextBox).Foreground = Brushes.Black;
        //    InputPassword.Visibility = Visibility.Visible;
        //   KeyBorad.Focus(InputPassword);
        //}
    }
}

