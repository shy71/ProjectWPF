using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using BE;

namespace BL
{
    //public delegate bool SortOutOrdersFunc(Order order);
    public static class FactoryBL
    {
        public static IBL getBL()
        {
            return new BL();
        }
    }
    public interface IBL
    {
        #region Dish Functions
        /// <summary>
        /// Adds a dish
        /// </summary>
        /// <param name="newDish"></param>
        void AddDish(Dish newDish);
        /// <summary>
        /// Deletes a dish by id
        /// </summary>
        /// <param name="id"></param>
        void DeleteDish(int id);
        /// <summary>
        /// Deletes a dish by item
        /// </summary>
        /// <param name="item"></param>
        void DeleteDish(Dish item);
        /// <summary>
        /// Upsates a specific dish
        /// </summary>
        /// <param name="item"></param>
        void UpdateDish(Dish item);
        /// <summary>
        /// Returns all of the dishes. 
        /// </summary>
        /// <param name="predicate">You can limit it so it only returns the dishes that pass the predicate test</param>
        /// <returns></returns>
        IEnumerable<Dish> GetAllDishs(Func<Dish, bool> predicate = null);
        /// <summary>
        /// Searches for a dish by a string. 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        IEnumerable<Dish> SearchDishs(object str);
        #endregion

        #region Branch Functions
        /// <summary>
        /// Adds a branch
        /// </summary>
        /// <param name="newBranch"></param>
        void AddBranch(Branch newBranch);
        void DeleteBranch(int id);
        void DeleteBranch(Branch item);
        void UpdateBranch(Branch item);
        IEnumerable<Branch> GetAllBranchs(Func<Branch, bool> predicate = null);
        IEnumerable<Branch> SearchBranchs(object str);

        #endregion

        #region Order Functions
        void AddOrder(Order newOrder);
        void DeliveredOrder(Order item);
        void DeleteOrder(int id);
        void DeleteOrder(Order item);
        void UpdateOrder(Order item);
        IEnumerable<Order> GetAllOrders(Func<Order, bool> predicate = null);
        IEnumerable<Order> SearchOrders(object str);

        #endregion

        #region DishOrder Functions
        void AddDishOrder(DishOrder newDishOrder);
        void DeleteDishOrder(int id);
        void DeleteDishOrder(DishOrder item);
        void UpdateDishOrder(DishOrder item);
        IEnumerable<DishOrder> GetAllDishOrders(Func<DishOrder, bool> predicate = null);
        #endregion

        #region Client Functions
        void AddClient(Client newClient);
        void DeleteClient(int id);
        void DeleteClient(Client item);
        /// <summary>
        /// update a client datd, can update anything but you cant update an address while it has an ongoing order for the old address
        /// </summary>
        /// <param name="item">The new upadted client</param>
        void UpdateClient(Client item);
        IEnumerable<Client> GetAllClients(Func<Client, bool> predicate = null);
        IEnumerable<Client> SearchClients(object str);
        #endregion

        /// <summary>
        /// Returns Price of the order
        /// </summary>
        /// <param name="order">Order being priced</param>
        /// <returns>Price</returns>
        float PriceOfOrder(Order order);
        /// <summary>
        /// Checks if the price exceeds maximum price for an order
        /// </summary>
        /// <param name="order">Order being checked</param>
        /// <returns>False if it exceeds the maximum price,
        ///          True if it doesn't exceed the maximum price</returns>
        /// <summary>
        /// Sorts out spicific orders from the list that is true in the condition
        /// from the condition fuction
        /// </summary>
        /// <param name="condition">Condition function</param>
        /// <returns>List of all the orders that the condition returns True</returns>
        void PrintAll();
        /// <summary>
        /// To create some raw data to do some checks
        /// </summary>
        void Inti();
        //Add grouping functions 

        List<IEnumerable<InterID>> Search(object obj);
        /// <summary>
        /// Get Profits grouped by the Dish id
        /// </summary>
        /// <returns>The Profits grouped by the Dish ID(Key for the grouping is the Dish ID)</returns>
        IEnumerable<IGrouping<int, float>> GetProfitByDishs();
        /// <summary>
        /// Get Profits grouped by the Address order
        /// </summary>
        /// <returns>The Profits grouped by the Address order(Key for the grouping is the Address)</returns>
        IEnumerable<IGrouping<string, float>> GetProfitByAddress();
        /// <summary>
        /// Get Profits grouped by the date of the order
        /// </summary>
        /// <returns>The Profits grouped by the date of the order(Key for the grouping is the date(dd/mm/yy) </returns>
        IEnumerable<IGrouping<string, float>> GetProfitByDates();
        /// <summary>
        /// Print an order, and its dishs 
        /// </summary>
        /// <param name="order">The order you want to print</param>
        void PrintOrder(Order order);
        #region Statistics Functions

