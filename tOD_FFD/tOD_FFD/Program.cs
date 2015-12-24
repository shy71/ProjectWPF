using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tOD_FFD
{
    static class MyClass
    {
        public static void Make(params string[] str)
        {
           Console.WriteLine(str.Select((item, index) => (item.Select((r,index1) => (int)r*(int)Math.Pow(10,index1)).Sum()) * (int)Math.Pow(10, index)).Sum());
           Console.ReadLine();

       }
    }
    class Program
    {
        static public void Main(params string[] str)
        {
            MyClass.Make("h", "f", "qqqrf", "t", "r", "a");
        }
    }
}
