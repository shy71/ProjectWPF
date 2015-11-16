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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PrinterUserControl CourentPrinter;
        Queue<PrinterUserControl> printers;
        public MainWindow()
        {

            InitializeComponent();
            printers = new Queue<PrinterUserControl>();
            foreach (Control item in printersGrid.Children)
            {
                if (item is PrinterUserControl)
                {
                    PrinterUserControl printer = item as PrinterUserControl;
                    // …
                    printer.PageMissing += OutOfPaper;
                    printer.InkEmpty += LowOnInk;
                    printers.Enqueue(printer);

                }
            }
            CourentPrinter = printers.Dequeue();

        }

        PrinterUserControl BestPrinter()
        {
            PrinterUserControl printer;
            for (int i = 0; i < printers.Count; i++)
            {
                printer = printers.Dequeue();
                if (printer.InkCount > 15 && printer.PageCount > 0)
                    return printer;
                printers.Enqueue(printer);
            }
            for (int i = 0; i < printers.Count; i++)
            {
                printer = printers.Dequeue();
                if (printer.InkCount > 10 && printer.PageCount > 0)
                    return printer;
                printers.Enqueue(printer);
            }
            for (int i = 0; i < printers.Count; i++)
            {
                printer = printers.Dequeue();
                if (printer.InkCount > 1 && printer.PageCount > 0)
                    return printer;
                printers.Enqueue(printer);
            }
            return printers.Dequeue();
        }
        public void OutOfPaper(object sender, EventArgs args)
        {
            PrinterEventArgs arg = args as PrinterEventArgs;
            MessageBox.Show("Message from " + arg.PrinterName + ": " + arg.ErrorMessage, arg.PrinterName + " is out of paper!!");
            if (arg.Critical)
            {
                printers.Enqueue(CourentPrinter);
                CourentPrinter = BestPrinter();
            }
            (sender as PrinterUserControl).ChangePages(100);
        }
        public void LowOnInk(object sender, EventArgs args)
        {
            PrinterEventArgs arg = args as PrinterEventArgs;
            MessageBox.Show("Message from " + arg.PrinterName + ": " + arg.ErrorMessage, arg.PrinterName + " is " + ((arg.Critical) ? "out" : "low") + " of Ink!!");
            if (arg.Critical)
            {
                printers.Enqueue(CourentPrinter);
                CourentPrinter = BestPrinter();
                (sender as PrinterUserControl).ChangeInk(50);
            }


        }
        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            CourentPrinter.Print();
        }

    }
}



/*
        * שימו לב שבהוספת חומרים אסור לעבור את הכמות המרבית של אותו חומר )דיו או דפים(, והפיזור הרנדומלי של כמות שנוספה
צריך להיות אחיד, זאת אומרת לא מתאים "לחתוך" את המספר עם הכמות גדולה מידי אלא צריך לדאוג מראש שהכמות שמוסיפים
יחד עם הכמות שהייתה לא תעבור את המקסימום.
        * לבדוק האם זה בסדר
        * */

// לגבי משתנים לומר לעזרא