        /// <summary>
        /// client statistics
        /// </summary>
        /// <returns>returns client with most orders</returns>
        Client BestCustomer();
        /// <summary>
        /// Branch statistics
        /// </summary>
        /// <returns>the branch that made the most profit</returns>
        Branch BestBranch();
        /// <summary>
        /// dish statistics
        /// </summary>
        /// <returns>returns the most ordered dish</returns>
        Dish MostOrderedDish();
        /// <summary>
        /// best dish in specific branch
        /// </summary>
        /// <param name="myBranch">the branch being checked</param>
        /// <returns>the dish most frequintly used in the specific branch</returns>
        Dish BestDishInBranch(Branch myBranch);
        #endregion
    }
    public class BL : IBL
    {
        public readonly int MAX_PRICE;
        DAL.Idal myDal = DAL.FactoryDal.getDal();
        /// <summary>
        /// Constructor which starts up the maximu price
        /// </summary>
        /// <param name="maxPrice"></param>
        public BL(int maxPrice = 1000)
        {
            MAX_PRICE = maxPrice;
        }
        /// <summary>
        /// Checks the price of a specific order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public float PriceOfOrder(Order order)//need checking
        {
            float result = 0;
            List<DishOrder> list = myDal.GetAllDishOrders(item => item.OrderID == order.ID).ToList<DishOrder>();
            foreach (DishOrder item in list)
                result += item.DishAmount * myDal.GetDish(item.DishID).Price;
            return result;
        }
        public List<IEnumerable<InterID>> Search(object obj)
        {
            List<IEnumerable<InterID>> list = new List<IEnumerable<InterID>>();
            list.Add(SearchDishs(obj));
            list.Add(SearchClients(obj));
            list.Add(SearchBranchs(obj));
            list.Add(SearchOrders(obj));
            return list;
        }
        #region Profits Functions
        public IEnumerable<IGrouping<int, float>> GetProfitByDishs()
        {
            return from item in myDal.GetAllDishOrders()
                   group item.DishAmount * myDal.GetDish(item.DishID).Price by item.DishID;
        }
        public IEnumerable<IGrouping<string, float>> GetProfitByAddress()
        {
            return from item in myDal.GetAllDishOrders()
                   group item.DishAmount * myDal.GetDish(item.DishID).Price by myDal.GetOrder(item.OrderID).Address;
        }
        public IEnumerable<IGrouping<string, float>> GetProfitByDates()
        {
            return from item in myDal.GetAllDishOrders()
                   group item.DishAmount * myDal.GetDish(item.DishID).Price by myDal.GetOrder(item.OrderID).Date.ToShortDateString();
        }
        #endregion

        #region Statistics Functions
        public Client BestCustomer()
        {
            Client bestClient = null;
            int maxTimes = 0;
            foreach (Client curClient in myDal.GetAllClients())
            {
                int count = 0;
                foreach (Order item in myDal.GetAllOrders())
                    if (item.ClientID == curClient.ID)
                        count++;
                if (count > maxTimes)
                {
                    maxTimes = count;
                    bestClient = curClient;
                }
            }
            if (bestClient == null)
                throw new Exception("No Clients to choose the best client from");
            return bestClient;
        }
        public Branch BestBranch()
        {
            Branch bestBranch = null;
            int maxTimes = 0;
            foreach (Branch curBranch in myDal.GetAllBranchs())
            {
                int count = 0;
                foreach (Order item in myDal.GetAllOrders())
                    if (item.BranchID == curBranch.ID)
                        count++;
                if (count > maxTimes)
                {
                    maxTimes = count;
                    bestBranch = curBranch;
                }
            }
            if (bestBranch == null)
                throw new Exception("No Clients to choose the best client from");
            return bestBranch;
        }
        public Dish MostOrderedDish()
        {
            Dish maxDish = null;
            int maxTimes = 0;
            foreach (Dish curDish in myDal.GetAllDishs())
            {
                int count = 0;
                foreach (DishOrder item in myDal.GetAllDishOrders())
                    if (item.DishID == curDish.ID)
                        count += item.DishAmount;
                if (count > maxTimes)
                {
                    maxTimes = count;
                    maxDish = curDish;
                }
            }
            if (maxDish == null)
                throw new Exception("No dishes to choose the most ordered dish from");
            return maxDish;
        }
        public Dish BestDishInBranch(Branch myBranch)
        {
            Branch inList = myDal.GetBranch(myBranch.ID);
            Dish bestDish = null;
            int maxAmount = 0;
            if (inList == null)
                throw new Exception("There is no branch like that");
            foreach (Dish curDish in myDal.GetAllDishs())
            {
                int count = 0;
                foreach (DishOrder item in myDal.GetAllDishOrders())
                {
                    if (myDal.GetOrder(item.OrderID).BranchID == myBranch.ID && myDal.GetDish(item.DishID) == curDish)
                    {
                        count += item.DishAmount;
                    }
                }
                if (count > maxAmount)
                {
                    bestDish = curDish;
                    maxAmount = count;
                }
            }
            if (bestDish == null)
                throw new Exception("There are no dishes to choose from");
            return bestDish;
        }

