using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE;
using System.Threading.Tasks;

namespace Console_UI
{
    /*
     * להוסיף אפשרות לחפש הזמנות על ידי שם הלקוח
     * ואז שתהיה אפשרות בחירה להזמנה שהתכוונו אליה
     * 
     * לשנות את ה tostring 
     * לפונקציה שעוברת על כל הפרופטי פשוט
     * 
     * לשנות כמו שדיברנו את כל פנוקציות העדכון
     * 
     * קונסטרקרטורים
     * היה שגיאה שאם משנים את מה ששאילתה מצביעה אליה זה גורם בעיות
     * 
     * 
     * פונקציה גנרית שמחפשת את המחרוזת בכל אחד מהשדות
     * תחפש בשם ובהכול
     * ותחזיר אם אפשרות בחירה במקרה שיש יותר מאחד
     * 
     * לסיים switch
     * 
     * 
     * להוסיף הערות להכול
     * */
    class Program
    {
        
        static void Main(string[] args)
        {
            new PL().Run();
        }
    }
    class PL
    {

        BL.IBL myBL = BL.FactoryBL.getBL();
        public void Run()
        {
            myBL.Inti();
            bool exit = false;
            string address, creditCardstr, ageStr, strPrice, strID, phoneNumber;
            int creditCard, age, choice, ID;
            Size size;
            float price;
            Kashrut kosher;
            string str;
            int temp;
            while (!exit)
            {
                try
                {
                    switch (Menu())
                    {
                        case 1:
                            Console.WriteLine("Enter the name of the dish:");
                            str = Console.ReadLine();
                            Console.WriteLine("Enter the number of the size of the dish:");
                            Console.WriteLine("1) Large");
                            Console.WriteLine("2) Medium");
                            Console.WriteLine("3) Small");
                            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                                throw new Exception("Invaid input.");
                            switch(choice)
                            {
                                case 1:
                                    size = Size.LARGE;
                                    break;
                                case 2:
                                    size = Size.MEDIUM;
                                    break;
                                case 3:
                                    size = Size.SMALL;
                                    break;
                                default:
                                    size = Size.MEDIUM;
                                    break;
                            }
                            Console.WriteLine("Enter the price of the dish:");
                            strPrice = Console.ReadLine();
                            while(!float.TryParse(strPrice,out price) || price<=0)
                            {
                                Console.WriteLine("Invalid price. Please enter the price again:");
                                strPrice=Console.ReadLine();
                            }
                            Console.WriteLine("Enter the number of the level of kashrut of the dish:");
                            Console.WriteLine("1) High");
                            Console.WriteLine("2) Medium");
                            Console.WriteLine("3) Low");
                            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                                throw new Exception("Invaid input.");
                            switch(choice)
                            {
                        case 1:
                                    kosher = Kashrut.HIGH;
                                    break;
                                case 2:
                                    kosher = Kashrut.MEDIUM;
                                    break;
                                case 3:
                                    kosher = Kashrut.LOW;
                                    break;
                                default:
                                    kosher = Kashrut.MEDIUM;
                                    break;
                            }
                            Console.WriteLine("Enter an ID for your dish, or if you don't want to enter 0");
                            strID=Console.ReadLine();
                            if(!int.TryParse(strID,out ID))
                                myBL.AddDish(new Dish(str,size,price,kosher));
                            myBL.AddDish(new Dish(str, size, price, kosher,ID));
                            break;
                        case 2:
                            Console.WriteLine("Enter the name of the client:");
                            str = Console.ReadLine();
                            Console.WriteLine("Enter the address of the client:");
                            address = Console.ReadLine();
                            Console.WriteLine("Enter the credit card of the client:");
                            creditCardstr = Console.ReadLine();
                            while (!int.TryParse(creditCardstr, out creditCard))
                            {
                                Console.WriteLine("Invalid credit card number. Please enter the credit card number again:");
                                strPrice=Console.ReadLine();
                            }
                            Console.WriteLine("Enter the age of the client:");
                            ageStr = Console.ReadLine();
                            while (!int.TryParse(ageStr, out age))
                            {
                                Console.WriteLine("Invalid age. Please enter the age again:");
                                strPrice=Console.ReadLine();
                            }
                            if(age<18)
                                throw new Exception("The client has to be at least 18 years old.");
                            Console.WriteLine("Enter an ID for your client, or if you don't want to enter 0");
                            strID=Console.ReadLine();
                            if(!int.TryParse(strID,out ID))
                                myBL.AddClient(new Client(str,address,creditCard,age,ID));
                            myBL.AddClient(new Client(str,address,creditCard,age));
                            break;
                        case 3:
                            Console.WriteLine("Enter the name of the branch:");
                            str = Console.ReadLine();
                            Console.WriteLine("Enter the address of the branch:");
                            address = Console.ReadLine();
                            Console.WriteLine("Enter the phone number of the branch:");
                            phoneNumber = Console.ReadLine();
                            Console.WriteLine("Enter the boss of the branch:");
                            creditCardstr = Console.ReadLine();
                            Console.WriteLine("Enter the amount of employees of the branch:");
                            ageStr = Console.ReadLine();
                            while (!int.TryParse(ageStr, out age))
                            {
                                Console.WriteLine("Invalid amount. Please enter the age again:");
                                strPrice=Console.ReadLine();
                            }
                            Console.WriteLine("Enter the available messangers of the branch:");
                            ageStr = Console.ReadLine();
                            while (!int.TryParse(ageStr, out creditCard))
                            {
                                Console.WriteLine("Invalid amount. Please enter the age again:");
                                strPrice=Console.ReadLine();
                            }
                            Console.WriteLine("Enter the number of the level of kashrut of the dish:");
                            Console.WriteLine("1) High");
                            Console.WriteLine("2) Medium");
                            Console.WriteLine("3) Low");
                            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                                throw new Exception("Invaid input.");
                            switch(choice)
                            {
                                case 1:
                                    kosher = Kashrut.HIGH;
                                    break;
                        case 2:
                                    kosher = Kashrut.MEDIUM;
                            break;
                        case 3:
                                    kosher = Kashrut.LOW;
                                    break;
                                default:
                                    kosher = Kashrut.MEDIUM;
                                    break;
                            }
                            Console.WriteLine("Enter an ID for your branch, or if you don't want to enter 0");
                            strID=Console.ReadLine();
                            if(!int.TryParse(strID,out ID))
                                myBL.AddBranch(new Branch(str,address,phoneNumber,creditCardstr,age,creditCard,kosher,ID));
                            myBL.AddBranch(new Branch(str,address,phoneNumber,creditCardstr,age,creditCard,kosher));
                            break;
                        case 4:
                            //myBL.AddOrder(new Order());
                            break;
                        case 5:
                            //myBL.AddDishOrder(new DishOrder());
                            break;
                        case 6:
                            myBL.DeleteDish(GetID("Enter the ID of the dish you wish to delete"));
                            break;
                        case 7:
                            myBL.DeleteClient(GetID("Enter the ID of the client you wish to delete"));
                            break;
                        case 8:
                            myBL.DeleteBranch(GetID("Enter the ID of the branch you wish to delete"));
                            break;
                        case 9:
                            myBL.DeleteOrder(GetID("Enter the ID of the order you wish to delete"));
                            break;
                        case 10:
                            temp = GetID("Enter the ID of the order you wish to delete dishes from");
                            IEnumerable<Dish> dishList = from item in myBL.GetAllDishOrders((item) => item.OrderID == temp).ToList()
                                                         select myBL.GetAllDishs(var => var.ID == item.DishID).ToList()[0];
                            myBL.DeleteDish(Choose<Dish>(dishList));
                            break;
                        case 11:
                            break;
                        case 12:
                            break;
                        case 13:
                            break;
                        case 14:
                            break;
                        case 15:
                            break;
                        case 16:
                            break;
                        case 17:
                            break;
                        case 18:
                            break;
                        case 19:
                            break;
                        case 20:
                            myBL.PrintAll();
                            break;
                        default:
                            Console.WriteLine("\n\nGood By! Hope to see you again at our restaurants");
                            exit = true;
                            break;
                    }
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
        }
        int GetID(string str = null)
        {
            int temp;
            while (true)
            {
                Console.WriteLine(str);
                if (int.TryParse(Console.ReadLine(), out temp))
                    if (temp < 99999999 && temp > 0)
                        return temp;
                    else
                        Console.WriteLine("The ID is invalid! please try again\n\n");
                else
                    Console.WriteLine("the number you've entered is invalid! please try again\n\n");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
        T Choose<T>(IEnumerable<T> list)
        {
            if(list.Count<T>()==0)
                throw new Exception("There are no choices to choose from.");
            int i=1,choice;
            Console.WriteLine("Choose from the following options: ");
            foreach(T item in list)
            {
                Console.WriteLine(i+":");
                Console.WriteLine(item);
                i++;
            }
            while(!int.TryParse(Console.ReadLine(),out choice) || choice<1 || choice>=i)
                Console.WriteLine("Invalid Choice. Choose again.");
            i = 0;
            foreach(T item in list)
            {
                i++;
                if (i == choice)
                    return item;
            }
            throw new Exception("Invalid choice.");
        }
        int Menu()
        {
            int num;
            string input;
            while (true)
            {
                Console.WriteLine("Menu: Project 1 - Ezra Block - Shy Tennenbaum\n");
                Console.WriteLine("Press the number of the operation you wish to do:");
                Console.WriteLine("1. Create new dish in the menu of the restaurant");
                Console.WriteLine("2. Create new Client");
                Console.WriteLine("3. Create new Branch");
                Console.WriteLine("4. Create new Order");
                Console.WriteLine("5. Add dishs to an order");
                Console.WriteLine("6. Delete a dish from the menu of the restaurant");
                Console.WriteLine("7. Delete a Client");
                Console.WriteLine("8. Delete a Branch");
                Console.WriteLine("9. Delete an Order");
                Console.WriteLine("10. Delete dishs from an order");
                Console.WriteLine("11. Update a dish in the menu of the restaurant");
                Console.WriteLine("12. Update Client's details");
                Console.WriteLine("13. Update Branch's details");
                Console.WriteLine("14. Update Order's details");
                Console.WriteLine("15. Update the amount of dishs order from one kind from an order");
                Console.WriteLine("16. Search and print all of the orders of a client");
                Console.WriteLine("17. Search and print all of the orders from a certain branch");
                Console.WriteLine("18. Search and print all of the orders of a certain dish");
                Console.WriteLine("19. Caclute an order price(by the order ID)");//להוסיף אפשרות לפי שם הלקוח
                Console.WriteLine("20. Print the entire DataBase");
                Console.WriteLine("21. Exit\n");
                input = Console.ReadLine();
                if ((!int.TryParse(input, out num)) || num < 1 || num > 22)
                {
                    Console.WriteLine("the number you've entered is invalid! please try again\n\n");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                    return num;
            }
        }

    }
}