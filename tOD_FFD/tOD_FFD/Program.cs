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
            public static void Main()
            {
                MyClass[] arr = new MyClass[4];
                arr[0] = new MyClass();
                List<int> list = new List<int> { 2, 1, 5, 3 };
                int x = 0;
                IEnumerable<int> v = from item in list
                                     where item == x++
                                     select item;
                Console.WriteLine("x is : "+x);
                foreach (var item in v)
                {
                    Console.WriteLine("{0}:{1}",x,item);
                    
                }
                foreach (var item in v)
                {
                    Console.WriteLine("{0}:{1}", x, item);
                }
                f1(ref x); 
                mydel temp = item => item.ToString();
                Console.WriteLine(DateTime.Parse(DateTime.Now.ToString()).ToString());

            }
        }
    }
}
