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
    /// Interaction logic for BranchEditor.xaml
    /// </summary>
    public partial class BranchEditor : Window
    {
        bool IsUpadte;
        BE.Branch branch;
        public BranchEditor()
        {
            InitializeComponent();
            branch = new BE.Branch();

        }
        public BranchEditor(BE.Branch bra, bool IsNetworkManger = true)
        {
            InitializeComponent();
            nameBox.SetText(bra.Name);
            addressBox.SetText(bra.Address);
            phoneBox.SetText(bra.PhoneNumber);
            empoyeBox.SetNum(bra.EmployeeCount);
            messengersBox.SetNum(bra.AvailableMessangers);
            KashrutCombo.SelectedItem = bra.Kosher.ToString();
            branch = bra;
            IsUpadte = true;
            DoButton.Content = "Update!";
            if (!IsNetworkManger)
            {
                nameBox.IsEnabled = false;
                addressBox.IsEnabled = false;
                MangerCombo.Visibility = Visibility.Hidden;
                KashrutCombo.Visibility = Visibility.Hidden;
                branchCombo.Visibility = Visibility.Hidden;
                branchComboText.Visibility = Visibility.Hidden;
            }
        }

        private void CreateBranchManagerButton_Click(object sender, RoutedEventArgs e)
        {
            new UserEditor(BE.UserType.BranchManger).ShowDialog();
            MangerCombo.Items.Clear();
            MangerComboBox_Loaded(this, null);
        }

        private void NumberOfEmplyes_Changed(object sender, BE.EventValue e)
        {
            if (branch != null)
                branch.EmployeeCount = Convert.ToInt32(e.Value);
        }
        private void AvilbleMesnngers_Changed(object sender, BE.EventValue e)
        {
            if (branch != null)
                branch.AvailableMessangers = Convert.ToInt32(e.Value);
        }

        private void TextControl_Changed(object sender, BE.EventValue e)
        {
            if (branch != null)
                branch.GetType().GetProperty(e.pName).SetValue(branch, e.Value);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsUpadte)
                {

                    BL.FactoryBL.getBL().UpdateBranch(branch);
                }
                else
                    BL.FactoryBL.getBL().AddBranch(branch);
                MessageBox.Show("The branch " + branch.Name + " was " + ((IsUpadte) ? "Updated!" : "created!"), "Branch created", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception exp)
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
            if (IsUpadte)
            {
                temp = new ComboBoxItem();
                temp.Content = "Stay with the current manger!";
                temp.ToolTip = branch.Boss;
                MangerCombo.Items.Add(temp);
                MangerCombo.SelectedItem = temp;
            }
            foreach (BE.User item in BL.FactoryBL.getBL().GetAllUsers(item2 => item2.Type == BE.UserType.BranchManger && item2.ItemID == 0))
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
            else if (MangerCombo.SelectedIndex != 0)
                CreateBranchManagerButton.IsEnabled = false;
            if (MangerCombo.SelectedIndex > 0 && ((!IsUpadte) || MangerCombo.SelectedIndex > 1))
                branch.Boss = (MangerCombo.Items.GetItemAt(MangerCombo.SelectedIndex) as ComboBoxItem).Content.ToString();
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
            catch (Exception Exp)
            {
                MessageBox.Show(Exp.ToString(), "Error");
            }

        }

        private void KashrutCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KashrutCombo.SelectedIndex >= 0)
                branch.Kosher = BE.Extensions.ToKashrut(KashrutCombo.SelectedItem.ToString());
        }

        private void DoButton_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).ToolTip = "Create the account";
        }
    }
}
