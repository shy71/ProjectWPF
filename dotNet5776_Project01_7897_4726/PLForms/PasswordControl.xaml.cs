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

namespace PLForms
{
    /// <summary>
    /// Interaction logic for PasswordControl.xaml
    /// </summary>
    public partial class PasswordControl : UserControl
    {
        public string Str { get; set; }
        public PasswordControl()
        {
            InitializeComponent();
        }
        public PasswordControl(string WaterMark)
        {
            InitializeComponent();
            Str = WaterMark;
        }

        private void passwordLabelBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "")
            {
                passwordBox.Visibility = Visibility.Hidden;
                textBox.Visibility = Visibility.Visible;
                passwordBox.Password = null;
                textBox.Foreground = Brushes.Gray;
                textBox.Text = Str;

            }
            else
            {
                textBox.Text = passwordBox.Password;
            }
        }
        public string GetPassword()
        {
            if (passwordBox.Password == "")
                return null;
            return passwordBox.Password;
        }
        public void Clear()
        {
            passwordBox.Clear();
            passwordLabelBox_LostFocus(passwordBox, null);
        }
        private void passwordLabelBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "")
            {
                textBox.Visibility = Visibility.Hidden;
                textBox.Foreground = Brushes.Black;
                passwordBox.Visibility = Visibility.Visible;
                Keyboard.Focus(passwordBox);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            passwordLabelBox_LostFocus(passwordBox, null);
        }
    }
}