        #endregion

        /// <summary>
        /// Checks if the object (usualy a list of T's) includes the item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item being searched for</param>
        /// <param name="obj">The list</param>
        /// <returns></returns>
        bool Include<T>(T item, object obj)
        {
            foreach (PropertyInfo p in item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (Compares(p.GetValue(item), (p.PropertyType.Name == "String"), (p.PropertyType.Name == "Int32"),(p.PropertyType.Name=="Single"),(p.PropertyType.Name=="DateTime"), obj))
                {
                    return true;
                }
            }
            return false;
        }
        bool Compares(object obj,bool IsString,bool IsInt,bool IsFloat,bool IsDate,object subObj)
        {
            int temp;
            float temp2;
            if (IsString && subObj.GetType().Name == "String")
                return (obj as string).ToLower().Contains((subObj as string).ToLower());
            else if (IsInt && int.TryParse(subObj as string, out temp))
                return (int)obj == temp;
            else if (IsDate && subObj.GetType().Name == "String")
                return ((DateTime)obj).ToShortDateString() == (obj as string);
            else if (IsFloat && float.TryParse(subObj as string, out temp2))
                return ((float)obj) == (temp2);
            return false;

        }

        public IEnumerable<T> Search<T>(object obj, IEnumerable<T> list)
        {
            if (obj == null)
                return list;
            return from item in list
                    where Include(item, obj)
                    select item;
        }

