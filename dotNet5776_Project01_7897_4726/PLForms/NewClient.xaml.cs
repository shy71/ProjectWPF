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
    /// Interaction logic for NewClient.xaml
    /// </summary>
    public partial class NewClient : Window
    {
        public NewClient()
        {
            InitializeComponent();
        }

        private void Box_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = null;
            (sender as TextBox).Foreground = Brushes.Black;
        }

        private void passwordLabel1_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordLabel1.Visibility = Visibility.Hidden;
            passowrdBox1.Foreground = Brushes.Black;
        }
        private void passwordLabel2_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordLabel2.Visibility = Visibility.Hidden;
            passowrdBox2.Foreground = Brushes.Black;
        }
    }
}
