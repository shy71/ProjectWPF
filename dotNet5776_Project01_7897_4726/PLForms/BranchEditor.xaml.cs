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
    /// Incharge of editing or creating a branch
    /// </summary>
    public partial class BranchEditor : Window
    {
        bool IsUpadte;
        BE.Branch branch;
        /// <summary>
        /// constructor
        /// </summary>
        public BranchEditor()
        {
            InitializeComponent();
            branch = new BE.Branch();
        }
        /// <summary>
        /// constructor which starts everything
        /// </summary>
        /// <param name="bra"></param>
        /// <param name="IsNetworkManager"></param>
        public BranchEditor(BE.Branch bra, bool IsNetworkManager = true)
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
            if (!IsNetworkManager)
            {
                nameBox.IsEnabled = false;
                addressBox.IsEnabled = false;
                ManagerCombo.Visibility = Visibility.Hidden;
                KashrutCombo.Visibility = Visibility.Hidden;
                branchCombo.Visibility = Visibility.Hidden;
                branchComboText.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// in case the user decided to hire a new manager for the branch, this deals with it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateBranchManagerButton_Click(object sender, RoutedEventArgs e)
        {
            new UserEditor(BE.UserType.BranchManger).ShowDialog();
            ManagerCombo.Items.Clear();
            ManagerComboBox_Loaded(this, null);
        }
        /// <summary>
        /// checks if the number of employees was changed by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberOfEmplyes_Changed(object sender, BE.EventValue e)
        {
            if (branch != null)
                branch.EmployeeCount = Convert.ToInt32(e.Value);
        }
        /// <summary>
        /// checks if the user changed the number of available messanagers the branch starts with
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AvailableMessangers_Changed(object sender, BE.EventValue e)
        {
            if (branch != null)
                branch.AvailableMessangers = Convert.ToInt32(e.Value);
        }
        /// <summary>
        /// checks if a specific text box was changed by the user and changes the property in the branch that that text box represents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextControl_Changed(object sender, BE.EventValue e)
        {
            if (branch != null)
                branch.GetType().GetProperty(e.pName).SetValue(branch, e.Value);
        }
        /// <summary>
        /// in case the finalizing button was clicked it deals with it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsUpadte)
                    BL.FactoryBL.getBL().UpdateBranch(branch);
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
        /// <summary>
        /// loading the items into the comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManagerComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = new ComboBoxItem();
            temp.Content = "Create a New manager!";
            temp.ToolTip = "Will give you the option to\ncreate a new manager that\nwill run the branch";
            ManagerCombo.Items.Add(temp);
            if (IsUpadte)
            {
                temp = new ComboBoxItem();
                temp.Content = "Stay with the current manager!";
                temp.ToolTip = branch.Boss;
                ManagerCombo.Items.Add(temp);
                ManagerCombo.SelectedItem = temp;
            }
            foreach (BE.User item in BL.FactoryBL.getBL().GetAllUsers(item2 => item2.Type == BE.UserType.BranchManger && item2.ItemID == 0))
            {
                temp = new ComboBoxItem();

                temp.Content = item.Name + " @" + item.UserName;
                temp.ToolTip = item.ToString();
                ManagerCombo.Items.Add(temp);
            }
        }
        /// <summary>
        /// checks if a manager was chosen from the managers in the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManagerCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ManagerCombo.SelectedIndex == 0)
                CreateBranchManagerButton.IsEnabled = true;
            else if (ManagerCombo.SelectedIndex != 0)
                CreateBranchManagerButton.IsEnabled = false;
            if (ManagerCombo.SelectedIndex > 0 && ((!IsUpadte) || ManagerCombo.SelectedIndex > 1))
                branch.Boss = (ManagerCombo.Items.GetItemAt(ManagerCombo.SelectedIndex) as ComboBoxItem).Content.ToString();
        }
        /// <summary>
        /// loading the items into the comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BranchComboBox_Loaded(object sender, RoutedEventArgs e)
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
                branchCombo.ToolTip = "There isn't any branchs to pick from!";
            }
        }
        /// <summary>
        /// loading the items into the comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KashrutCombo_Loaded(object sender, RoutedEventArgs e)
        {
            KashrutCombo.ItemsSource = typeof(BE.Kashrut).GetEnumNames();
        }
        /// <summary>
        /// copies a branch to use as a sample for the new one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// checks if the kashrut level was changed and updated the Kosher level of the branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KashrutCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KashrutCombo.SelectedIndex >= 0)
                branch.Kosher = BE.Extensions.ToKashrut(KashrutCombo.SelectedItem.ToString());
        }
    }
}
