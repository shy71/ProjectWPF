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
    /// Interaction logic for UserEditor.xaml
    /// </summary>
    public partial class UserEditor : Window
    {
        BE.User user;
        bool IsUpdated;
        public UserEditor(BE.UserType type)
        {
            InitializeComponent();
            user = new BE.User();
            user.Type = type;
        }
        public UserEditor(BE.User user):this(user.Type)
        {
            IsUpdated = true;
            this.user = user;
            if (user.ItemID == 0)
                idBox.Str = "Doesnt have a branch";
            if (user.ItemID != 0)
                idBox.Visibility = Visibility.Visible;
            

        }
        private void usernameBox_Changed(object sender, BE.EventValue e)
        {
            user.UserName = e.Value.ToString();
        }

        private void nameBox_Changed(object sender, BE.EventValue e)
        {
            user.Name = e.Value.ToString();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (passwordBox1.GetPassword() != passwordBox2.GetPassword())
                    throw new Exception("The passwords does not match!");
                if (passwordBox1.GetPassword() == "")
                    throw new Exception("The password cant be empty!");
                if (!IsUpdated)
                {
                    user.Password = passwordBox1.GetPassword();
                    BL.FactoryBL.getBL().AddUser(user);
                    MessageBox.Show("The user " + user.UserName + " was created!", "User created", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    user.Password = passwordBox2.GetPassword();
                    BL.FactoryBL.getBL().UpdateUser(user);
                    MessageBox.Show("The user " + user.UserName + " was Updated!", "User Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                this.Close();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
