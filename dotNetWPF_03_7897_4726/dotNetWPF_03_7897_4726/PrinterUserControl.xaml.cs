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
        //Constant values:
        const int MAX_PAGES = 400;
        public static readonly double MaxPages = MAX_PAGES;//Property for MAX_PAGES
        const int MIN_ADD_PAGES = MAX_PAGES / 10, MAX_PRINT_PAGES = MAX_PAGES / 20;
        const double MAX_INK = 100;
        const double MIN_ADD_INK = MAX_INK / 10.0, MAX_PRINT_INK = MAX_INK / 15.0;

        static Random rand = new Random();//Random

        static int amountOfPrinters = 0;

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

        /// <summary>
        /// Printer Events
        /// </summary>
        public event EventHandler<PrinterEventArgs> PageMissing, InkEmpty;
        public EventHandler<EventArgs> TechnicianArrived;

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


        //Changing Functions:
        /// <summary>
        /// הפונקציה משנה את מדד הדפים בהתאם למספר שקיבלה ומתמודדת עם כל ההשלכות הנצרכות משינוי הערך
        /// </summary>
        /// <param name="num">המספר אותו רוצים להוסיף/להוריד ממדד הדפים</param>
        public void ChangePages(int num)
        {
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
            double temp;
            if (num > MIN_ADD_INK)
            {
                if (InkCount + num < MAX_INK)
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
        }
        

        //Adding Functions:
        /// <summary>
        /// הפוקנציה מוסיפה מספר רנדומלי(בהתאם לטווח) של דפים למדפסת
        /// </summary>
        public void AddPages()
        {
            int num;
            if (MAX_PAGES - PageCount - MIN_ADD_PAGES >= 0)
                num = MIN_ADD_PAGES + rand.Next(MAX_PAGES - PageCount - MIN_ADD_PAGES);
            else
                return;
            if (CheckAccess())
                ChangePages(num);
            else
                Dispatcher.BeginInvoke((Action<int>)(x => ChangePages(x)), num);
        }
        /// <summary>
        /// הפוקנציה מוסיפה מספר רנדומלי(בהתאם לטווח) של דיו למדפסת
        /// </summary>
        public void AddInk()
        {
            double num;
            if (MAX_INK - InkCount - MIN_ADD_INK >= 0)
                num = MIN_ADD_INK + rand.NextDouble() * (MAX_INK - InkCount - MIN_ADD_INK);
            else
                return;
            if (CheckAccess())
                ChangeInk(num);
            else
                Dispatcher.BeginInvoke((Action<double>)(x=>ChangeInk(x)),num);
        }


        //Technician Functions:
        /// <summary>
        /// שולח טכנאי מתאים בהתאם למשתנה הובליאני
        /// </summary>
        /// <param name="IsPagesEmpty">האם צריך למלא את הדפים, או אחרת למלא את הדיו</param>
        public void SendTechnician(bool IsPagesEmpty)
        {
            if (IsPagesEmpty)
                new Thread(sendPageTechnician).Start();
            else
                new Thread(sendInkTechnician).Start();
        }
        /// <summary>
        /// שליחת "טכנאי" למלא מחדש את הדפים
        /// </summary>
        void sendPageTechnician()
        {
            Thread.Sleep(rand.Next(50000, 60000));
            this.AddPages();
            if (CheckAccess()&& TechnicianArrived!=null)
                TechnicianArrived(this, null);
            else if(TechnicianArrived!=null)
                Dispatcher.BeginInvoke((Action)(() => TechnicianArrived(this, null)));
        }
        /// <summary>
        /// שליחת "טכנאי" למלא מחדש את הדיו
        /// </summary>
        void sendInkTechnician()
        {
            Thread.Sleep(rand.Next(50000,60000));
            AddInk();
            if (CheckAccess() && TechnicianArrived != null)
                TechnicianArrived(this, null);
            else if(TechnicianArrived!=null)
                Dispatcher.BeginInvoke((Action)(() => TechnicianArrived(this, null)));
        }


        /// <summary>
        /// הפונקציה מדמה "הדפסה" של מספר דפים רנדומלי ומשתמש במספר דיו רנדומלי
        /// </summary>
        public void Print()
        {
            ChangePages(-rand.Next(MAX_PRINT_PAGES));
            ChangeInk(-(rand.NextDouble() * MAX_PRINT_INK));
        }

        //Controllers(WPF) Functions:

        /// <summary>
        /// פונקציה המופעלת כאשר העכבר נכנס לתחום התווית ומכפיל את גודל התווית
        /// </summary>
        private void printerNameLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            printerNameLabel.FontSize = printerNameLabel.FontSize*2;
        }
        /// <summary>
        /// פונקציה המופעלת כאשר העכבר יוצא מתחום התווית וומחזירה את גודל התווית לגודלה המוקרי 
        /// </summary>
        private void printerNameLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            printerNameLabel.FontSize = printerNameLabel.FontSize/2;
        }
        /// <summary>
        /// הפונקציה מעדכנת את מונה הדפים בהתאם לשינויים הנעשים בסליידר
        /// </summary>
        private void pageCountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (PageCount != e.NewValue && e.OldValue == 0 && TechnicianArrived != null)// דואג לכך שאם אין מדפסות שעובדות ושינו באופן ידני את כמות הדפים הוא יפעיל מחדש את הכפתור
            {
                PageCount = (int)e.NewValue;
                TechnicianArrived(this,null);
            }
            else
                PageCount = (int)e.NewValue;
            if (PageCount == 0 && PageMissing != null)
                PageMissing(this, new PrinterEventArgs(true, "Out Of Paper!", this.PrinterName));

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