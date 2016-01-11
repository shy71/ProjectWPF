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

    class Extensions
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
    }

    public enum Kashrut
    {
        LOW, MEDIUM, HIGH
    }

    public enum Size
    {
        SMALL, MEDIUM, LARGE
    }
}
