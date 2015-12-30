using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace tOD_FFD
{
    class temp
    {
        public static int MakeID()
        {
            string Name="shy", Size="large", Price="78";
            string[] DishComponents = new string[3]{Name,Size,Price};
            //DishComponents[0] = Name;
            //DishComponents[1] = Size;
            //DishComponents[2] = Price;
            return DishComponents.Sum<Func<string,object>>((item) => (item.Select(r => (int)r).ToArray()));
        }
    }

    class Program
    {
        static public void Main()
        {
            temp.MakeID();
        }


    }
}
