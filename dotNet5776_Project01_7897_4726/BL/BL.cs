using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using BE;

namespace BL
{
    public static class FactoryBL
    {
        private static IBL instance;
        /// <summary>
        /// Function to start get the BL
        /// </summary>
        /// <returns></returns>
        public static IBL getBL()
        {
            if (instance == null)
                instance = new BL();
            return instance;
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
        IEnumerable<Dish> SearchDishs(string str);
        #endregion

        #region Branch Functions
        /// <summary>
        /// Adds a branch
        /// </summary>
        /// <param name="newBranch"></param>
        void AddBranch(Branch newBranch);
        /// <summary>
        /// Adds a branch by ID
        /// </summary>
        /// <param name="id"></param>
        void DeleteBranch(int id);
        /// <summary>
        /// Adds a branch
        /// </summary>
        /// <param name="item"></param>
        void DeleteBranch(Branch item);
        /// <summary>
        /// Updates a branch
        /// </summary>
        /// <param name="item"></param>
        void UpdateBranch(Branch item);
        /// <summary>
        /// Gets all Branches that pass the predicate test (if there is one)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<Branch> GetAllBranchs(Func<Branch, bool> predicate = null);
        /// <summary>
        /// searches for branchs by a string included in it
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        IEnumerable<Branch> SearchBranchs(string str);

        #endregion

        #region Order Functions
        /// <summary>
        /// adds an order
        /// </summary>
        /// <param name="newOrder"></param>
        void AddOrder(Order newOrder);
        /// <summary>
        /// delivers an order
        /// </summary>
        /// <param name="item"></param>
        void DeliveredOrder(Order item);
        /// <summary>
        /// deletes an order by ID
        /// </summary>
        /// <param name="id"></param>
        void DeleteOrder(int id);
        /// <summary>
        /// deletes an order
        /// </summary>
        /// <param name="item"></param>
        void DeleteOrder(Order item);
        /// <summary>
        /// updates an order
        /// </summary>
        /// <param name="item"></param>
        void UpdateOrder(Order item);
        /// <summary>
        /// Gets all orders that pass the predicate test (if there is one)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<Order> GetAllOrders(Func<Order, bool> predicate = null);
        /// <summary>
        /// searches for orders by a string included in it
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        IEnumerable<Order> SearchOrders(string str);

        #endregion

        #region DishOrder Functions
        /// <summary>
        /// adds a dish-order
        /// </summary>
        /// <param name="newDishOrder"></param>
        void AddDishOrder(DishOrder newDishOrder);
        /// <summary>
        /// deletes a dish-order by ID
        /// </summary>
        /// <param name="id"></param>
        void DeleteDishOrder(int id);
        /// <summary>
        /// deletes a dish-order
        /// </summary>
        /// <param name="item"></param>
        void DeleteDishOrder(DishOrder item);
        /// <summary>
        /// updates a dish-order
        /// </summary>
        /// <param name="item"></param>
        void UpdateDishOrder(DishOrder item);
        /// <summary>
        /// gets all the dish-orders that pass the predicate test (if there is one)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<DishOrder> GetAllDishOrders(Func<DishOrder, bool> predicate = null);
        #endregion

        #region Client Functions
        /// <summary>
        /// adds a client
        /// </summary>
        /// <param name="newClient"></param>
        void AddClient(Client newClient);
        /// <summary>
        /// deletes a client by ID
        /// </summary>
        /// <param name="id"></param>
        void DeleteClient(int id);
        /// <summary>
        /// deletes a client
        /// </summary>
        /// <param name="item"></param>
        void DeleteClient(Client item);
        /// <summary>
        /// update a client datd, can update anything but you cant update an address while it has an ongoing order for the old address
        /// </summary>
        /// <param name="item">The new upadted client</param>
        void UpdateClient(Client item);
        /// <summary>
        /// gets all clients that pass the predicate test (if there is one)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<Client> GetAllClients(Func<Client, bool> predicate = null);
        /// <summary>
        /// Suggests a dish by similarity to other similar clients
        /// </summary>
        /// <param name="ID">id of the client</param>
        /// <returns></returns>
        Dish SuggestedDish(int ID);
        /// <summary>
        /// searches for clients by a string included in it
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        IEnumerable<Client> SearchClients(string str);
        #endregion



        #region ToString Functions
        /// <summary>
        /// return the string of an order, and its dishs. ready to be print
        /// </summary>
        /// <param name="order">The order you want to print</param>
        /// <returns>The string to print</returns>
        string ToStringOrder(Order order);

        #endregion

        #region Profits Functions

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

        #endregion

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



        /// <summary>
        /// searchs for something by something
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        List<IEnumerable<InterID>> Search(string str);
        /// <summary>
        /// Returns Price of the order
        /// </summary>
        /// <param name="order">Order being priced</param>
        /// <returns>Price</returns>
        float PriceOfOrder(Order order);


        /// <summary>
        /// To create some raw data to do some checks
        /// </summary>
        void Inti();



        //New

        User getUser(string UserName);
        void AddUser(User user);
    }

