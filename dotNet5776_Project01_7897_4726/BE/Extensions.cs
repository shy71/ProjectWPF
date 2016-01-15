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
                case "BRANCHWORKER":
                    return UserType.BranchWorker;
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
    { Client,BranchWorker,BranchManger,NetworkManger }


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
