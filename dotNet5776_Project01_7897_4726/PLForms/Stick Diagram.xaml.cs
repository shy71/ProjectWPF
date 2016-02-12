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
    /// Interaction logic for Stick_Diagram.xaml
    /// </summary>
    public partial class Stick_Diagram : UserControl
    {
        internal BE.GroupSum[] Heights;
        public Stick_Diagram()
        {
            InitializeComponent();
        }
        internal int getHeight(double height, params BE.GroupSum[] heights)
        {
            return Convert.ToInt32(0.9 * this.ActualHeight * (double)((double)height / (from item in heights
                                                                                        select item.Sum).Max()));
        }
        public Stick_Diagram(params BE.GroupSum[] heights)
        {
            InitializeComponent();
            Heights = heights;
        }
        public void Refresh()
        {
            TextBlock text;
            TextBlock numberText;
            StackPanel stack;
            //for (int i = 0; i < Heights.Count(); i++)
            //    MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            //int counter = 0;
            //foreach (ColumnDefinition column in MainList.ColumnDefinitions)
            MainStack.Children.Clear();
            for (int counter = 0; counter < Heights.Count(); counter++)
            {

                stack = new StackPanel();
                stack.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                //column.Width = new System.Windows.GridLength(30, GridUnitType.Pixel);
                text = new TextBlock();
                text.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                text.Height = getHeight(Heights[counter].Sum, Heights);
                text.ToolTip = Heights[counter].ToolTip;
                numberText = new TextBlock();
                numberText.TextAlignment = TextAlignment.Center;
                numberText.Text = Heights[counter].Sum.ToString().Substring(0, (Heights[counter].Sum.ToString().Length > 5) ? 5 : Heights[counter].Sum.ToString().Length);
                numberText.ToolTip = Heights[counter].ToolTip;
                //numberText.Margin.Bottom = text.Height;
                if (text.Height / (0.9 * MainStack.ActualHeight) < 0.05)
                    text.Background = Brushes.Red;
                else if (text.Height / (0.9 * MainStack.ActualHeight) < 0.2)
                    text.Background = Brushes.Orange;
                else if (text.Height / (0.9 * MainStack.ActualHeight) < 0.6)
                    text.Background = Brushes.Yellow;
                else
                    text.Background = Brushes.Green;
                stack.Children.Add(numberText);
                text.Width = 75;
                stack.Children.Add(text);
                TextBlock name = new TextBlock();
                name.TextAlignment = TextAlignment.Center;
                name.Text = Heights[counter].LowerHeadr;
                name.ToolTip = Heights[counter].ToolTip;
                stack.Children.Add(name);
                // MainList.Children.Add(stack);
                MainStack.Children.Add(stack);
                text = new TextBlock();
                text.Width = 25;
                MainStack.Children.Add(text);
                Grid.SetColumn(stack, counter);
            }
            if (MainStack.Children.Count == 0)
            {
                text = new TextBlock();
                text.Text = "No Diagram To Show for this Data";
                MainStack.Children.Add(text);
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Heights != null)
                Refresh();
        }
    }
}
