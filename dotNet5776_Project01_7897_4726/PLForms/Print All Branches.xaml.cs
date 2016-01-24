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
    /// Interaction logic for Print_All_Branches.xaml
    /// </summary>
    public partial class Print_All_Branches : Window
    {
        public Print_All_Branches()
        {
            InitializeComponent();
            try
            {
                BL.IBL myBL = BL.FactoryBL.getBL();
                IEnumerable<BE.Branch> BranchList = myBL.GetAllBranchs();
                foreach (BE.Branch item in BranchList)
                {
                    Branches.Text += item.ToString();
                }
            }
            catch(Exception Exp)
            {
                Branches.Text = Exp.ToString();
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
