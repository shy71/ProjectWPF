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
 * אפשרות במקרה שלא ידעת את השם משתמש לחפש לפי פרטים אחרים על הבן אדם
 * אפשרות שכחתי סיסמא - לבקש ממנהל מערכת לשחזר לך אותה - completed
 *כאשר מוחקים מנה ישנה את ה תעודת זהות שלה בהזמנות - completed
 *להוסיף תמונות למנות
 *לעשות שהכפתורים יגדלו כאשר לוחצים עליהם
 *להוסיף מחיר של הזמנה בכל מיני מקומות - completed
 * חובה! לשנות גרופינג לרק לאלה שנשלחו - completed
 * לשנות מ get to first
*/
namespace PLForms
{
    /// <summary>
    /// Interaction logic for ClientEditor.xaml
    /// </summary>
    public partial class ClientEditor : Window
    {
        BE.Client client;
        BE.User user;
        bool IsUpdated = false;
        /// <summary>
        /// constructor
        /// </summary>
        public ClientEditor()
        {
            InitializeComponent();
            client = new BE.Client();
            user = new BE.User();
            this.SetBinding();
        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="str"></param>
        public ClientEditor(string str)
        {

            InitializeComponent();
            client = new BE.Client();
            user = new BE.User();
            this.SetBinding();
            usernameBox.SetText(str);
        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="user"></param>
        public ClientEditor(BE.User user)
        {

            InitializeComponent();
            IsUpdated = true;
            if (user.Type != BE.UserType.Client)
                throw new Exception("Error!");
            client = BL.FactoryBL.getBL().GetAllClients(item => item.ID == user.ItemID).FirstOrDefault();
            this.user = user;
            this.SetBinding();
            usernameBox.SetText(user.UserName);
            nameBox.SetText(user.Name);
            creditCardBox.SetText(client.CreditCard.ToString());
            addressBox.SetText(client.Address);
            idBox.SetText(client.ID.ToString());
            ageBox.SetNum(client.Age);
            idBox.IsEnabled = false;
            usernameBox.IsEnabled = false;
        }
        /// <summary>
        /// sets the bindings up with the xaml
        /// </summary>
        void SetBinding()
        {
            usernameBox.SetBinding(user, "UserName", BindingMode.TwoWay);
            nameBox.SetBinding(client, "Name", BindingMode.TwoWay);
            creditCardBox.SetBinding(client, "CreditCard", BindingMode.TwoWay);
            addressBox.SetBinding(client, "Address", BindingMode.TwoWay);
            idBox.SetBinding(client, "ID", BindingMode.TwoWay);
        }
        /// <summary>
        /// checks when next button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (!IsUpdated)
                {
                    BL.FactoryBL.getBL().AddClient(client);
                    user.Name = client.Name;
                    user.Password = passwordBox1.GetPassword();
                    user.Type = BE.UserType.Client;
                    user.ItemID = client.ID;
                    BL.FactoryBL.getBL().AddUser(user);
                    MessageBox.Show("The account " + user.UserName + " was created!", "Account created", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    user.Name = client.Name;
                    user.Password = passwordBox1.GetPassword();
                    BL.FactoryBL.getBL().UpdateUser(user);
                    BL.FactoryBL.getBL().UpdateClient(client);
                    MessageBox.Show("The account " + user.UserName + " was Updated!", "Account Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                this.Close();

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Problem with account", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// checks if the age was changed by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlusMinusTextBox_Changed(object sender, BE.EventValue e)
        {
            if (client != null)
                client.Age = Convert.ToInt32(e.Value);
        }
        //bool GotPropty(object obj,string str)
        //{
        //    if(obj.GetType().GetProperty(str)!=null)
        //        return true;
        //    return false;

        //}
    }
}
