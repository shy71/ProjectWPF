using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE;
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
                myBL.AddBranch(new Branch("beer sheva", "rechov a 1/2", "0585205020", "bob", 4, 0, Kashrut.MEDIUM));
                myBL.AddClient(new Client("yair","jerusalem 3/4",7812873,1899));
                //myBL.DeleteBranch(new Branch("Eilat", "freedom 98", "078496352", "oshri", 5, 3, Kashrut.LOW, 2));
                myBL.DeleteOrder(192334);

                Console.WriteLine("************************************************\n\n\n");
                Console.WriteLine(myBL.PriceOfOrder(new Order(2, "Beit Shemesh", DateTime.Now, Kashrut.LOW, 10934, 192334)));
                Console.WriteLine("************************************************\n\n\n");
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