        public void PrintAll()//need checking // פונקציה זמנית
        {
            foreach (Dish item in myDal.GetAllDishs())
               Console.WriteLine(item);
           foreach (Branch item in myDal.GetAllBranchs())
               Console.WriteLine(item);
           foreach (Client item in myDal.GetAllClients())
               Console.WriteLine(item);
           foreach (Order item in myDal.GetAllOrders())
               Console.WriteLine(item);
           foreach (DishOrder item in myDal.GetAllDishOrders())
               Console.WriteLine(item);
        }
        public void Inti()//need checking
        {

            AddDish(new Dish("Soup shy", Size.LARGE, 13, Kashrut.HIGH, 957473));
            AddDish(new Dish("Hot Dogs", Size.MEDIUM, 15, Kashrut.LOW, 19273));
            AddDish(new Dish("Bamba", Size.SMALL, 5, Kashrut.HIGH, 1243));
            AddDish(new Dish("Wings", Size.MEDIUM, 20, Kashrut.MEDIUM, 95840));
            AddDish(new Dish("Stake", Size.LARGE, 34, Kashrut.LOW, 21));
            AddClient(new Client("Shy", "Sderot Hertzel 12", 45326, 23, 1921));
            AddClient(new Client("Ezra", "Beit Shemesh", 78695, 65, 10934));
            AddClient(new Client("Itai", "Giv'at Ze'ev", 1938, 18, 493));
            AddClient(new Client("Tal", "Alon Shvut", 91731, 20, 1313));
            AddClient(new Client("Gal", "Ma'ale Adumim shy", 38267, 19, 20744));
            AddBranch(new Branch("Jerusalem", "malcha 1", "026587463", "morli shy", 3, 4, Kashrut.MEDIUM, 87465));
            AddBranch(new Branch("Bnei Brak", "sholm 7", "039872611", "kidron", 1, 5, Kashrut.HIGH, 18932));
            AddBranch(new Branch("Eilat", "freedom 98", "078496352", "oshri", 5, 3, Kashrut.LOW, 2));
            AddBranch(new Branch("Tel Aviv", "zion 6", "032648544", "amram", 10, 10, Kashrut.LOW, 0));
            AddBranch(new Branch("Beit Shemesh", "Big Center 1", "073524121", "joffrey", 2, 3, Kashrut.MEDIUM, 9873));
            AddOrder(new Order(2, "Beit Shemesh shy", DateTime.Now, Kashrut.LOW, 10934, 192334));
            AddDishOrder(new DishOrder(192334, 957473, 3));
            AddDishOrder(new DishOrder(192334, 957473, 2));
            AddDishOrder(new DishOrder(192334, 19273, 2));
        }
        public void PrintOrder(Order order)
        {
            int temp=0;
            Console.WriteLine(order);
            foreach (DishOrder item in myDal.GetAllDishOrders(item => item.OrderID == order.ID).OrderBy(item=>item.ID))//מסודר כדי שנוכל להשתמש בזה לבחירת קלט
                Console.WriteLine("\t("+(temp++)+") Name: "+myDal.GetDish(item.DishID).Name+" Amount: "+item.DishAmount);
        }
        //לחשוב אולי אפשר יהיה לעדכן שדות מוסימים גם בזמן שיש הזמנות לדבר
        #region Dish Functions
        internal void CompatibleDish(Dish dish, string str = null)//need checking
        {
            if (dish.Price <= 0)
                throw new Exception(str + " The price of a dish have to be highr then zero!");
            else if (dish.Name == null)
                throw new Exception(str + " A dish has to have a name!");
            else if (dish.ID > 99999999)
                throw new Exception(str + "The ID must be a positive number with at the most 8 digits");
        }
        public void AddDish(Dish newDish)//need checking
        {
                CompatibleDish(newDish, "The Dish you are trying to add is incompatible:");
                myDal.AddDish(newDish);
        }
        public void DeleteDish(int id)//need checking
        {
            if (!myDal.GetAllDishOrders(item => item.DishID == id).Any(item => myDal.GetOrder(item.OrderID).Delivered==false))
                myDal.DeleteDish(id);
            else
                throw new Exception("You can't delete a dish which is being ordered");
        }
        public void DeleteDish(Dish item)//need checking
        {
            DeleteDish(item.ID);
        }
        public void UpdateDish(Dish item)//Done
        {
            Dish temp = myDal.GetDish(item.ID);
            if (!myDal.GetAllDishOrders(var => var.DishID == item.ID).Any(var => (myDal.GetOrder(var.OrderID).Kosher>item.Kosher|| temp.Price!=item.Price||temp.Size!=item.Size) &&myDal.GetOrder(var.OrderID).Delivered == false))
            {
                CompatibleDish(item, "The Updated Dish you sended to upadte the old one is incompatible:");
                myDal.UpdateDish(item);
            }
            else
                throw new Exception("You can't update a dish(Price,Size or lower her kasrut) when the dish is already inside an active order");
        }
        public IEnumerable<Dish> GetAllDishs(Func<Dish, bool> predicate = null)
        {
            return myDal.GetAllDishs(predicate);
        }
        public IEnumerable<Dish> SearchDishs(object str)
        {
            return Search(str, myDal.GetAllDishs());
        }
        #endregion

        #region Branch Functions
        internal void CompatibleBranch(Branch branch, string str = null)//need checking
        {
            if (branch.Address == null)
                throw new Exception(str + " The Address filed cant be empty!");
            else if (branch.Boss == null)
                throw new Exception(str + " every branch must have a boss!");
            else if (branch.EmployeeCount <= 0)
                throw new Exception(str + " A branch must have at least one worker!");
            else if (branch.PhoneNumber == null)
                throw new Exception(str + " every branch must have a phone number!");
            else if (branch.Name == null)
                throw new Exception(str + " The Name filed cant be empty!");
        }
        public void AddBranch(Branch newBranch)//need checking
        {
            CompatibleBranch(newBranch, "The Branch you are trying to add is incompatible:");
            myDal.AddBranch(newBranch);
        }
        public void DeleteBranch(int id)//need checking
        {
            if (!myDal.GetAllOrders(item => item.BranchID == id).Any(item => item.Delivered==false))
                myDal.DeleteBranch(id);
            else
                throw new Exception("you cant delete a branch that has active orders from!");
        }
        public void DeleteBranch(Branch myBranch)//need checking
        {
            DeleteBranch(myBranch.ID);
        }
        public void UpdateBranch(Branch myBranch)//Done
        {
            if (!myDal.GetAllOrders(item => item.BranchID == myBranch.ID).Any(item => item.Kosher > myBranch.Kosher&&item.Delivered == false))
            {
                CompatibleBranch(myBranch, "The updated branch you sended to upadte the old one is incompatible. Anything that could be updated was updated.");
                myDal.UpdateBranch(myBranch);
            }
            else
                throw new Exception("you can't lower a bracnh kashrut when he has an active orders from a higher kashrutstandardstandard!");
        }
        public IEnumerable<Branch> GetAllBranchs(Func<Branch, bool> predicate = null)
        {
            return myDal.GetAllBranchs(predicate);
        }
        public IEnumerable<Branch> SearchBranchs(object obj)
        {
            return Search(obj, myDal.GetAllBranchs());
        }
        #endregion

