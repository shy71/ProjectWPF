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
using System.ComponentModel;

namespace PLForms
{
    /// <summary>
    /// Interaction logic for TextControl.xaml
    /// </summary>
    public partial class TextControl : UserControl, INotifyPropertyChanged
    {
        public event EventHandler<BE.EventValue> Changed;

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
        public string pName { get; set; }
        Brush foreG;
        [Bindable(true)]
        public Brush ForeG 
        { 
            get
            {
                return foreG;
            }
            set
            {
                SetField(ref foreG, value, "foreG");
            }
        }
   
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
                ForeG = Brushes.Gray;
            }
        }
        public string GetText()
        {
            if (textBox.Foreground == Brushes.Gray)
                return null;
            return textBox.Text;
        }
        public void SetText(string str)
        {
            textBox_GotFocus(textBox, null);
            textBox.Text = str;
        }
        public void Clear()
        {
            textBox.Clear();
            textBox_LostFocus(textBox, null);
        }
        private void textBox_Loaded(object sender, RoutedEventArgs e)
        {
            textBox_LostFocus(textBox, null);
            if(FontS!=0)
            textBox.FontSize = FontS;
        }

        private void UserControl_TextInput(object sender, TextChangedEventArgs e)
        {
            if (textBox.Foreground == Brushes.Gray && textBox.Text != "")
            {
                textBox.Foreground = Brushes.Black;
                ForeG = Brushes.Black;
            }
            else if(textBox.Text =="")
            {
                textBox.Foreground = Brushes.Gray;
                ForeG = Brushes.Gray;
            }
            if (textBox.Foreground == Brushes.Black && Changed != null)
                Changed(this, new BE.EventValue(textBox.Text, pName));
        }

    }
}
