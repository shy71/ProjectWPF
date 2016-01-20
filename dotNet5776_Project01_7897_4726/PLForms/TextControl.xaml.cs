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
        public string Str { get; set; }
        public string Text { get; set; }
        Binding myBind=null;
        public TextControl()
        {
            InitializeComponent();
        }
        public TextControl(string  waterMark)
        {
            InitializeComponent();
            Str = waterMark;
        }
        public void SetBinding(object BindingObject, string PropertyName, BindingMode Mode = BindingMode.OneWay)
        {
            myBind = new Binding();
            myBind.Source = BindingObject;
            myBind.Path = new PropertyPath(BindingObject.GetType().GetProperty(PropertyName));
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
                textBox.Text = Str;
                textBox.Foreground = Brushes.Gray;
            }
        }
        public string GetText()
        {
            if (textBox.Foreground == Brushes.Gray)
                return null;
            return textBox.Text;
        }
        public void Clear()
        {
            textBox.Clear();
            textBox_LostFocus(textBox, null);
        }
        private void textBox_Loaded(object sender, RoutedEventArgs e)
        {
            textBox_LostFocus(textBox, null);
        }

        private void UserControl_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (textBox.Foreground == Brushes.Gray)
                Text = null;
            else
                Text = textBox.Text;
        }

    }
}