        #region Order Functions
        
        internal void CompatibleOrder(Order myOrder, string str)//need checking
        {
            if (myOrder.Address == null)
                throw new Exception(str + " The Address filed cant be empty!");
            else if (!myDal.ContainID<Client>(myOrder.ClientID))
                throw new Exception(str + " the client in the order does not exists!");
            else if (!myDal.ContainID<Branch>(myOrder.BranchID))
                throw new Exception(str + " the branch in the order does not exists!");
            else if (myOrder.Date == null)
                throw new Exception(str + " The Date field can't be empty!");
            //else if (myOrder.Kosher == null)
            //    throw new Exception(str+" The kashrut filed cant be empty");
            else if (myOrder.Kosher > myDal.GetBranch(myOrder.BranchID).Kosher)//need checking
                throw new Exception(str + " The Kashrut in the branch is not sufficient for the order");
            else if (myDal.GetBranch(myOrder.BranchID).AvailableMessangers == 0)
                throw new Exception(str + " There isnt any available messangers to deliver the order");
        }
        public void DeliveredOrder(Order item)
        {
            myDal.GetBranch(item.BranchID).AvailableMessangers++;
            myDal.DeliveredOrder(item.ID);
        }
        public void AddOrder(Order newOrder)//need checking
        {

            CompatibleOrder(newOrder, "The Order you are trying to add is incompatible:");

                myDal.AddOrder(newOrder);
                myDal.GetBranch(newOrder.BranchID).AvailableMessangers--;
        }
        public void DeleteOrder(int id)//need checking
        {
            foreach (DishOrder item in myDal.GetAllDishOrders(item => item.OrderID == id).ToList())
                DeleteDishOrder(item);
            myDal.DeleteOrder(id);
            myDal.GetBranch((myDal.GetOrder(id).BranchID)).AvailableMessangers++;
        }
        public void DeleteOrder(Order myOrder)//need checking
        {
            DeleteOrder(myOrder.ID);
        }
        public void UpdateOrder(Order newOrder)//fixed! need checking
        {
            //make sure that kashrut doesn't contradict kashrut of branch or dishes
            Order oldOrder = myDal.GetOrder(newOrder.ID);
            if (oldOrder.BranchID != newOrder.BranchID || oldOrder.ClientID != newOrder.ClientID || oldOrder.Delivered != newOrder.Delivered || myDal.GetAllDishOrders(item => item.OrderID == newOrder.ID).Any(item => myDal.GetDish(item.DishID).Kosher < newOrder.Kosher))
                    throw new Exception("You can't update the order's kashrut level because it has dishes which aren't in the new sufficient kashrout level, also you cant update The client ID , branch ID, or if it was delivered or not(use the proper function to do it)");
            CompatibleOrder(newOrder, "The Updated order you sended to upadte the old one is incompatible:");
            myDal.UpdateOrder(newOrder);
        }
        public IEnumerable<Order> GetAllOrders(Func<Order, bool> predicate = null)
        {
            return myDal.GetAllOrders(predicate);
        }
        public IEnumerable<Order> SearchOrders(object obj)
        {
            return Search(obj, myDal.GetAllOrders());
        }
        #endregion

