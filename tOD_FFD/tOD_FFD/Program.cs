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
    class Inta
    {
        public virtual void aa()
        {
            Console.WriteLine("a");
        }
    }
    class grandfather { public virtual string hello() { return "i am grandfather"; } }
    class father : grandfather { public virtual string hello() { return "i am father"; } }
    class son : father { public override string hello() { return "i am son"; } }
    class Dish : Inta
    {
        public string S { get; set; }
        public Dish a;
        public Dish(string s, int a)
        {
            S = s;
            DishID = a;
        }
        public Dish()
            : this("aa", 5)
        { }
        public int DishID { get; set; }
        override public void aa()
        {
            Console.WriteLine("aaaaaa");
        }
        public string this[string s]
        {
            get { return S; }
        }
    }
    class Program
    { 
        static void Main(string[] args)
        {
            int s=5;
            Console.WriteLine(s is 7);
        }  

        static public IEnumerable<T> func<T>(T shy, T ezra, T itai)
        {
            yield return shy;
            yield return ezra;
            yield return itai;
        }

    }
}
