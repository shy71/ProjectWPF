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
/*
להוסיף אייקון
להוסיף קידוד לסיסמא
האם כל השגיאות בהודעה נפתחת או בהערה
*/
namespace PLForms
{
    /// <summary>
    /// Interaction logic for NewClient.xaml
    /// </summary>
    public partial class NewClient : Window
    {
        BE.Client client;
        BE.User user;
        public NewClient()
        {
           
            InitializeComponent();
            client = new BE.Client();
            user = new BE.User();
            this.DataContext = client;
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int a=5;
                
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Problem with account", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
