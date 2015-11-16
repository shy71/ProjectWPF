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

namespace dotNetWPF_03_7897_4726
{
    /// <summary>
    /// Interaction logic for PrinterUserControl.xaml
    /// </summary>
    public partial class PrinterUserControl : UserControl
    {
        static int amountOfPrinters=0;
        const int MAX_PAGES = 400, MIN_ADD_PAGES = 10, MAX_PRINT_PAGES = 60;
        const double MAX_INK = 100, MIN_ADD_INK = 10.0, MAX_PRINT_INK = 10;
        string printerName;
        public string PrinterName
        {
            get { return printerName; }
            set
            {
                printerName = value;
                printerNameLabel.Content = printerName;
            }
        }
        public static readonly double MaxPages = MAX_PAGES;
        double inkCount;
        public double InkCount
        {
            get { return inkCount; }
            set
            {
                inkLabel.Foreground = Brushes.Black;
                if (value > MAX_INK)
                    inkCount = MAX_INK;
                else
                {
                    inkCount = Math.Round(value, 1);
                    if (value >= 1 && value <= 15)
                        inkLabel.Foreground = (InkCount > 10) ? Brushes.Yellow : Brushes.Orange;
                    else if (value < 1)
                        inkLabel.Foreground = Brushes.Red;
                }
                inkCountProgressBar.Value = inkCount;
            }
        }
        int pageCount;
        public int PageCount
        {
            get { return pageCount; }
            set
            {
                pageLabel.Foreground = Brushes.Black;
                if (value > MAX_PAGES)
                    pageCount = MAX_PAGES;
                else
                {
                    if (value == 0)
                        pageLabel.Foreground = Brushes.Red;
                    pageCount = value;
                }
                pageCountSlider.Value = pageCount;
            }
        }
        public bool ChangePages(int num)///מוסיף/מדפיס דפים ודואג לכל העניינים הקשורים(עדכון שדות,אירועים וכו')
        {
            /*
             * צריך לבדוק מה קורה אם הוא מנסה להדפיס יותר מידי דפים
             * האם זה "ממשיך" אחרי השגיאה להדפיס
             * לבדוק עם עובד!
             */
            int temp;
            if (num > MIN_ADD_PAGES)
            {
                if (PageCount + num < MAX_PAGES)
                {
                    temp = PageCount + num;
                    PageCount = temp;
                    return false;
                }
                else
                {
                    PageCount = MAX_PAGES;
                    return true;
                }
            }
            else if (num < 0)
            {
                if (num < -MAX_PRINT_PAGES)
                {
                    return false;
                }
                else if (this.PageCount + num > 0)
                {
                    temp = this.PageCount + num;
                    this.PageCount = temp;
                    return true;
                }
                else
                {
                    temp = PageCount;
                    PageCount = 0;
                    pageLabel.Foreground = Brushes.Red;
                    if (PageMissing != null)
                        PageMissing(this, new PrinterEventArgs(true, "Out Of Paper(" + System.Math.Abs(temp + num) + ")", this.PrinterName));
                    //מה אמור לעשות פה?
                    return false;
                }
            }
            else
                return false;
        }
        public bool ChangeInk(double num)///מוסיף/מדפיס דיו ודואג לכל העניינים הקשורים(עדכון שדות,אירועים וכו')
        {
            /*
             * לבדוק מה אמורים לעשות עם נגמר הדיו וגם האם אפשר להמשיך להדפיס למרות שנשאר רק אחד אחוז
             * לבדוק עם עובד!
             */
            double temp;
            if (num > MIN_ADD_INK)
            {
                if (this.InkCount + num < MAX_INK)
                {
                    temp= InkCount +num;
                    InkCount = temp;
                    return true;
                }
                else
                {
                    InkCount = MAX_INK;
                    return false;
                }
            }
            else if (num < 0)
            {
                if (num < -MAX_PRINT_INK)
                    return false;
                else if (InkCount + num > 0)
                {
                    temp = InkCount + num;
                    InkCount = temp;
                    if(InkCount>=1&&InkCount<=15)
                    {
                        if (InkEmpty != null)
                            InkEmpty(this, new PrinterEventArgs(false, "Ink is Low! only " + InkCount + "% is left", this.PrinterName));
                    }
                    else if(InkCount<1)
                    {
                        if (InkEmpty != null)
                            InkEmpty(this, new PrinterEventArgs(true, "Ink is Low! only " + InkCount + "% is left", this.PrinterName));
                    }
                    return true;
                }
                else
                {
                    temp = InkCount;
                    InkCount = 0;
                    if (InkEmpty != null)
                        InkEmpty(this, new PrinterEventArgs(true, "Out Of Ink", this.PrinterName));
                    return false;
                }
            }
            else
                return false;
        }
       
        public void AddInk()
        {
            Random rand = new Random();
            ChangeInk(rand.Next((int)MIN_ADD_INK));
        }
        public void AddPages()
        {
            Random rand = new Random();
            ChangePages(rand.Next((int)MIN_ADD_PAGES));
        }
        public void Print()
        {
            Random randPage = new Random(), randInk = new Random();
            ChangePages(-randPage.Next((int)MAX_PRINT_PAGES));
            ChangeInk(-randInk.Next((int)MAX_PRINT_INK));
        }
        public event EventHandler PageMissing, InkEmpty;
        
        public PrinterUserControl()
        {
            InitializeComponent();
            this.PrinterName = ("Printer " + (++amountOfPrinters));
            InkCount = 50;
            PageCount = 250;
        }

        private void printerNameLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            printerNameLabel.FontSize = 40;
        }

        private void printerNameLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            printerNameLabel.FontSize = 16;
        }

    }
    public class PrinterEventArgs : EventArgs
    {
        public readonly bool Critical;
        public readonly DateTime Time;
        public readonly string ErrorMessage, PrinterName;
        public PrinterEventArgs(bool critical, string errorMessage, string printerName)
        {
            Critical = critical;
            ErrorMessage = errorMessage;
            PrinterName = printerName;
            Time = DateTime.Now;
        }
    }
}