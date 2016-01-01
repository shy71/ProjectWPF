using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace tOD_FFD
{
    enum MyEnum
    {
        LOW,MED,HIGH
    }
    class Program
    {
        public static void Main()
        {

        MyEnum var=MyEnum.HIGH,low=MyEnum.MED;
            if(var<low)
                Console.WriteLine("high!");



            Console.ReadKey();

        }
    }
}
