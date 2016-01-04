using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Extensions
    {
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
