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
 * להוסיף פונצקית מחיקה
 * לבדוק לגבי שמות בצד או בתוך השדות
 *כאשר מוחקים מנה ישנה את ה תעודת זהות שלה בהזמנות
 *להוסיף תמונות למנות
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
            this.SetBinding();
        }
        public NewClient(string str)
        {

            InitializeComponent();
            client = new BE.Client();
            user = new BE.User();
            this.SetBinding();
            usernameBox.SetText(str);
        }
        void SetBinding()
        {
            usernameBox.SetBinding(user, "UserName", BindingMode.TwoWay);
            nameBox.SetBinding(client, "Name", BindingMode.TwoWay);
            creditCardBox.SetBinding(client, "CreditCard", BindingMode.TwoWay);
            addressBox.SetBinding(client, "Address", BindingMode.TwoWay);
            idBox.SetBinding(client, "ID", BindingMode.TwoWay);
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (nextButton.Opacity != 1)
                //    throw new Exception("You must filed all of the fileds!");
                if (passwordBox1.GetPassword() != passwordBox1.GetPassword())
                    throw new Exception("The passwords does not match!");
                if (passwordBox1.GetPassword() == "")
                    throw new Exception("The password cant be empty!");
                BL.FactoryBL.getBL().AddClient(client);
                user.Name = client.Name;
                user.Password = passwordBox1.GetPassword();
                user.Type=BE.UserType.Client;
                user.ItemID=client.ID;
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
        //bool GotPropty(object obj,string str)
        //{
        //    if(obj.GetType().GetProperty(str)!=null)
        //        return true;
        //    return false;
            
        //}
    }
}
