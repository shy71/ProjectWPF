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
    /// Interaction logic for OrderEditor1.xaml
    /// </summary>
    public partial class OrderEditor1 : Window
    {
        BE.Client client;
        public OrderEditor1()
        {
            InitializeComponent();
        }
        public OrderEditor1(BE.Client client):this()
        {
            this.client = client;
        }
        private void KashrutCombo_Loaded(object sender, RoutedEventArgs e)
        {
            KashrutCombo.ItemsSource = typeof(BE.Kashrut).GetEnumNames();
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp;
            if (branchCombo.Items.Count != 0)
                branchCombo.Items.Clear();
            foreach (BE.Branch item in BL.FactoryBL.getBL().GetAllBranchs(item=>item.Kosher>=BE.Extensions.ToKashrut(KashrutCombo.SelectedItem.ToString())))
            {
                temp = new ComboBoxItem();
                temp.Content = item.Name + " - " + item.Address +" | "+item.Kosher;
                temp.ToolTip = item.ToString();
                branchCombo.Items.Add(temp);
            }
            if (branchCombo.Items.Count == 0)
            {
                branchCombo.IsEnabled = false;
                branchCombo.ToolTip = "There isnt any branchs to pick from! Ask a staff member when they will open there first Branch!";
            }
        }

        private void KashrutCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh(this, null);
            branchCombo.SelectedItem = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string temp;
            BE.Order order;
                temp = (branchCombo.SelectedItem as ComboBoxItem).ToolTip.ToString();
                order = new BE.Order(Convert.ToInt32(temp.Substring(temp.IndexOf("ID: ") + 4, temp.IndexOf("\n\tName:") - temp.IndexOf("ID: ") - 4)),
                                               client.Address, BE.Extensions.ToKashrut(KashrutCombo.SelectedItem.ToString()), client.ID);//עובד רק עם ה by ref...
                BL.FactoryBL.getBL().AddOrder(order);
            this.Hide();
            new OrderEditor(order).ShowDialog();
            this.Close();
           
        }

    }
}   
