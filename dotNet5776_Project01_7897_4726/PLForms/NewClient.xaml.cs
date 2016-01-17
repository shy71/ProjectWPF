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
 * אפשרות להוסיף משתמש מההערה
 * אדמין אוטומטי בהרצה ראשונה
 * להוסיף יותר דברים לגרידים
 * 
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
                if (nextButton.Opacity != 1)
                    throw new Exception("You must filed all of the fileds!");
                if (passowrdBox1.Password != passowrdBox2.Password)
                    throw new Exception("The passwords does not match!");
                if (passowrdBox1.Password == "")
                    throw new Exception("The password cant be empty!");
                BL.FactoryBL.getBL().AddClient(client);
                user.Name=client.Name;
                user.UserName = usernameBox.Text;
                user.Password=passowrdBox1.Password;
                user.Type=BE.UserType.Client;
                user.ClientID=client.ID;
                BL.FactoryBL.getBL().AddUser(user);
                MessageBox.Show("The account "+user.UserName+" was created!", "Account created", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
                
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Problem with account", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
