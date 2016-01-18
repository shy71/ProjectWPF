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
להוסיף אייקון למסעדה
להוסיף קידוד לסיסמא
האם כל השגיאות בהודעה נפתחת או בהערה
 * אפשרות במקרה שלא ידעת את השם משתמש לחפש לפי פרטים אחרים על הבן אדם
 * אפשרות שכחתי סיסמא - לבקש ממנהל מערכת לשחזר לך אותה
 * להוסיף יותר דברים לגרידים
 * באג-לבדוק לגבי שגיאות
 * 
 * לבדוק לגבי שמות בצד או בתוך השדות
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
        public NewClient(string str)
        {

            InitializeComponent();
            client = new BE.Client();
            user = new BE.User();
            usernameBox.Text = str;
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

        private void PlusMinusTextBox_Changed(object sender, EventValue e)
        {
            if (client != null)
                client.Age =Convert.ToInt32(e.Value);
        }

        //private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    (sender as TextBox).Text = null;
        //    (sender as TextBox).Foreground = Brushes.Black;
        //}
        //private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var temp = (sender as TextBox).Resources.Values.GetEnumerator();
        //    temp.MoveNext();
        //    (sender as TextBox).Text = temp.Current as string;
        //    (sender as TextBox).Foreground = Brushes.Gray;
        //}
    }
}
