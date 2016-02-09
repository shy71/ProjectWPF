using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;

namespace tOD_FFD
{
    enum MyEnum
    {
        LOW, MED, HIGH
    }
    interface Inta
    { }
    class Dish : Inta
    {
        public string S { get; set; }
        public Dish(string s, int a)
        {
            S = s;
            DishID = a;
        }
        public int DishID { get; set; }
    }
    static public class Tools
    {
        public static IEnumerable<int> Shy(this int[] arr, Func<int, bool> func)
        {
            return arr.Where(func);
        }
    }
    class MyClass
    {
        delegate string mydel(int a);
        public MyClass()
        {
            Console.WriteLine("a");
        }
        class Program
        {
            public static int f1(ref int a)
            {
                return 2;
            }
        }
    }
}
