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
    /// Interaction logic for TextControl.xaml
    /// </summary>
    public partial class TextControl : UserControl
    {
        string str;
        Binding myBind=null;
        public TextControl()
        {
            InitializeComponent();
        }
        public TextControl(string  waterMark)
        {
            InitializeComponent();
        }
        public TextControl(string WaterMark, object BindingObject, string PropertyName, BindingMode Mode = BindingMode.OneWay)
        {
            InitializeComponent();
            str = WaterMark;
            myBind = new Binding();
            myBind.Source = BindingObject;
            myBind.Path =new PropertyPath(BindingObject.GetType().GetProperty(PropertyName));
            myBind.Mode = Mode;
            myBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;


        }
        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBox.Foreground == Brushes.Gray)
            {
                (sender as TextBox).Text = null;
                (sender as TextBox).Foreground = Brushes.Black;
                if (myBind != null)
                    textBox.SetBinding(TextBox.TextProperty, myBind);
                if (textBox.Text == "0")
                    (sender as TextBox).Text = null;
            }
        }
        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == "")
            {
                BindingOperations.ClearBinding(textBox, TextBox.TextProperty);
                textBox.Text = str;
                textBox.Foreground = Brushes.Gray;
            }
        }
    }
}
