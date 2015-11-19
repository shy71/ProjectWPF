using System;
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
    /// Interaction logic for PrinterUserControl.xaml
    /// </summary>
    public partial class PrinterUserControl : UserControl
    {
        static Random rand = new Random();
        static int amountOfPrinters = 0;
        const int MAX_PAGES = 400;
        const int MIN_ADD_PAGES = MAX_PAGES / 10, MAX_PRINT_PAGES = MAX_PAGES / 12;
        const double MAX_INK = 100;
        const double MIN_ADD_INK = MAX_INK / 10.0, MAX_PRINT_INK = MAX_INK / 12.0;
        string printerName;
        /// <summary>
        /// Return or sets the name of the printer
        /// </summary>
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
        /// <summary>
        /// Return or sets how much Ink(%) is in the printer
        /// </summary>
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
        /// <summary>
        /// Return or sets how much pages are in the printer
        /// </summary>
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
        /// <summary>
        /// הפונקציה משנה את מדד הדפים בהתאם למספר שקיבלה ומתמודדת עם כל ההשלכות הנצרכות משינוי הערך
        /// </summary>
        /// <param name="num">המספר אותו רוצים להוסיף/להוריד ממדד הדפים</param>
        public void ChangePages(int num)
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
                }
                else
                    PageCount = MAX_PAGES;
            }
            else if (num <= 0)
            {
                if (num < -MAX_PRINT_PAGES)
                    return;
                else if (this.PageCount + num > 0)
                {
                    temp = this.PageCount + num;
                    this.PageCount = temp;
                }
                else
                {
                    temp = PageCount;
                    PageCount = 0;
                    if (PageMissing != null)
                        PageMissing(this, new PrinterEventArgs(true, "Out Of Paper(" + System.Math.Abs(temp + num) + ")", this.PrinterName));
                    //מה אמור לעשות פה?
                }
            }
            else
                return;
        }
        /// <summary>
        /// הפונקציה משנה את מדד הדיו בהתאם למספר שקיבלה ומתמודדת עם כל ההשלכות הנצרכות משינוי הערך
        /// </summary>
        /// <param name="num">המספר אותו רוצים להוסיף/להוריד ממדד הדיו</param>
        public void ChangeInk(double num)
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
                    temp = InkCount + num;
                    InkCount = temp;
                }
                else
                    InkCount = MAX_INK;
            }
            else if (num < 0)
            {
                if (num < -MAX_PRINT_INK)
                    return;
                else if (InkCount + num > 0)
                {
                    temp = InkCount + num;
                    InkCount = temp;
                    if (InkCount >= 1 && InkCount <= 15)
                    {
                        if (InkEmpty != null)
                            InkEmpty(this, new PrinterEventArgs(false, "Ink is Low! only " + InkCount + "% is left", this.PrinterName));
                    }
                    else if (InkCount < 1)
                    {
                        if (InkEmpty != null)
                            InkEmpty(this, new PrinterEventArgs(true, "Out Of Ink! only " + InkCount + "% is left", this.PrinterName));
                    }
                }
                else
                {
                    temp = InkCount;
                    InkCount = 0;
                    if (InkEmpty != null)
                        InkEmpty(this, new PrinterEventArgs(true, "Out Of Ink", this.PrinterName));
                }
            }
            return;
        }
        /// <summary>
        /// הפוקנציה מוסיפה מספר רנדומלי(בהתאם לטווח) של דיו למדפסת
        /// </summary>
        public void AddInk()
        {
            double num=MIN_ADD_INK + rand.NextDouble() * (MAX_INK - MIN_ADD_INK);
            if (CheckAccess())
                ChangeInk(num);
            else
                Dispatcher.BeginInvoke((Action<double>)(x=>ChangeInk(x)),num);
        }
        /// <summary>
        /// הפוקנציה מוסיפה מספר רנדומלי(בהתאם לטווח) של דפים למדפסת
        /// </summary>
        public void AddPages()
        {
            int num = MIN_ADD_PAGES + rand.Next(MAX_PAGES - MIN_ADD_PAGES);
            if (CheckAccess())
            ChangePages(num);
            else
                Dispatcher.BeginInvoke((Action<int>)(x => ChangePages(x)), num);
        }
        /// <summary>
        /// הפונקציה מדמה "הדפסה" של מספר דפים רנדומלי ומשתמש במספר דיו רנדומלי
        /// </summary>
        public void Print()
        {
            ChangePages(-rand.Next(MAX_PRINT_PAGES));
            ChangeInk(-(rand.NextDouble() * MAX_PRINT_INK));
        }
        public event EventHandler<PrinterEventArgs> PageMissing, InkEmpty;

        /// <summary>
        /// קונסטרקטור המאפס את הנתונים ההתחלתיים של המדפס ונותן לה את שמה
        /// </summary>
        public PrinterUserControl()
        {
            InitializeComponent();
            this.PrinterName = ("Printer " + (++amountOfPrinters));
            InkCount = MAX_INK / 2 + rand.NextDouble() * (MAX_INK / 2);//על מנת להכניס ערכים התחלתיים רנדומליים אבל יחסית גדולים
            PageCount = MAX_PAGES / 2 + rand.Next(MAX_PAGES / 2);//על מנת להכניס ערכים התחלתיים רנדומליים אבל יחסית גדולים
        }
        /// <summary>
        /// פונקציה המופעלת כאשר העכבר נכנס לתחום התווית ומשנה את גודל התווית
        /// </summary>
        private void printerNameLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            printerNameLabel.FontSize = 40;
        }
        /// <summary>
        /// פונקציה המופעלת כאשר העכבר יוצא מתחום התווית וומחזירה את גודל התווית לגודלה המוקרי 
        /// </summary>
        private void printerNameLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            printerNameLabel.FontSize = 16;
        }

        /// <summary>
        /// הפונקציה מעדכנת את מונה הדפים בהתאם לשינויים הנעשים בסליידר
        /// </summary>
        private void pageCountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PageCount = (int)e.NewValue;
            if (PageCount == 0 && PageMissing != null)
                PageMissing(this, new PrinterEventArgs(true, "Out Of Paper!", this.PrinterName));
        }
        /// <summary>
        /// שליחת "טכנאי" למלא מחדש את הדפים
        /// </summary>
        public void SendPageTechnician()
        {
            Thread.Sleep(rand.Next(5000,15000));
            this.AddPages();
        }
        /// <summary>
        /// שליחת "טכנאי" למלא מחדש את הדיו
        /// </summary>
        public void SendInkTechnician()
        {
            Thread.Sleep(rand.Next(5000, 15000));
            this.AddInk();
        }


    }
    /// <summary>
    /// משתני אירוע כלשהו של מדפסת
    /// </summary>
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