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
        int MAX_PAGES = 400, MIN_ADD_PAGES = 10, MAX_PRINT_PAGES = 200;
        double MAX_INK = 100, MIN_ADD_INK = 10.0, MAX_PRINT_INK = 90.0;
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
        double inkCount;
        public double InkCount
        {
            get { return inkCount; }
            set
            {
                if (value > MAX_INK)
                    inkCount = MAX_INK;
                else
                    inkCount = Math.Round(value, 1);
                inkCountProgressBar.Value = inkCount;
            }
        }
        int pageCount;
        public int PageCount
        {
            get { return pageCount; }
            set
            {
                if (value > MAX_PAGES)
                    pageCount = MAX_PAGES;
                else
                    pageCount = value;
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
                else if (this.PageCount - num > 0)
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
             * לבדוק עם עובד!!
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
                else if (InkCount - num > 0)
                {
                    temp = InkCount + num;
                    InkCount = temp;
                    if(InkCount>=1&&InkCount<=15)
                    {
                        inkCountProgressBar.Foreground =(InkCount>10)?Brushes.Yellow:Brushes.Orange;
                        InkEmpty(this, new PrinterEventArgs(false, "Ink is Low! only " + InkCount + "% is left", this.PrinterName));
                    }
                    else if(InkCount<1)
                    {
                        inkCountProgressBar.Foreground = Brushes.Red;
                        InkEmpty(this, new PrinterEventArgs(true, "Ink is Low! only " + InkCount + "% is left", this.PrinterName));
                    }
                    return true;
                }
                else
                {
                    temp = InkCount;
                    InkCount = 0;
                    inkCountProgressBar.Foreground = Brushes.Red;
                    InkEmpty(this, new PrinterEventArgs(true, "Out Of Ink (" + System.Math.Abs(temp - num) + ")", this.PrinterName));
                    return false;
                }
            }
            else
                return false;
        }

        EventHandler<PrinterEventArgs> PageMissing, InkEmpty;
        public PrinterUserControl()
        {
            InitializeComponent();
        }
    }
    public class PrinterEventArgs
    {
        bool critical;
        public bool Critical { get { return critical; } }
        DateTime time;
        public DateTime Time { get { return time; } }
        string errorMessage, printerName;
        public string ErrorMessage { get { return errorMessage; } }
        public string PrinterName { get { return printerName; } }
        public PrinterEventArgs(bool crit, string errorMessage, string name)
        {
            critical = crit;
            this.errorMessage = errorMessage;
            printerName = name;
            time = DateTime.Now;
        }
    }
}