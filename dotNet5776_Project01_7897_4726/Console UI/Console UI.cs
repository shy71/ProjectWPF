using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_UI
{
    class Program
    {
       
        static void Main(string[] args)
        {
            try
            {
                BL.BL myBL = new BL.BL();
                myBL.Inti();
                myBL.PrintAll();
                //finish UI and check all of the functions
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
    }
}
