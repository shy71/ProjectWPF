using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Interface for every class that we want to have the unique ID system
    /// </summary>
    public interface InterID
    {
        int ID { get; set; }
        int MakeID();
    }
    public class EventValue : EventArgs
    {
        public object Value { get; set; }
        public string pName { get; set; }
        public EventValue(object value, string pName = null)
        {
            Value = value;
            this.pName = pName;
        }
    }
    public class Extensions
    {
        /// <summary>
        /// Make a unique ID from any numbers of strings
        /// </summary>
        /// <param name="str">The strings from them it will make the unique ID</param>
        /// <returns>The unique ID</returns>
        internal static int MakeID(params string[] str)
        {
            return str.Select((item, index) => (item.Sum(r => (int)r)) * (int)Math.Pow(10, index)).Sum() % 100000000;
        }
        public static Kashrut ToKashrut(string str)
        {
            switch (str.ToUpper())
            {
                case "HIGH":
                    return Kashrut.HIGH;
                case "MEDIUM":
                    return Kashrut.MEDIUM;
                case "LOW":
                    return Kashrut.LOW;
                default:
                    throw new Exception("Invalid kashrut level.");
            }
        }
        public static Size ToSize(string str)
        {
            switch (str.ToUpper())
            {
                case "LARGE":
                    return BE.Size.LARGE;
                case "MEDIUM":
                    return BE.Size.MEDIUM;
                case "SMALL":
                    return BE.Size.SMALL;
                default:
                    throw new Exception("Invalid size level.");
            }
        }
        public static UserType ToUserType(string str)
        {
            switch (str.ToUpper())
            {
                case "CLIENT":
                    return UserType.Client;
                case "BRANCHMANGER":
                    return UserType.BranchManger;
                case"NETWORKMANGER":
                    return UserType.NetworkManger;
                default:
                    throw new Exception("Invalid size level.");
            }
        }
    }

    public enum Kashrut
    {
        LOW, MEDIUM, HIGH
    }

    public enum Size
    {
        SMALL, MEDIUM, LARGE
    }
    //new
    public enum UserType
    { Client,BranchManger,NetworkManger }

    public enum CurrentPlacing
    { Action,Info,Edit}
    public enum GroupingByType
    {
        Address,
        Dish,
        Date,
        Branch,
        DishKashrut,
        BranchKashrut,
        WeekDays,
        DishesAmountbyDish,
        DishesAmountbyBranch,
        DishAmountbyWeekDays
    }

    public class GroupSum
    {
        public GroupSum(GroupingByType gtype,Kashrut kashrutlevel, double newsum)
        {
            Type = gtype;
            Kashrut = kashrutlevel;
            Sum = newsum;
        }
        public GroupSum(GroupingByType gtype, string str, double newsum)
        {
            StrKey = str;
            Type = gtype;
            Sum = newsum;
        }
        public GroupSum(GroupingByType gtype, int num, double newsum)
        {
            NumKey = num;
            Type = gtype;
            Sum = newsum;
        }
        public GroupSum(GroupingByType gtype,int num, int amount)
        {
            NumKey = num;
            Type = gtype;
            Sum = amount;
        }
        public GroupSum(GroupingByType gtype, DayOfWeek theDay,int amount)
        {
            Day = theDay;
            Type = gtype;
            Sum = amount;
        }
        public GroupSum(GroupingByType gtype, DayOfWeek theDay, float newsum)
        {
            Day = theDay;
            Type = gtype;
            Sum = newsum;
        }
        GroupingByType type;
        private DayOfWeek day;

        public DayOfWeek Day
        {
            get { return day; }
            set { day = value; }
        }
        
        private Kashrut kashrut;
        public Kashrut Kashrut
        {
            get { return kashrut; }
            set { kashrut = value; }
        }
        
        public GroupingByType Type { get { return type; } set { type = value; } }
        string strKey;
        public string StrKey { get { return strKey; } set { strKey = value; } }
        int numKey;
        public int NumKey { get { return numKey; } set { numKey = value; } }
        double sum;
        public double Sum { get { return sum; } set { sum = value; } }
    }
    ////כרגע לא בשימוש 
    ////אולי אחר כך
    //class MessageBoxException : Exception
    //{
    //    string text, caption;
    //    MessageBoxButton button;
    //    MessageBoxImage icon;
    //    public MessageBoxException(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
    //        : base()
    //    {
    //        text = messageBoxText;
    //        this.caption = caption;
    //        this.button = button;
    //        this.icon = icon;
    //    }
    //}

}
