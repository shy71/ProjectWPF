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
    /// Interaction logic for MangeOrder.xaml
    /// </summary>
    public partial class OrderEditor : Window
    {
        BE.Client client;
        public OrderEditor()
        {
            InitializeComponent();
        }
        public OrderEditor(BE.Client client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void HomeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            addressBox.SetText(client.Address);
        }
        private void HomeCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            addressBox.Clear();
        }
    }
}