        #region DishOrder Functions
        internal void CompatibleDishOrder(DishOrder theDishOrder, string str = null)//need checking
        {
            if (theDishOrder.DishAmount <= 0)
                throw new Exception(str + " you cant order less the one from a Dish");
            else if (!myDal.ContainID<Dish>(theDishOrder.DishID))
                throw new Exception(str + " the Dish you are trying to order does not exists!");
            else if (!myDal.ContainID<Order>(theDishOrder.OrderID))
                throw new Exception(str + " The order you are trying to add dishs to does not exists!");
            else if ((PriceOfOrder(myDal.GetOrder(theDishOrder.OrderID)) + theDishOrder.DishAmount * myDal.GetDish(theDishOrder.DishID).Price) > MAX_PRICE)//בודק שהמחיר הצפוי לא גבוה מהמקסימום המותר
                throw new Exception(str + " with those dishes the order price will be above the approved limit!");
            else if (myDal.GetDish(theDishOrder.DishID).Kosher < myDal.GetOrder(theDishOrder.OrderID).Kosher)
                throw new Exception(str + " you cant add a dish without the sufficient Kashrut for the order");
        }
        public void AddDishOrder(DishOrder newDishOrder)//need checking
        {
            CompatibleDishOrder(newDishOrder, "The Dish you are trying to add to the order is incompatible:");
            myDal.AddDishOrder(newDishOrder);
        }
        public void DeleteDishOrder(int id)//need checking
        {
            myDal.DeleteDishOrder(id);
        }
        public void DeleteDishOrder(DishOrder item) //need checking
        {
            DeleteDishOrder(item.ID);
        }
        public void UpdateDishOrder(DishOrder item)//Done
        {
            DishOrder temp =myDal.GetDishOrder(item.ID);
            if (item.DishAmount > temp.DishAmount)
            {
                if (PriceOfOrder(myDal.GetOrder(item.OrderID)) + (item.DishAmount - temp.DishAmount) * myDal.GetDish(item.ID).Price > MAX_PRICE)
                    throw new Exception("you cant upadte the order because with the extra dishs your ordered your order price will be above the approved limit!");
            }
            else if (item.DishID != temp.DishID || item.OrderID != temp.OrderID)
                throw new Exception("You cant update those components!");
                CompatibleDishOrder(item, "The Updated Client you sended to upadte the old one is incompatible:");//bug - כאשר ההמחיר קרוב למקסימום וזה מחשב גם את ערך המנה הזו וגם את הערך של הזו הישנה שאנו מעדכנים
            myDal.UpdateDishOrder(item);
        }
        public IEnumerable<DishOrder> GetAllDishOrders(Func<DishOrder, bool> predicate = null)
        {
            return myDal.GetAllDishOrders(predicate);
        }
        #endregion

        #region Client Functions
        //need checking
        internal void CompatibleClient(Client client, string str = null)//האם יש הגבלות יותר מוסימות על כרטיס אשראי?
        {
            if (client.Address == null)
                throw new Exception(str + " The Address filed cant be empty!");
            else if (client.CreditCard <= 0)
                throw new Exception(str + " the client credit card is invalid");
            else if (client.Name == null)
                throw new Exception(str + " The Name filed cant be empty!");
            if (client.Age < 18)
                throw new Exception(str + " the services is offerd only to age 18+ client");
        }
        public void AddClient(Client newClient)//need checking
        {

            CompatibleClient(newClient, "The Client you are trying to add is incompatible:");
            myDal.AddClient(newClient);
        }
        public void DeleteClient(int id)//need checking
        {
            if (myDal.GetAllOrders(item => item.ClientID == id).Any(item => item.Delivered==false))
                throw new Exception("You cant delete a Client that has active orders");
            myDal.DeleteClient(id);
        }
        public void DeleteClient(Client item)//need checking
        {
            DeleteClient(item.ID);
        }
        public void UpdateClient(Client item)//Done!
        {
            Client temp = myDal.GetClient(item.ID);
            if (temp.Address != item.Address)
            {
                if (myDal.GetAllOrders(var => var.ClientID == var.ID && var.Address == temp.Address && var.Delivered == false).Count() > 0)
                    throw new Exception("You cant upadte a client address when he has an order to that address!");
            }
            CompatibleClient(item, "The Updated Client you sended to upadte the old one is incompatible:");
            myDal.UpdateClient(item);
        }
        public IEnumerable<Client> GetAllClients(Func<Client, bool> predicate = null)
        {
            return myDal.GetAllClients(predicate);
        }
        public IEnumerable<Client> SearchClients(object obj)
        {
            return Search(obj, myDal.GetAllClients());
        }
        #endregion
    }
}