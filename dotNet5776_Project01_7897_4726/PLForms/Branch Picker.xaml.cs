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
    /// Interaction logic for Delete_Branch.xaml
    /// Used for chooseing a branch 
    /// </summary>
    public partial class BranchPicker : Window
    {
        List<int> theBranchesIDs;
        bool IsCtrlDown = false;
        Action<int> func;
        /// <summary>
        /// Checks if ctrl is being held down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDownCheck(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                IsCtrlDown = true;
        }
        /// <summary>
        /// checks if ctrl was released
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyUpCheck(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                IsCtrlDown = false;
        }
        /// <summary>
        /// constructor which starts and prints everything
        /// </summary>
        /// <param name="func"></param>
        /// <param name="str"></param>
        public BranchPicker(Action<int> func, string str)
        {
            InitializeComponent();
            choiceBtn.IsEnabled = false;
            // choiceBtn.DataContext = theBranchesIDs;
            theBranchesIDs = new List<int>();
            TextBox text;
            int NumOfBranches = 0;
            foreach (StackPanel item in MainGrid.Children)
            {
                item.Children.RemoveRange(0, item.Children.Count);
            }
            var list = BL.FactoryBL.getBL().GetAllBranchs();
            foreach (BE.Branch item in BL.FactoryBL.getBL().GetAllBranchs())
            {
                text = new TextBox();
                text.Text = item.ToString().Replace("\t", "");
                text.FontFamily = new FontFamily("Comic Sans MS");
                text.Opacity = 0.5;
                text.Width = 180;
                text.PreviewMouseUp += MouseClick;
                text.IsReadOnly = true;
                if (NumOfBranches % 3 == 0)
                    Stack1.Children.Add(text);
                else if (NumOfBranches % 3 == 1)
                    Stack2.Children.Add(text);
                else
                    Stack3.Children.Add(text);
                NumOfBranches++;
            }
            this.func = func;
            this.DataContext = new { Name = str, Header = "Choose the branch you want to " + str.ToLower() + ":" };

        }
        /// <summary>
        /// refreshes the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh(object sender, RoutedEventArgs e)
        {
            TextBox text;
            int NumOfBranches = 0;
            foreach (StackPanel item in MainGrid.Children)
            {
                item.Children.RemoveRange(0, item.Children.Count);
            }
            var list = BL.FactoryBL.getBL().GetAllBranchs();
            foreach (BE.Branch item in BL.FactoryBL.getBL().GetAllBranchs())
            {
                text = new TextBox();
                text.Text = item.ToString().Replace("\t", "");
                text.FontFamily = new FontFamily("Comic Sans MS");
                text.Opacity = 0.5;
                text.Width = 180;
                text.PreviewMouseUp += MouseClick;
                text.IsReadOnly = true;
                if (NumOfBranches % 3 == 0)
                    Stack1.Children.Add(text);
                else if (NumOfBranches % 3 == 1)
                    Stack2.Children.Add(text);
                else
                    Stack3.Children.Add(text);
                NumOfBranches++;
            }
        }
        /// <summary>
        /// checks when mouse clicks something relevent like a branch being chosen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MouseClick(object sender, MouseButtonEventArgs e)
        {
            bool WasSelected = (sender as TextBox).Opacity == 1;
            if (!IsCtrlDown)
            {
                foreach (StackPanel stack in MainGrid.Children)
                {
                    foreach (TextBox item in stack.Children)
                    {
                        item.Opacity = 0.5;
                        item.BorderThickness = new Thickness(1);
                        item.BorderBrush = Brushes.Black;
                        choiceBtn.IsEnabled = false;
                    }
                }
            }
            if (!WasSelected)
            {
                (sender as TextBox).Opacity = 1;
                (sender as TextBox).BorderThickness = new Thickness(2);
                (sender as TextBox).BorderBrush = Brushes.LightBlue;
                choiceBtn.IsEnabled = true;
            }
            else if (IsCtrlDown)
            {
                (sender as TextBox).Opacity = 0.5;
                (sender as TextBox).BorderThickness = new Thickness(1);
                (sender as TextBox).BorderBrush = Brushes.Black;
                choiceBtn.IsEnabled = false;
                foreach (StackPanel stack in MainGrid.Children)
                {
                    foreach (TextBox item in stack.Children)
                    {
                        if (item.Opacity == 1)
                            choiceBtn.IsEnabled = true;
                    }
                }
            }
        }
        /// <summary>
        /// checks when the button to finalize the choices is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void choiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (StackPanel stack in MainGrid.Children)
                {
                    foreach (TextBox item in stack.Children)
                    {
                        if (item.Opacity == 1)
                            theBranchesIDs.Add(Convert.ToInt32(item.Text.Substring(item.Text.IndexOf("ID: ") + 4, item.Text.IndexOf("\nName:") - item.Text.IndexOf("ID: ") - 4)));
                    }
                }
                if (theBranchesIDs.Count == 0)
                    MessageBox.Show("No branch has been chosen.", "Information", MessageBoxButton.OK);
                else
                {
                    foreach (int item in theBranchesIDs)
                    {
                        func(item);
                        //BL.FactoryBL.getBL().GetAllUsers(var => var.ItemID == item).FirstOrDefault().ItemID = 0;
                        //BL.FactoryBL.getBL().DeleteBranch(item);
                    }
                }
                this.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                theBranchesIDs.Clear();
                choiceBtn.IsEnabled = false;
            }
        }
        /// <summary>
        /// checks when the window was loaded adn messages the user if there is nothing to choose from
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Stack1.Children.Count == 0)
            {
                MessageBox.Show("You don't have any branch to pick from!");
                this.Close();
            }
        }
    }
}
