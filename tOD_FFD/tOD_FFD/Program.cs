using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace tOD_FFD
{
    class Program
    {
        public static void Main()
        {
            int[] arr ={1,2,3,4,5,6,7,8,9,10};
            var v =( from item in arr
                    where item == 5
                    select item).ToList<int>();


            Console.ReadKey();

        }
    }
}
