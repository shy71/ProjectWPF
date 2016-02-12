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
        /// <summary>
        /// creates an ID
        /// </summary>
        /// <returns></returns>
        int MakeID();
    }
    public class EventValue : EventArgs
    {
        public object Value { get; set; }
        public string pName { get; set; }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pName"></param>
        public EventValue(object value, string pName = null)
        {
            Value = value;
            this.pName = pName;
        }
    }
    public static class Extensions
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
        /// <summary>
        /// changes a string to a kashrut level
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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
        /// <summary>
        /// changes a string to a Size level
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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
        /// <summary>
        /// changes a string to a userType 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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
        public static bool IsActive(this Order order)
        {
            return (order.Delivered == false && order.Date != DateTime.MinValue);
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

  
    public class GroupSum
    {
        public GroupSum(Kashrut kashrutlevel, double newsum,string LowHeadr,string toolTip)
        {
            Kashrut = kashrutlevel;
            Sum = newsum;
            LowerHeadr = LowHeadr;
            ToolTip = toolTip.Replace("\t", "");
        }
        public GroupSum(string str, double newsum, string LowHeadr, string toolTip)
        {
            StrKey = str;
            Sum = newsum;
            LowerHeadr = LowHeadr;
            ToolTip = toolTip.Replace("\t", "");
        }
        public GroupSum(int num, double newsum, string LowHeadr, string toolTip)
        {
            NumKey = num;
            Sum = newsum;
            LowerHeadr = LowHeadr;
            ToolTip = toolTip.Replace("\t", "");
        }
        public GroupSum(int num, int amount, string LowHeadr, string toolTip)
        {
            NumKey = num;
       Sum = amount;
       LowerHeadr = LowHeadr;
       ToolTip = toolTip.Replace("\t", "");
        }
        public GroupSum(DayOfWeek theDay, int amount, string LowHeadr, string toolTip)
        {
            Day = theDay;
            Sum = amount;
            LowerHeadr = LowHeadr;
            ToolTip = toolTip.Replace("\t","");
        }
        public GroupSum(DayOfWeek theDay, float newsum, string LowHeadr, string toolTip)
        {
            Day = theDay;
        Sum = newsum;
        LowerHeadr = LowHeadr;
        ToolTip = toolTip;
        }
     
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

        private string lowerHeadr;

        public string LowerHeadr
        {
            get { return lowerHeadr; }
            set { lowerHeadr = value; }
        }

        private string toolTip;

        public string ToolTip
        {
            get { return toolTip; }
            set { toolTip = value; }
        }
        
        string strKey;
        public string StrKey { get { return strKey; } set { strKey = value; } }
        int numKey;
        public int NumKey { get { return numKey; } set { numKey = value; } }
        double sum;
        public double Sum { get { return sum; } set { sum = value; } }
    }
}
