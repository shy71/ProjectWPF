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
    /// Interaction logic for Print_All_Clients.xaml
    /// </summary>
    public partial class Print_All_Clients : Window
    {
        public Print_All_Clients()
        {
            InitializeComponent();
            try
            {
                BL.IBL myBL = BL.FactoryBL.getBL();
                IEnumerable<BE.Client> ClientList = myBL.GetAllClients();
                foreach (BE.Client item in ClientList)
                {
                    Clients.Text += (item.ToString() + "\n");
                }
            }
            catch (Exception Exp)
            {
                Clients.Text = Exp.ToString();
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
