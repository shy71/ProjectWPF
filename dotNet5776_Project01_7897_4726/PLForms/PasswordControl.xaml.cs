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
using System.ComponentModel;

namespace PLForms
{
    /// <summary>
    /// Interaction logic for PasswordControl.xaml
    /// </summary>
    public partial class PasswordControl : UserControl,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public string Str { get; set; }
        public double FontS { get; set; }
        Brush foreG;
        public Brush ForeG 
        {
            get
            {
                return foreG;
            }
            set
            {
                SetField(ref foreG, value, "ForeG");
            }
        }
        public PasswordControl()
        {
            InitializeComponent();
            ForeG = Brushes.Gray;
        }
        public PasswordControl(string WaterMark)
        {
            InitializeComponent();
            Str = WaterMark;
            ForeG = Brushes.Gray;
        }

        private void passwordLabelBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "")
            {
                passwordBox.Visibility = Visibility.Hidden;
                textBox.Visibility = Visibility.Visible;
                passwordBox.Password = null;
                passwordBox.Foreground = Brushes.Gray;
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
                passwordBox.Visibility = Visibility.Visible;
                Keyboard.Focus(passwordBox);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            passwordLabelBox_LostFocus(passwordBox, null);
            if (FontS != 0)
            {
                passwordBox.FontSize = FontS;
                textBox.FontSize = FontS;
            }
        }
        private void PasswordInput(object sender, TextCompositionEventArgs e)
        {
            if (passwordBox.Foreground == Brushes.Gray && passwordBox.Password == "" && e.Text!="")
            {
                passwordBox.Foreground = Brushes.Black;
                ForeG = Brushes.Black;
            }
        }

        private void passwordBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Back&& passwordBox.Password.Length==0)
            {
                passwordBox.Foreground = Brushes.Gray;
                ForeG = Brushes.Gray;
            }
        }
    }
}
