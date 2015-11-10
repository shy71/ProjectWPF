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
        public PrinterUserControl()
        {
            InitializeComponent();
        }
    }
    public class PrinterEventArgs : EventArgs
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
