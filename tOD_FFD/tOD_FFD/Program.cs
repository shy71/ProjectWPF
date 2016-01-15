using System;
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
        public string S{get;set;}
        public Dish(string s, int a)
        {
            S = s;
            DishID = a;
        }
        public int DishID { get; set; }
    }
    class Program
    {
        public static void Main()
        {
            //לעזרא 
            float x = Convert.ToSingle("4.234");
            Console.WriteLine( x);
            Console.ReadKey();
            return;
            Dish d = new Dish("Hot dogs", 0);
            new XElement("Dish", from item in d.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                 select new XElement(item.Name, item.GetValue(d))).Save(@"Dish.xml");
        }

    }
}