    public class BL : IBL
    {
        //New
        internal void CompatibleUser(User myUser, string str)
        {
            if (myUser.Password == "")
                throw new Exception(str + "The password cant be empty!");
            else if(myUser.Name=="")
                throw new Exception(str + "The name cant be empty!");
            else if(myUser.UserName=="")
                throw new Exception(str + "The username cant be empty!");
            else if(myUser.Type==UserType.Client&&!myDal.ContainID<Client>(myUser.ClientID))
                throw new Exception(str + "There isnt a client that connected to this user!");

        }
       public void AddUser(User user)
        {
            myDal.AddUser(user);
        }
        public User getUser(string UserName)
        {
           return myDal.GetUser(UserName);
        }


        /// <summary>
        /// Max Price for an order
        /// </summary>
        public readonly int MAX_PRICE;

        DAL.Idal myDal;

        /// <summary>
        /// Constructor which starts up the max price and initialize the DAL layer
        /// </summary>
        /// <param name="maxPrice"></param>
        public BL(int maxPrice = 1000)
        {
            MAX_PRICE = maxPrice;
            myDal = DAL.FactoryDal.getDal();
        }


        #region Dish Functions
        /// <summary>
        /// checks if a dish is compatible for adding
        /// </summary>
        /// <param name="dish"></param>
        /// <param name="str"></param>
        internal void CompatibleDish(Dish dish, string str = null)
        {
            if (dish.Price <= 0)
                throw new Exception(str + " The price of a dish have to be highr then zero!");
            else if (dish.Name == null)
                throw new Exception(str + " A dish has to have a name!");
            else if (dish.ID > 99999999)
                throw new Exception(str + "The ID must be a positive number with at the most 8 digits");
        }
        public void AddDish(Dish newDish)
        {
            CompatibleDish(newDish, "The Dish you are trying to add is incompatible:");
            myDal.AddDish(newDish);
        }
        public void DeleteDish(int id)
        {
            if (!myDal.GetAllDishOrders(item => item.DishID == id).Any(item => myDal.GetOrder(item.OrderID).Delivered == false))
                myDal.DeleteDish(id);
            else
                throw new Exception("You can't delete a dish which is being ordered");
        }
        public void DeleteDish(Dish item)
        {
            DeleteDish(item.ID);
        }
        public void UpdateDish(Dish item)
        {
            Dish temp = myDal.GetDish(item.ID);
            if (!myDal.GetAllDishOrders(var => var.DishID == item.ID).Any(var => (myDal.GetOrder(var.OrderID).Kosher > item.Kosher || temp.Price != item.Price || temp.Size != item.Size) && myDal.GetOrder(var.OrderID).Delivered == false))
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
        public IEnumerable<Dish> SearchDishs(string str)
        {
            return Search(str, myDal.GetAllDishs());
        }
        #endregion

        #region Branch Functions
        /// <summary>
        /// checks if branch is compatible for adding
        /// </summary>
        /// <param name="branch"></param>
        /// <param name="str"></param>
        internal void CompatibleBranch(Branch branch, string str = null)
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
        public void AddBranch(Branch newBranch)
        {
            CompatibleBranch(newBranch, "The Branch you are trying to add is incompatible:");
            myDal.AddBranch(newBranch);
        }
        public void DeleteBranch(int id)
        {
            if (!myDal.GetAllOrders(item => item.BranchID == id).Any(item => item.Delivered == false))
                myDal.DeleteBranch(id);
            else
                throw new Exception("you cant delete a branch that has active orders from!");
        }
        public void DeleteBranch(Branch myBranch)
        {
            DeleteBranch(myBranch.ID);
        }
        public void UpdateBranch(Branch myBranch)
        {
            if (!myDal.GetAllOrders(item => item.BranchID == myBranch.ID).Any(item => item.Kosher > myBranch.Kosher && item.Delivered == false))
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
        public IEnumerable<Branch> SearchBranchs(string str)
        {
            return Search(str, myDal.GetAllBranchs());
        }
        #endregion

        #region Order Functions

        internal void CompatibleOrder(Order myOrder, string str)
        {
            if (myOrder.Address == null)
                throw new Exception(str + " The Address filed cant be empty!");
            else if (!myDal.ContainID<Client>(myOrder.ClientID))
                throw new Exception(str + " the client in the order does not exists!");
            else if (!myDal.ContainID<Branch>(myOrder.BranchID))
                throw new Exception(str + " the branch in the order does not exists!");
            else if (myOrder.Date == null)
                throw new Exception(str + " The Date field can't be empty!");
            else if (myOrder.Kosher > myDal.GetBranch(myOrder.BranchID).Kosher)
                throw new Exception(str + " The Kashrut in the branch is not sufficient for the order");
            else if (myDal.GetBranch(myOrder.BranchID).AvailableMessangers == 0)
                throw new Exception(str + " There isnt any available messangers to deliver the order");
        }
        public void DeliveredOrder(Order item)
        {
            myDal.GetBranch(item.BranchID).AvailableMessangers++;
            myDal.DeliveredOrder(item.ID);
        }
        public void AddOrder(Order newOrder)
        {

            CompatibleOrder(newOrder, "The Order you are trying to add is incompatible:");

            myDal.AddOrder(newOrder);
            myDal.GetBranch(newOrder.BranchID).AvailableMessangers--;
        }
        public void DeleteOrder(int id)
        {
            foreach (DishOrder item in myDal.GetAllDishOrders(item => item.OrderID == id).ToList())
                DeleteDishOrder(item);
            myDal.DeleteOrder(id);
            myDal.GetBranch((myDal.GetOrder(id).BranchID)).AvailableMessangers++;
        }
        public void DeleteOrder(Order myOrder)
        {
            DeleteOrder(myOrder.ID);
        }
        public void UpdateOrder(Order newOrder)
        {
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
        public IEnumerable<Order> SearchOrders(string str)
        {
            return Search(str, myDal.GetAllOrders());
        }
        #endregion

        #region DishOrder Functions
        internal void CompatibleDishOrder(DishOrder theDishOrder, string str = null, bool IsNewDishOrder = true)
        {
            if (theDishOrder.DishAmount <= 0)
                throw new Exception(str + " you cant order less the one from a Dish");
            else if (!myDal.ContainID<Dish>(theDishOrder.DishID))
                throw new Exception(str + " the Dish you are trying to order does not exists!");
            else if (!myDal.ContainID<Order>(theDishOrder.OrderID))
                throw new Exception(str + " The order you are trying to add dishs to does not exists!");
            else if (IsNewDishOrder && (PriceOfOrder(myDal.GetOrder(theDishOrder.OrderID)) + theDishOrder.DishAmount * myDal.GetDish(theDishOrder.DishID).Price) > MAX_PRICE)//בודק שהמחיר הצפוי לא גבוה מהמקסימום המותר
                throw new Exception(str + " with those dishes the order price will be above the approved limit!");
            else if (myDal.GetDish(theDishOrder.DishID).Kosher < myDal.GetOrder(theDishOrder.OrderID).Kosher)
                throw new Exception(str + " you cant add a dish without the sufficient Kashrut for the order");
        }
        public void AddDishOrder(DishOrder newDishOrder)
        {
            CompatibleDishOrder(newDishOrder, "The Dish you are trying to add to the order is incompatible:");
            myDal.AddDishOrder(newDishOrder);
        }
        public void DeleteDishOrder(int id)
        {
            myDal.DeleteDishOrder(id);
        }
        public void DeleteDishOrder(DishOrder item)
        {
            DeleteDishOrder(item.ID);
        }
        public void UpdateDishOrder(DishOrder item)
        {
            DishOrder temp = myDal.GetDishOrder(item.ID);
            if (item.DishAmount > temp.DishAmount)
            {
                if (PriceOfOrder(myDal.GetOrder(item.OrderID)) + (item.DishAmount - temp.DishAmount) * myDal.GetDish(item.ID).Price > MAX_PRICE)
                    throw new Exception("you cant upadte the order because with the extra dishs your ordered your order price will be above the approved limit!");
            }
            else if (item.DishID != temp.DishID || item.OrderID != temp.OrderID)
                throw new Exception("You cant update those components!");
            CompatibleDishOrder(item, "The Updated Client you sended to upadte the old one is incompatible:", false);
            myDal.UpdateDishOrder(item);
        }
        public IEnumerable<DishOrder> GetAllDishOrders(Func<DishOrder, bool> predicate = null)
        {
            return myDal.GetAllDishOrders(predicate);
        }
        #endregion

        #region Client Functions

        internal void CompatibleClient(Client client, string str = null)
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
        public void AddClient(Client newClient)
        {

            CompatibleClient(newClient, "The Client you are trying to add is incompatible:");
            myDal.AddClient(newClient);
        }
        public void DeleteClient(int id)
        {
            if (myDal.GetAllOrders(item => item.ClientID == id).Any(item => item.Delivered == false))
                throw new Exception("You cant delete a Client that has active orders");
            myDal.DeleteClient(id);
        }
        public void DeleteClient(Client item)
        {
            DeleteClient(item.ID);
        }
        public void UpdateClient(Client item)
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
        public IEnumerable<Client> SearchClients(string str)
        {
            return Search(str, myDal.GetAllClients());
        }
        public Dish SuggestedDish(int ID)
        {
            Dish suggestion = null;
            Client theClient = GetAllClients(item => item.ID == ID).FirstOrDefault();
            List<Client> mostSimilarClients = new List<Client>();
            int maxSimilarities = 0;
            foreach (Client var in GetAllClients(item => item.ID != ID))
            {
                int similarityCount = 0;
                foreach (DishOrder item1 in GetAllDishOrders(item => myDal.GetOrder(item.OrderID).ClientID == var.ID))//all the DishOrders of this Client
                    foreach (DishOrder item2 in GetAllDishOrders(item => myDal.GetOrder(item.OrderID).ClientID == theClient.ID))
                    {
                        if (myDal.GetDish(item1.DishID).ID == myDal.GetDish(item2.DishID).ID)//checks for all DishOrders of this client that are similar to the input client
                            similarityCount += ((item1.DishAmount < item2.DishAmount) ? item1.DishAmount : item2.DishAmount);//adds the mimimum of the similarities (between the amounts of each one)
                    }
                if (similarityCount > maxSimilarities)
                {
                    maxSimilarities = similarityCount;
                    mostSimilarClients.Clear();
                    mostSimilarClients.Add(var);
                }
                else if (similarityCount == maxSimilarities && maxSimilarities!=0)
                {
                    mostSimilarClients.Add(var);
                }
            }
            if (mostSimilarClients != null && mostSimilarClients.Count >0)
            {
                //finding the most common dish from all the similar clients
                int maxUsedDish = 0;
                foreach (Dish curDish in GetAllDishs())
                {
                    int amount = 0;
                    foreach (Client curClient in mostSimilarClients)//goes over all the clients that are most similar
                        foreach (DishOrder curDO in GetAllDishOrders(item => (myDal.GetClient(myDal.GetOrder(item.OrderID).ClientID) == curClient) && (item.DishID == curDish.ID)))//gets all DishOrders that are from the current dish in this client
                            amount += curDO.DishAmount;
                    if (amount > maxUsedDish)
                    {
                        suggestion = curDish;
                        maxUsedDish = amount;
                    }
                }
                if (suggestion != null)
                    return suggestion;
            }
            Random rand = new Random();
            return GetAllDishs().ToList()[rand.Next(0, GetAllDishs().ToList().Count - 1)];
        }
        #endregion



        #region ToString functions
        /// <summary>
        /// Return A string of the Entire DataBase ready to be print
        /// </summary>
        /// <returns>The string to print</returns>
        public override string ToString()
        {
            int temp = 0;
            string res = "\n The DataBase:\n" + "Dishs:\n\t";
            foreach (Dish item in myDal.GetAllDishs())
                res += "|"+(++temp)+"| "+item + "\n\n\t";
            res = res.Substring(0, res.Length - 1) + "Branchs:\n\t";
            temp = 0;
            foreach (Branch item in myDal.GetAllBranchs())
                res += "|" + (++temp) + "| " + item + "\n\n\t";
            res = res.Substring(0, res.Length - 1) + "Clients:\n\t";
            temp = 0;
            foreach (Client item in myDal.GetAllClients())
                res += "|" + (++temp) + "| " + item + "\n\n\t";
            res = res.Substring(0, res.Length - 1) + "Orders:\n\t";
            temp = 0;
            foreach (Order item in myDal.GetAllOrders())
                res += "|" + (++temp) + "| " + ToStringOrder(item) + "\n\n\t";
            return res;
        }

        public string ToStringOrder(Order order)
        {
            string res = "";
            int temp = 0;
            res += order + "\n";
            foreach (DishOrder item in myDal.GetAllDishOrders(item => item.OrderID == order.ID).OrderBy(item => item.ID))//מסודר כדי שנוכל להשתמש בזה לבחירת קלט
                res += "\t(" + (++temp) + ") Name: " + myDal.GetDish(item.DishID).Name + " Amount: " + item.DishAmount + "\n";
            return res;
        }

        #endregion

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



        #region Search Functions
        public List<IEnumerable<InterID>> Search(string str)
        {
            List<IEnumerable<InterID>> list = new List<IEnumerable<InterID>>();
            list.Add(SearchDishs(str));
            list.Add(SearchClients(str));
            list.Add(SearchBranchs(str));
            list.Add(SearchOrders(str));
            return list;
        }
        IEnumerable<T> Search<T>(string str, IEnumerable<T> list)
        {
            if (str == "")
                return list;
            return from item in list
                   where Include(item, str)
                   select item;
        }
        /// <summary>
        /// Checks if the object (usualy a list of T's) includes the item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item being searched for</param>
        /// <param name="obj">The list</param>
        /// <returns></returns>
        bool Include<T>(T item, string str)
        {
            foreach (PropertyInfo p in item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (Compares(p.GetValue(item), (p.PropertyType.Name == "String"), (p.PropertyType.Name == "Int32"), (p.PropertyType.Name == "Single"), (p.PropertyType.Name == "DateTime"), str))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Compares between an object and a string and see return whether the object equal or contain the string
        /// </summary>
        /// <param name="obj">The object</param>
        /// <param name="IsString">Does the object is string</param>
        /// <param name="IsInt">Does the object is int</param>
        /// <param name="IsFloat">Does the object is float</param>
        /// <param name="IsDate">Does the object is Date</param>
        /// <param name="subStr">The string</param>
        /// <returns>a bool that says whether the object equal or contain the string</returns>
        bool Compares(object obj, bool IsString, bool IsInt, bool IsFloat, bool IsDate, string subStr)
        {
            int temp;
            float temp2;
            if (IsString)
                return (obj as string).ToLower().Contains(subStr.ToLower());
            else if (IsInt && int.TryParse(subStr, out temp))
                return (int)obj == temp;
            else if (IsDate)
                return ((DateTime)obj).ToShortDateString() == (obj as string);
            else if (IsFloat && float.TryParse(subStr, out temp2))
                return ((float)obj) == (temp2);
            return false;

        }
        #endregion

        /// <summary>
        /// Checks the price of a specific order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public float PriceOfOrder(Order order)
        {
            float result = 0;
            List<DishOrder> list = myDal.GetAllDishOrders(item => item.OrderID == order.ID).ToList<DishOrder>();
            foreach (DishOrder item in list)
                result += item.DishAmount * myDal.GetDish(item.DishID).Price;
            return result;
        }

        public void Inti()
        {

            AddDish(new Dish("Soup", Size.LARGE, 13, Kashrut.HIGH, 957473));
            AddDish(new Dish("Hot Dogs", Size.MEDIUM, 15, Kashrut.LOW, 19273));
            AddDish(new Dish("Bamba", Size.SMALL, 5, Kashrut.HIGH, 1243));
            AddDish(new Dish("Wings", Size.MEDIUM, 20, Kashrut.MEDIUM, 95840));
            AddDish(new Dish("Stake", Size.LARGE, 34, Kashrut.LOW, 21));
            AddClient(new Client("Shy", "Sderot Hertzel 12", 45326, 23,1921));
            AddClient(new Client("Ezra", "Beit Shemesh", 78695, 65, 10934));
            AddClient(new Client("Itai", "Giv'at Ze'ev", 1938, 18, 493));
            AddClient(new Client("Tal", "Alon Shvut", 91731, 20, 1313));
            AddClient(new Client("Gal", "Ma'ale Adumim", 38267, 19, 20744));
            AddBranch(new Branch("Jerusalem", "malcha 1", "026587463", "morli", 3, 4, Kashrut.MEDIUM, 87465));
            AddBranch(new Branch("Bnei Brak", "sholm 7", "039872611", "kidron", 1, 5, Kashrut.HIGH, 18932));
            AddBranch(new Branch("Eilat", "freedom 98", "078496352", "oshri", 5, 3, Kashrut.LOW, 2));
            AddBranch(new Branch("Tel Aviv", "zion 6", "032648544", "amram", 10, 10, Kashrut.LOW, 0));
            AddBranch(new Branch("Beit Shemesh", "Big Center 1", "073524121", "joffrey", 2, 3, Kashrut.MEDIUM, 9873));
            AddOrder(new Order(2, "Beit Shemesh shy", DateTime.Now, Kashrut.LOW, 10934, 192334));
            AddOrder(new Order(87465, "Beit Shemesh shy", DateTime.Now, Kashrut.MEDIUM, 1921, 34567));
            AddDishOrder(new DishOrder(192334, 957473, 3));
            AddDishOrder(new DishOrder(192334, 957473, 2));
            AddDishOrder(new DishOrder(192334, 19273, 7));
            AddDishOrder(new DishOrder(34567, 957473, 7));
            myDal.AddUser(new User(UserType.Client,"shy71","123456","Shy Tennenbaum",1921));
        }
    }
}