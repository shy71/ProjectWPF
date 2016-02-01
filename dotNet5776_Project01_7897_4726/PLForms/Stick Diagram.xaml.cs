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
            return Convert.ToInt32(0.9*this.ActualHeight * (double)((double)height / (from item in heights
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
            for (int i = 0; i < Heights.Count(); i++)
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            int counter = 0;
            foreach (ColumnDefinition column in MainGrid.ColumnDefinitions)
            {
                stack = new StackPanel();
                stack.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                column.Width = new System.Windows.GridLength(1, GridUnitType.Star);
                text = new TextBlock();
                text.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                text.Height = getHeight(Heights[counter].Sum, Heights);
                numberText = new TextBlock();
                numberText.TextAlignment = TextAlignment.Center;
                numberText.Text = Heights[counter].Sum.ToString();
                //numberText.Margin.Bottom = text.Height;
                if (text.Height / (0.9 * MainGrid.ActualHeight) < 0.05)
                    text.Background = Brushes.Red;
                else if (text.Height / (0.9*MainGrid.ActualHeight) < 0.2)
                    text.Background = Brushes.Orange;
                else if (text.Height / (0.9 * MainGrid.ActualHeight) < 0.6)
                    text.Background = Brushes.Yellow;
                else
                    text.Background = Brushes.Green;
                stack.Children.Add(numberText);
                stack.Children.Add(text);
                TextBlock name = new TextBlock();
                name.TextAlignment = TextAlignment.Center;
                text.Width = MainGrid.ActualWidth / (2 * Heights.Count());
                switch(Heights[0].Type)
                {
                    case BE.GroupingByType.BranchKashrut:
                        name.Text = Heights[counter].Kashrut.ToString();
                        break;
                    case BE.GroupingByType.DishKashrut:
                        name.Text = Heights[counter].Kashrut.ToString();
                        break;
                    case BE.GroupingByType.Branch:
                        name.Text = BL.FactoryBL.getBL().GetAllBranchs(item => item.ID == Heights[counter].NumKey).FirstOrDefault().Name;
                        break;
                    case BE.GroupingByType.DishesAmount:
                        name.Text = BL.FactoryBL.getBL().GetAllDishs(item => item.ID == Heights[counter].NumKey).FirstOrDefault().Name;
                        break;
                    case BE.GroupingByType.Address:
                        name.Text = Heights[counter].StrKey;
                        break;
                    case BE.GroupingByType.Date:
                        name.Text = Heights[counter].StrKey;
                        break;
                    case BE.GroupingByType.Dish:
                        name.Text = BL.FactoryBL.getBL().GetAllDishs(item => item.ID == Heights[counter].NumKey).FirstOrDefault().Name;
                        break;
                }
                stack.Children.Add(name);
                MainGrid.Children.Add(stack);
                Grid.SetColumn(stack, counter);
                counter++;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(Heights!=null)
                Refresh();
        }
    }
}
