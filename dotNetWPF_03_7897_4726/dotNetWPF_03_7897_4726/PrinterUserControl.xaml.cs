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
        int MAX_INK = 100, MAX_PAGES = 400;
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
                    inkCount = System.Math.Round(value, 1);
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