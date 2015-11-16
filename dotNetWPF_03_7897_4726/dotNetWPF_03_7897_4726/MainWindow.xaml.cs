﻿using System;
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
                    printers.Enqueue(printer);

                }
            }
            CourentPrinter = printers.Dequeue();

        }

        public void OutOfPaper(object sender,EventArgs args)
        {

        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {

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