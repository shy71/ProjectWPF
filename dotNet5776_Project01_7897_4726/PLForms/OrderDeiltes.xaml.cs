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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLForms
{
    /// <summary>
    /// Interaction logic for OrderDeiltes.xaml
    /// </summary>
    public partial class OrderDeiltes : UserControl
    {
        public OrderDeiltes()
        {
            InitializeComponent();
        }
        public OrderDeiltes(BE.Order order)
        {
            InitializeComponent();//error if branch does not exsist
            BE.Branch temp=BL.FactoryBL.getBL().GetAllBranchs(item=> item.ID==order.BranchID).FirstOrDefault();
            if(temp==null)
                throw new Exception("There isnt a branch that matches this Order!");
            ShortAddress.Text =temp.Name;
            ShortAddress.ToolTip = temp.Address;
            FullAddress.Text = order.Address;
            Date.Content = order.Date.ToShortDateString();
            Date.ToolTip = order.Date.ToShortTimeString();
        }
    }
}
