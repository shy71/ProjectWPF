﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        /// <summary>
        /// קונסטרקטור שבונה את החלון ומאתחל את משתני המחלקה
        /// </summary>
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
        /// <summary>
        /// הפונקציה מחפשת ובוחרת את המדפסת הבאה שנמצאת ב"רמה" הכי גבוהה מבחינת מצב דיו/נייר לפי מה שהוגדר בתרגיל כרמות שונות
        /// </summary>
        /// <returns>מחזירה את המדפסת שצריכה להיות הבאה בתור</returns>
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
        /// <summary>
        /// פונקציה המטפלת בשגיאת חוסר דפים במדפסת
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg">נתנוי השגיאה</param>
        public void OutOfPaper(object sender, PrinterEventArgs arg)
        {
            new Thread(() => MessageBox.Show("At: " + arg.Time + "\nMessage from " + arg.PrinterName + ": " + arg.ErrorMessage, arg.PrinterName + " is out of paper!!", MessageBoxButton.OK, MessageBoxImage.Stop)).Start();
            printers.Enqueue(CourentPrinter);
            CourentPrinter = BestPrinter();
            (sender as PrinterUserControl).AddPages();
        }
        /// <summary>
        /// פונקציה המטפלת בשגיאת מעט דיו/חוסר דיו במדפסת
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg">נתנוי השגיאה</param>
        public void LowOnInk(object sender, PrinterEventArgs arg)
        {
            MessageBox.Show("At: " + arg.Time + "\nMessage from " + arg.PrinterName + ": " + arg.ErrorMessage, arg.PrinterName + " is " + ((arg.Critical) ? "out" : "low") + " of Ink!!", MessageBoxButton.OK, ((arg.Critical) ? MessageBoxImage.Stop : MessageBoxImage.Warning));
            if (arg.Critical)
            {
                printers.Enqueue(CourentPrinter);
                CourentPrinter = BestPrinter();
                (sender as PrinterUserControl).AddInk();
            }


        }
        /// <summary>
        /// פונקציה המדפיסה במדפסת הנוכחית בכל פעם שלוחצים על כפתור הההדפסה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            CourentPrinter.Print();
        }

    }
}

