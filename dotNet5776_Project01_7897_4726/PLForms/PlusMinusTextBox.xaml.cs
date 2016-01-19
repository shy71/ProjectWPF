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
    public class EventValue :EventArgs
    {
        public object Value{get;set;}
        public EventValue( object value)
        {
            Value=value;
        }
    }
    /// <summary>
    /// Interaction logic for PlusMinusTextBox.xaml
    /// </summary>
    public partial class PlusMinusTextBox : UserControl
    {
       public event EventHandler<EventValue> Changed;
        int maxNum;
        public PlusMinusTextBox()
        {
            InitializeComponent();
            maxNum = 1000;
        }
        public PlusMinusTextBox(int MaxNum)
        {
            InitializeComponent();
            maxNum = MaxNum;
        }
        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            int num;
            if (int.TryParse(Number.Text, out num))
            {
                num++;
                Number.Text = num.ToString();
            }
            else
                throw new Exception("Problem with PlusMinusTextBox.");

        }

        private void Minus_Click(object sender, EventArgs e)
        {
            int num;
            if (int.TryParse(Number.Text, out num))
            {
                num--;
                Number.Text = num.ToString();
            }
            else
                throw new Exception("Problem with PlusMinusTextBox.");
        }

        private void Number_TextChanged(object sender, TextChangedEventArgs e)
        {
            int num;
            if ((!int.TryParse(Number.Text, out num)))
            {
                MessageBox.Show("You cant put letters in an only-positive-numbers filed!", "Warning!");
                Number.Text = "0";
            }
            else if (num < 0)
                Number.Text = "0";
            else if (num == 0)
                MinusButton.IsEnabled = false;
            else if ((!MinusButton.IsEnabled) && num != 0)
                MinusButton.IsEnabled = true;
            else if ((!MinusButton.IsEnabled) && num < maxNum)
                PlusButton.IsEnabled = true;
            else if (num >= maxNum)
            {
                Number.Text = maxNum.ToString();
                PlusButton.IsEnabled = false;
            }
            if (Changed != null)
                Changed(sender, new EventValue(Convert.ToInt32(Number.Text)));
        }
        private void Number_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (((Convert.ToInt32(Number.Text) + ((int)e.Delta / 100))) >= 0)
                Number.Text = ((Convert.ToInt32(Number.Text) + ((int)e.Delta / 100))).ToString();
            else
                Number.Text = "0";
        }

    }
}
