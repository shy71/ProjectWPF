using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE;
using System.Threading.Tasks;

namespace Console_UI
{
    /*
     * 
     * 
     * לסיים switch
     * 
     * לבדוק איך כל מערך השגיאות עובד ולנהול אותו
     * 
     * להריץ כמה עשרות ניסיונות
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
            int tempID,temp,num;
            float price;
            Order order;
            while (!exit)
            {
                try
                {
                    switch (Menu())
                    {
                        case 1:
                            #region Add Dish
                            myBL.AddDish(new Dish(
                                GetString("Enter the name of the dish:", item => item.Length > 2, "The dish name must be at least 3 characters")
                                , SwtichCase(int.Parse(GetString("Enter the number of the size of the dish:\n1) Large\n2) Medium\n3) Small", item => (item == "1") || (item == "2") || (item == "3"), "Invalid input.")), Size.LARGE, Size.MEDIUM, Size.SMALL)
                                , float.Parse(GetString("Enter the price of the dish:", item => float.TryParse(item, out price) && price > 0, "Invalid price. price cant be negative or contain letters."))
                                , SwtichCase(int.Parse(GetString("Enter the number of the level of kashrut of the dish:\n1) High\n2) Medium\n3) Low", item => (item == "1") || (item == "2") || (item == "3"), "Invalid input.")), Kashrut.HIGH, Kashrut.MEDIUM, Kashrut.LOW)
                                , int.Parse(GetString("Enter an ID for your dish, or if you don't want to enter 0(recommended) and the system will generate a unique id:", item => int.TryParse(item, out tempID) && tempID >= 0 && tempID < 100000000, "The ID must be 8 or less numbers, only numbers and positive"))));
                            #endregion
                            break;
                        case 2:
                            #region  Add Client
                            myBL.AddClient(new Client(
                                GetString("Enter the name of the client:", item => item.Length > 2, "Client name must be at least 3 characters")
                                , GetString("Enter the address of the client:", item => item.Length > 2, "Client address must be at least 3 characters")
                                , int.Parse(GetString("Enter the credit card of the client:", item => int.TryParse(item, out temp) && temp > 0, "The creditCard can contain only numbers and must be positive"))
                                , int.Parse(GetString("Enter the age of the client:", item => int.TryParse(item, out temp) && temp >= 18, "Client age must be above 18 years old"))
                                , int.Parse(GetString("Enter an ID for your Client, or if you don't want to enter 0(recommended) and the system will generate a unique id:", item => int.TryParse(item, out tempID) && tempID >= 0 && tempID < 100000000, "The ID must be 8 or less numbers, only numbers and positive"))));
                            #endregion
                            break;
                        case 3:
                            #region  Add Branch
                            myBL.AddBranch(new Branch(
                             GetString("Enter the name of the branch:", item => item.Length > 2, "branch name must be at least 3 characters")
                             , GetString("Enter the branch of the client:", item => item.Length > 2, "branch address must be at least 3 characters")
                             , GetString("Enter the phone number of the branch:", item => item.Length >= 7 && int.TryParse(item, out temp), "The min lenght of a phone number is 7 numbers, and it must contain only numbers")
                             , GetString("Enter the name of the boss of the branch:", item => item.Length > 2, "the boss of the branch name must be at least 3 characters")
                             , int.Parse(GetString("Enter the amount of employees of the branch:", item => int.TryParse(item, out temp) && temp > 0, "The amount of employees in the branch must be above zero, or youve entered Invalid input"))
                             , int.Parse(GetString("Enter the available messangers of the branch:", item => int.TryParse(item, out temp), "Invalid Input"))
                             , SwtichCase(int.Parse(GetString("Enter the number of the level of kashrut of the branch:\n1) High\n2) Medium\n3) Low", item => (item == "1") || (item == "2") || (item == "3"), "Invalid input.")), Kashrut.HIGH, Kashrut.MEDIUM, Kashrut.LOW)
                             , int.Parse(GetString("Enter an ID for your Client, or if you don't want to enter 0(recommended) and the system will generate a unique id:", item => int.TryParse(item, out tempID) && tempID >= 0 && tempID < 100000000, "The ID must be 8 or less numbers, only numbers and positive"))));
                            #endregion
                            break;
                        case 4:
                            #region Add Order
                            order = new Order(
                                ManageBing<Branch>("Search for The Branch that you are going to order from(just search it using Bing!)")
                                , ManageBing<Client>("Search for The Client that is making the order(just search it using Bing!)")
                                , DateTime.Now.AddHours(-int.Parse(GetString("Enter the number of hours that past since this order was created(or 0 if its just created now)", item => int.TryParse(item, out temp) && temp >= 0, "The number of hours needs to be a positive number")))
                                , SwtichCase(int.Parse(GetString("Enter the number of the level of kashrut of the Order:\n1) High\n2) Medium\n3) Low", item => (item == "1") || (item == "2") || (item == "3"), "Invalid input.")), Kashrut.HIGH, Kashrut.MEDIUM, Kashrut.LOW) //התנאי של הכשרות פה ייבדק ב BL
                                , int.Parse(GetString("Enter an ID for your Client, or if you don't want to enter 0(recommended) and the system will generate a unique id:", item => int.TryParse(item, out tempID) && tempID >= 0 && tempID < 100000000, "The ID must be 8 or less numbers, only numbers and positive")));
                            if (SwtichCase(int.Parse(GetString("Does the client is ordering the Dish home?(enter 1) or to someplace else(2)", item => item == "1" || item == "2", "Invalid Input.")), false, true))
                                order.Address = GetString("Enter the address for the order:", item => item.Length > 2, "order address must be at least 3 characters");
                            myBL.AddOrder(order);
                            #endregion
                            break;
                        case 5:
                            #region Add DishOrder
                            myBL.AddDishOrder(new DishOrder(
                                ManageBing<Order>("Search for The Order you want to add dish to(just search it using Bing)")
                                , ManageBing<Dish>("Search for The Dish you want to add to the order(just search it using Bing!)")
                                ,int.Parse(GetString("How many dishs of that dish do you want to add to the order?)",item=>int.TryParse(item, out temp),"The number of dishs to add has to be a positive number."))));
                            #endregion
                            break;
                        case 6:
                            #region Delete Item
                            InterID res = Bing("Search for The Item you wish to delete from the database of the restaurant");
                            switch (res.GetType().Name)
                            {
                                case "Dish":
                                    myBL.DeleteDish(res as Dish);
                                    break;
                                case "Branch":
                                    myBL.DeleteBranch(res as Branch);
                                    break;
                                case "Client":
                                    myBL.DeleteClient(res as Client);
                                    break;
                                case "Order":
                                    myBL.DeleteOrder(res as Order);
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        case 7:
                            #region  delete DishOrders
                            order = ManageBing<Order>("Search for the Order you wish to delete orders from");
                            List<DishOrder> list = myBL.GetAllDishOrders(item => item.OrderID == order.ID).ToList();
                            temp = 0;
                            int temp2;
                            foreach (DishOrder item in list)
                            {
                                temp++;
                                Console.WriteLine(temp + ") " + "Name: " + myBL.GetAllDishs(var => var.ID == item.DishID).First().Name + " , Amount: " + item.DishAmount);
                            }
                            Console.WriteLine(temp + ") Cancel");
                            temp2 = int.Parse(GetString("Enter the number near the dishs you wish to remove from the order", item => int.TryParse(item, out temp2) && temp2 > 0 && temp2 <= temp + 1, "Invalid Input."));
                            if (temp2 == temp + 1)
                                break;
                            myBL.DeleteDishOrder(list[temp - 1]);
                            #endregion
                            break;
                        case 8:
                            #region Profits Grouping
                            switch (GetString("Do you want profits by Dishs(1), Dates(2) or Clients(3)?", item => (item == "1") || (item == "2") || (item == "3"), "Invalid Input."))
                            {
                                case "1":
                                    #region By Dishs
                                    temp = 0;
                                    var grouping = myBL.GetProfitByDishs();
                                    foreach (IGrouping<int, float> item in grouping)
                                    {
                                        Console.WriteLine("Details for profit from Dish" + myBL.GetAllDishs(item2 => item2.ID == item.Key).First().Name + " (" + item.Key + "):");
                                        foreach (float item2 in item)
                                        {
                                            temp++;
                                            Console.WriteLine("\t(" + temp + ") " + item2);
                                        }
                                        Console.WriteLine("\tTotal of: " + item.Sum() + " Profit from " + item.Count() + " orders for this dish.");
                                    }
                                    Console.WriteLine("\nTotal of:" + grouping.Sum(item => item.Sum()) + " Profit from " + grouping.Sum(item => item.Count()) + " orders of " + grouping.Count() + " diffrent dishs");
                                    #endregion
                                    break;
                                case "2":
                                    #region By Dates
                                    temp = 0;
                                    var grouping2 = myBL.GetProfitByDates();
                                    foreach (IGrouping<string, float> item in grouping2)
                                    {
                                        Console.WriteLine("Details for profit from Date " + item.Key+":");
                                        foreach (float item2 in item)
                                        {
                                            temp++;
                                            Console.WriteLine("\t(" + temp + ") " + item2);
                                        }
                                        Console.WriteLine("\tTotal of: " + item.Sum() + " Profit from " + item.Count() + " orders for this date.");
                                    }
                                    Console.WriteLine("\nTotal of:" + grouping2.Sum(item => item.Sum()) + " Profit from " + grouping2.Sum(item => item.Count()) + " orders of " + grouping2.Count() + " diffrent dates");
                                    #endregion
                                    break;
                                case "3":
                                    #region By Clients
                                    temp = 0;
                                    var grouping3 = myBL.GetProfitByAddress();
                                    foreach (IGrouping<int, float> item in grouping3)
                                    {
                                        Console.WriteLine("Details for profit from Client" + myBL.GetAllClients(item2 => item2.ID == item.Key).First().Name + " (" + item.Key + "):");
                                        foreach (float item2 in item)
                                        {
                                            temp++;
                                            Console.WriteLine("\t(" + temp + ") " + item2);
                                        }
                                        Console.WriteLine("\tTotal of: " + item.Sum() + " Profit from " + item.Count() + " orders form this client.");
                                    }
                                    Console.WriteLine("\nTotal of:" + grouping3.Sum(item => item.Sum()) + " Profit from " + grouping3.Sum(item => item.Count()) + " orders of " + grouping3.Count() + " diffrent clients");
                                    #endregion
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        case 9:
                            #region Calculate Order Price
                            order = ManageBing<Order>("Search for the order you want to Calculate it's total price");
                            if (order != null)
                                Console.WriteLine("The Price of the order is: " + myBL.PriceOfOrder(order));
                            #endregion
                            break;
                        case 10:
                            #region Mark as Delivered
                            order = ManageBing<Order>("Search for the order you want to makr as delivered");
                            if (order != null)
                                order.Delivered = true;
                            #endregion
                            break;
                        case 11:
                            #region Print all Undelivered orders
                            temp = 0;
                            temp2 = 0;
                            foreach (Order item in myBL.GetAllOrders(item => item.Delivered == false))
                            {
                                Console.WriteLine(item);
                                Console.WriteLine("Dishs in the Order:");
                                foreach (DishOrder item2 in myBL.GetAllDishOrders(var=>var.OrderID==item.ID))
                                {
                                    temp++;
                                    Console.WriteLine("("+temp + ") " + "Name: " + myBL.GetAllDishs(var => var.ID == item2.DishID).First().Name + " , Amount: " + item2.DishAmount);
                                }
                                temp2++;
                            }
                            Console.WriteLine("Total of "+temp2+" Undelivered Orders are in the restaurant database(this number may change as time goes on)");
                            #endregion
                            break;
                        case 12:
                            #region Search and Print Order
                            myBL.PrintOrder(ManageBing<Order>("Search for The Order you wish to print"));
                            #endregion
                            break;
                        case 13:
                            #region Update Item
                            res = Bing("Search for The Item you wish to Update ");
                            switch (res.GetType().Name)
                            {
                                case "Dish":
                                    #region Update Dish
                                    myBL.UpdateDish(new Dish(
                                GetString("Enter the updated name of the dish:", item => item.Length > 2, "The dish name must be at least 3 characters")
                                , (res as Dish).Size, (res as Dish).Price
                              , SwtichCase(int.Parse(GetString("Enter the number of the level of kashrut of the dish:\n1) High\n2) Medium\n3) Low", item => (item == "1") || (item == "2") || (item == "3"), "Invalid input.")), Kashrut.HIGH, Kashrut.MEDIUM, Kashrut.LOW)
                              , (res as Dish).ID));
                                    #endregion
                                    break;
                                case "Branch":
                                    #region Update Branch
                                    myBL.UpdateBranch(new Branch(
                             GetString("Enter the name of the branch:", item => item.Length > 2, "branch name must be at least 3 characters")
                             , GetString("Enter the branch of the client:", item => item.Length > 2, "branch address must be at least 3 characters")
                             , GetString("Enter the phone number of the branch:", item => item.Length >= 7 && int.TryParse(item, out temp), "The min lenght of a phone number is 7 numbers, and it must contain only numbers")
                             , GetString("Enter the name of the boss of the branch:", item => item.Length > 2, "the boss of the branch name must be at least 3 characters")
                             , int.Parse(GetString("Enter the amount of employees of the branch:", item => int.TryParse(item, out temp) && temp > 0, "The amount of employees in the branch must be above zero, or youve entered Invalid input"))
                             , int.Parse(GetString("Enter the available messangers of the branch:", item => int.TryParse(item, out temp), "Invalid Input"))
                             , SwtichCase(int.Parse(GetString("Enter the number of the level of kashrut of the branch:\n1) High\n2) Medium\n3) Low", item => (item == "1") || (item == "2") || (item == "3"), "Invalid input.")), Kashrut.HIGH, Kashrut.MEDIUM, Kashrut.LOW)
                             , (res as Branch).ID));
                                    #endregion
                                    break;
                                case "Client":
                                    #region  Update Client
                                    myBL.UpdateClient(new Client(
                                        GetString("Enter the name of the client:", item => item.Length > 2, "Client name must be at least 3 characters")
                                        , GetString("Enter the address of the client:", item => item.Length > 2, "Client address must be at least 3 characters")
                                        , int.Parse(GetString("Enter the credit card of the client:", item => int.TryParse(item, out temp) && temp > 0, "The creditCard can contain only numbers and must be positive"))
                                        , int.Parse(GetString("Enter the age of the client:", item => int.TryParse(item, out temp) && temp >= 18, "Client age must be above 18 years old"))
                                        , (res as Client).ID));
                                    #endregion
                            break;
                                case "Order":
                                    if (GetString("Do you want to edit the order(1)? or the dishs in the order(2)?", item => item == "1" || item == "2", "Invalid Input.") == "1")
                                    {
                                        #region Update Order
                                        order = new Order(
                                            (res as Order).BranchID
                                            , null
                                            , DateTime.Now.AddHours(-int.Parse(GetString("Enter the number of hours that past since this order was created(or 0 if its just created now)", item => int.TryParse(item, out temp) && temp >= 0, "The number of hours needs to be a positive number")))
                                            , SwtichCase(int.Parse(GetString("Enter the number of the level of kashrut of the Order:\n1) High\n2) Medium\n3) Low", item => (item == "1") || (item == "2") || (item == "3"), "Invalid input.")), Kashrut.HIGH, Kashrut.MEDIUM, Kashrut.LOW) //התנאי של הכשרות פה ייבדק ב BL
                                            , (res as Order).ClientID);
                                        if (SwtichCase(int.Parse(GetString("Does the client is ordering the Dish home?(enter 1) or to someplace else(2)", item => item == "1" || item == "2", "Invalid Input.")), false, true))
                                            order.Address = GetString("Enter the address for the order:", item => item.Length > 2, "order address must be at least 3 characters");
                                        else
                                            order.Address = myBL.GetAllClients(item => item.ID == (res as Order).ClientID).First().Address;
                                        myBL.UpdateOrder(order);
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Update DishOrder
                                        list =myBL.GetAllDishOrders(item=>item.OrderID==(res as Order).ID).OrderBy(item=>item.ID).ToList();
                                        int length=myBL.GetAllDishOrders(item=>item.OrderID==(res as Order).ID).Count();
                                        Console.WriteLine("Which dish order do you want to edit?");
                                        myBL.PrintOrder(res as Order);
                                        temp =int.Parse(GetString("Enter her number: ", item => int.TryParse(item, out temp) && temp > length && temp > 0, "Invalid Input."));
                                        num = int.Parse(GetString("How much dishes do you want from this dish?", item => int.TryParse(item, out num) && num > 0, "Invalid Input.(must be positive)"));
                                        myBL.UpdateDishOrder(new DishOrder((res as Order).ID, list[temp - 1].DishID, num, (res as Order).ID));
                                        #endregion
                                    }
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        case 14:
                            #region Report on delivered Order
                            order = ManageBing<Order>("Search for the order you want to Report for a successful delivery");
                            myBL.DeliveredOrder(order);
                            #endregion
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
                            #region Print All
                            myBL.PrintAll();
#endregion
                            break;
                        case 21:
                            #region Search Bing!
                            var s=Bing();
                            #endregion
                            break;
                        case 22:
                            #region Best Customer
                            Console.WriteLine(myBL.BestCustomer());
                            #endregion
                            break;
                        case 23:
                            #region Most Ordered Dish
                            Console.WriteLine(myBL.MostOrderedDish());
                            #endregion
                            break;
                        case 24:
                            #region Most Ordered Dish In Branch
                            Console.WriteLine(myBL.BestDishInBranch(ManageBing<Branch>("Search for The Branch that you are going to order from(just search it using Bing!)")));
                            #endregion
                            break;
                        default:
                            #region Code
                            Console.WriteLine("\n\nGood By! Hope to see you again at our restaurants");
                            exit = true;
                            #endregion
                            break;
                    }
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
        }
        string GetString(string str, Func<string, bool> pred, string errorMsg=null)
        {
            string res;
            while (true)
            {
                Console.WriteLine("\n"+str);
                res=Console.ReadLine();
                if (pred(res))
                    return res;
                Console.WriteLine("The input youve entered is Invalid: "+errorMsg+" - please check your input and try again");
            }
        }
        T SwtichCase<T>(int choice,params T[] arr)
        {
          if(choice<arr.Length&&choice>=0)
            return arr[choice-1];
          throw new Exception("Invalid choice.");
        }
        T ManageBing<T>(string str)where T:class,InterID
        {
            InterID res;
            while (true)
            {
                res = Bing(str);
                if (res.GetType().Name == typeof(T).Name)
                    return res as T;
                else if (res == null)
                    return null;
                else
                    Console.WriteLine("You were asked to choose a " + typeof(T).Name + " but you have chosen a " + res.GetType().Name + ", please try again(if you want to exit the search choose the Exit option on Bing!");
            }
        }
        InterID Bing(string str = null)
        {
            string input;
            while (true)
            {
                try
                {
                    Console.WriteLine("\n" + str);
                    Console.WriteLine("Bing!(enter one keyword(number/string) and we will Bing! it(or press enter for the full list): ");
                    input = Console.ReadLine();
                    return ChooseSearch<InterID>(myBL.Search(input), input);
                }
                catch (Exception){}
            }

        }
        T ChooseSearch<T>(List<IEnumerable<T>> list, string str) where T : class
        {
            if (list.Sum(item => item.Count()) == 0)
            {
                Console.WriteLine("Your search - " + str + " - did not match any documents."
                    + "\n\tSuggestions:\n\t*Make sure that all words are spelled correctly.\n\t*Try different keywords. \n\t*Try fewer keywords. \n Press any key to continue...(or 0 to get out)");
                if (Console.ReadKey().Key == ConsoleKey.D0)
                    return null;
                throw new Exception("Didnt find anything!");
            }
            int i = 1, choice;
            Console.WriteLine("Choose from the following options: ");
            foreach (IEnumerable<T> item in list)
            {
                foreach (T item2 in item)
                {
                    Console.WriteLine(i + ": " + item2);
                    i++;
                }
            }
            Console.WriteLine(i+": Exit From Bing!");
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice >= i+1)
                Console.WriteLine("Invalid Choice. Choose again.");
            i = 0;
            foreach (IEnumerable<T> item in list)
            {
                foreach (T item2 in item)
                {
                    i++;
                    if (i == choice)
                        return item2;
                }
            }
            return null;
        }
        int Menu()
        {
            int num;
            string input;
            while (true)
            {
                #region Output
                Console.WriteLine("Menu: Project 1 - Ezra Block - Shy Tennenbaum\n");
                Console.WriteLine("Press the number of the operation you wish to do:");
                Console.WriteLine("1. Create new dish in the menu of the restaurant");
                Console.WriteLine("2. Create new Client");
                Console.WriteLine("3. Create new Branch");
                Console.WriteLine("4. Create new Order");
                Console.WriteLine("5. Add dishs to an order");
                Console.WriteLine("6. Delete an item from the database of the restaurant");
                Console.WriteLine("7. Delete a dish from an order");
                Console.WriteLine("8. Get profits detlies");
                Console.WriteLine("9. Calculate Order Total Price");
                Console.WriteLine("10. Mark Order as delivered");
                Console.WriteLine("11. Print All undelivered Orders");
                Console.WriteLine("12. Print an Order");
                Console.WriteLine("13. Update one of the items in the datdbase");
                Console.WriteLine("14. Report on a finished delivery");
                Console.WriteLine("12. Update Client's details");
                Console.WriteLine("13. Update Branch's details");
                Console.WriteLine("14. Update Order's details");
                Console.WriteLine("15. Update the amount of dishs order from one kind from an order");
                Console.WriteLine("20. Print the entire DataBase");
                Console.WriteLine("21. Use Bing!(Search Engine)");
                Console.WriteLine("22. Print the best customer");
                Console.WriteLine("23. Print the most ordered dish");
                Console.WriteLine("24. Print best dish from a specific branch");
                Console.WriteLine("25. Exit\n");
                #endregion
                input = Console.ReadLine();
                if ((!int.TryParse(input, out num)) || num < 1 || num > 25)
                {
                    Console.WriteLine("the number you've entered is Invalid! please try again\n\n");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                    return num;
            }
        }

    }
}