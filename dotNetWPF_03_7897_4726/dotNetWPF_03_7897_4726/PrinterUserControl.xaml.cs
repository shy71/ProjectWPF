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
        int MAX_PAGES = 400, MIN_ADD_PAGES = 0, MAX_ADD_PAGES = 400, MAX_PRINT_PAGES = 200;
        double MAX_INK = 10.0, MIN_ADD_INK = 0.0, MAX_ADD_INK = 100.0, MAX_PRINT_INK = 90.0;
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
                inkLabel.Content = inkCount;
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
                    PageCount = value;
                pageLabel.Content = pageCount;
            }
        }
        public bool ChangePages(int num)///מוסיף/מדפיס דפים ודואג לכל העניינים הקשורים(עדכון שדות,אירועים וכו')
        {
            int temp;
            if (num > MIN_ADD_PAGES)
            {
                bool toManyPages = false;
                if (num > MAX_ADD_PAGES)
                {
                    num = MAX_ADD_PAGES;
                    toManyPages = true;
                }
                if (this.PageCount + num > MAX_PAGES)
                {
                    temp = this.PageCount + num;
                    this.PageCount = temp;
                    return !toManyPages;
                }
                else
                {
                    this.PageCount = MAX_PAGES;
                    return false;
                }
            }
            else if (num < 0)
            {
                if (num < -MAX_PRINT_PAGES)
                {
                    return false;
                }
                if (this.PageCount - num > 0)
                {
                    temp = this.PageCount + num;
                    this.PageCount = temp;
                    return true;
                }
                else
                {
                    PageMissing(this, new PrinterEventArgs(true, "Out Of Paper(" + System.Math.Abs(pageCount - num) + ")", this.PrinterName));
                    //מה אמור לעשות פה?
                    return false;
                }
            }
            else
                return false;
        }
        public bool ChangeInk(double num)///מוסיף/מדפיס דיו ודואג לכל העניינים הקשורים(עדכון שדות,אירועים וכו')
        {
            double temp;
            if (num > MIN_ADD_INK)
            {
                bool toMuchInk = false;
                if (num > MAX_ADD_PAGES)
                {
                    num = MAX_ADD_INK;
                    toMuchInk = true;
                }
                if (this.inkCount + num > MAX_INK)
                {
                    temp = this.inkCount + num;
                    inkCountProgressBar.Value = this.inkCount = Math.Round(temp, 1);
                    return !toMuchInk;
                }
                else
                {
                    inkCountProgressBar.Value = this.inkCount = MAX_INK;
                    return false;
                }
            }
            else if (num < 0)
            {
                if (num < -MAX_PRINT_INK)
                {
                    return false;
                }
                if (this.inkCount - num > 0)
                {
                    temp = this.inkCount + num;
                    inkCountProgressBar.Value = this.inkCount = Math.Round(temp, 1);
                    return true;
                }
                else
                {
                    InkEmpty(this, new PrinterEventArgs(true, "Out Of Ink (" + System.Math.Abs(inkCount - num) + ")", this.PrinterName));
                    inkCountProgressBar.Value = 0;
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