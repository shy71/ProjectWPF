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
    /// Interaction logic for NewBranch.xaml
    /// </summary>
    public partial class NewBranch : Window
    {
        BE.Branch branch;
        public NewBranch()
        {
            InitializeComponent();
            branch = new BE.Branch();

        }

        private void CreateBranchManagerButton_Click(object sender, RoutedEventArgs e)
        {
            //add new branch manager window
        }

        private void NumberOfEmplyes_Changed(object sender, EventValue e)
        {
            if (branch != null)
                branch.EmployeeCount = Convert.ToInt32(e.Value);
        }
        private void AvilbleMesnngers_Changed(object sender, EventValue e)
        {
            if (branch != null)
                branch.AvailableMessangers = Convert.ToInt32(e.Value);
        }

        private void TextControl_Changed(object sender, EventValue e)
        {
            if (branch != null)
                branch.GetType().GetProperty(e.pName).SetValue(branch, e.Value);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.FactoryBL.getBL().AddBranch(branch);
                MessageBox.Show("The branch " + branch.Name + " was created!", "Branch created", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Problem with branch", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void MangerComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = new ComboBoxItem();
            temp.Content = "Create a New manger!";
            temp.ToolTip = "Will give you the option to\ncreate a new manger that\nwill run the branch";
            MangerCombo.Items.Add(temp);
            foreach (BE.User item in BL.FactoryBL.getBL().GetAllUsers(item2=>item2.Type==BE.UserType.BranchManger&&item2.ItemID==0))
            {
                temp = new ComboBoxItem();
                
                temp.Content = item.Name + " @" + item.UserName;
                temp.ToolTip = item.ToString();
                MangerCombo.Items.Add(temp);
            }
        }

        private void MangerCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MangerCombo.SelectedIndex == 0)
                CreateBranchManagerButton.IsEnabled = true;
            else if(MangerCombo.SelectedIndex!=0)
                CreateBranchManagerButton.IsEnabled = false;
            if (MangerCombo.SelectedIndex > 0)
                branch.Boss =( MangerCombo.Items.GetItemAt(MangerCombo.SelectedIndex) as ComboBoxItem).Content.ToString();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = new ComboBoxItem();//בזבוז
            foreach (BE.Branch item in BL.FactoryBL.getBL().GetAllBranchs())
            {
                temp = new ComboBoxItem();
                temp.Content = item.Name + " - " + item.Address;
                temp.ToolTip = item.ToString();
                branchCombo.Items.Add(temp); 
            }
            if (branchCombo.Items.Count == 0)
            {
                branchCombo.IsEnabled = false;
                branchCombo.ToolTip = "There isnt any branchs to pick from!";
            }
        }

        private void KashrutCombo_Loaded(object sender, RoutedEventArgs e)
        {
            KashrutCombo.ItemsSource = typeof(BE.Kashrut).GetEnumNames();
        }

        private void CopyBranch(object sender, RoutedEventArgs e)
       {
           try
           {
               BE.Branch temp;
               if (branchCombo.SelectedIndex >= 0)
               {
                   temp = BL.FactoryBL.getBL().GetAllBranchs(item2 => item2.ToString() == (branchCombo.Items.GetItemAt(branchCombo.SelectedIndex) as ComboBoxItem).ToolTip.ToString()).FirstOrDefault();
                   if (temp == null)
                       throw new Exception("ERROR");
                   nameBox.SetText(temp.Name);
                   addressBox.SetText(temp.Address);
                   phoneBox.SetText(temp.PhoneNumber);
                   empoyeBox.SetNum(temp.EmployeeCount);
                   messengersBox.SetNum(temp.AvailableMessangers);
                   KashrutCombo.SelectedIndex = (temp.Kosher == BE.Kashrut.HIGH) ? 2 : (temp.Kosher == BE.Kashrut.MEDIUM) ? 1 : 0;
                   branchCombo.SelectedIndex = -1;
               }
               else
                   throw new Exception("ERROR");
           }
            catch(Exception Exp)
           {
               MessageBox.Show(Exp.ToString(), "Error");
           }

        }

        private void KashrutCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KashrutCombo.SelectedIndex >= 0)
                branch.Kosher = BE.Extensions.ToKashrut(KashrutCombo.SelectedItem.ToString());
        }
    }
}
