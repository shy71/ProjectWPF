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

using System.Runtime.InteropServices;
using System.Windows.Interop;
namespace PLForms
{
    /// <summary>
    /// Interaction logic for UserEditor.xaml
    /// </summary>
    public partial class UserEditor : Window
    {
        
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);



        BE.User user;
        bool IsUpdated;
        public UserEditor(BE.UserType type)
        {
            InitializeComponent();
            user = new BE.User();
            user.Type = type;
            usernameBox.SetBinding(user, "UserName", BindingMode.TwoWay);
            nameBox.SetBinding(user, "Name", BindingMode.TwoWay);
        }

        public UserEditor(BE.User user):this(user.Type)
        {
            IsUpdated = true;
            this.user = user;
            usernameBox.IsEnabled = false;
            usernameBox.ToolTip = "You cant change your username!";
            if (user.ItemID == 0)
                idBox.Str = "Doesnt have a branch";
            else
            {
                idBox.SetText(user.ItemID.ToString());
                idBox.Visibility = Visibility.Visible;
            }
            nameBox.SetBinding(user, "Name", BindingMode.TwoWay);
            usernameBox.SetText(user.UserName);
            nameBox.SetText(user.Name);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(user.Type == BE.UserType.NetworkManger && (!BL.FactoryBL.getBL().GetAllUsers(item=>item.Type==BE.UserType.NetworkManger).Any()))
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
            }
        }
    }
}
 