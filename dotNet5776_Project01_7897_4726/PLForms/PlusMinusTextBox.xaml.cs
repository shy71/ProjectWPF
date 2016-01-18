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
    /// Interaction logic for PlusMinusTextBox.xaml
    /// </summary>
    public partial class PlusMinusTextBox : UserControl
    {
        public PlusMinusTextBox()
        {
            InitializeComponent();
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            int num=0;
            if (int.TryParse(Number.Text, out num))
            {
                num++;
                Number.Text = num.ToString();
            }
            else
                throw new Exception("Problem with PlusMinusTextBox.");
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            int num = 0;
            if (int.TryParse(Number.Text, out num))
            {
                num--;
                Number.Text = num.ToString();
            }
            else
                throw new Exception("Problem with PlusMinusTextBox.");
        }
    }
}
