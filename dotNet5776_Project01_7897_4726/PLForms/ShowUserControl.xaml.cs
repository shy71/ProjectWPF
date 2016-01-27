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
    /// Interaction logic for ShowUserControl.xaml
    /// </summary>
    public partial class ShowUserControl : Window
    {
        public ShowUserControl()
        {
            InitializeComponent();
        }
        public ShowUserControl(UserControl us)
        {
            InitializeComponent();
            if (us.Parent != null)
                throw new Exception("cant cretae a window for a Usercontrol that is already assingend");
            MainGrid.Children.Add(us);
        }
    }
}
