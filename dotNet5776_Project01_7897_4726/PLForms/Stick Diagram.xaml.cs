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
            for (int i = 0; i < Heights.Count(); i++)
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            int counter = 0;
            foreach (ColumnDefinition column in MainGrid.ColumnDefinitions)
            {
                column.Width = new System.Windows.GridLength(1, GridUnitType.Star);
                text = new TextBlock();
                text.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                text.Height = getHeight(Heights[counter].Sum, Heights);
                switch(Heights[0].Type)
                {
                    case BE.GroupingByType.Address:
                        text.ToolTip = "Amount: " + Heights[counter].Sum + "\nAddress: " + Heights[counter].StrKey;
                        break;
                    case BE.GroupingByType.Date:
                        text.ToolTip = "Amount: " + Heights[counter].Sum + "\nDate: " + Heights[counter].StrKey;
                        break;
                    case BE.GroupingByType.Dish:
                        text.ToolTip = "Amount: " + Heights[counter].Sum + "\nDish: " + BL.FactoryBL.getBL().GetAllDishs(item => item.ID == Heights[counter].NumKey).FirstOrDefault().Name;
                        break;
                }
                if (text.Height / (0.9 * MainGrid.ActualHeight) < 0.05)
                    text.Background = Brushes.Red;
                else if (text.Height / (0.9*MainGrid.ActualHeight) < 0.2)
                    text.Background = Brushes.Orange;
                else if (text.Height / (0.9 * MainGrid.ActualHeight) < 0.6)
                    text.Background = Brushes.Yellow;
                else
                    text.Background = Brushes.Green;
                MainGrid.Children.Add(text);
                Grid.SetColumn(text, counter);
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
