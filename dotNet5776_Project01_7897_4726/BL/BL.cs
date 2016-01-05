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
        void AddDish(Dish newDish);
        void DeleteDish(int id);
        void DeleteDish(Dish item);
        void UpdateDish(Dish item);
        IEnumerable<Dish> GetAllDishs(Func<Dish, bool> predicate = null);
        IEnumerable<Dish> SearchDishs(object str);

        #endregion

        #region Branch Functions
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
        void DeliveredOrder(int id);
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
        void Inti();
        //Add grouping functions 

        IEnumerable<IGrouping<int, float>> GetProfitByDishs();
        IEnumerable<IGrouping<int, float>> GetProfitByClients();
        IEnumerable<IGrouping<string, float>> GetProfitByDates();
    }
    public class BL : IBL
    {
        public readonly int MAX_PRICE;
        DAL.Idal myDal = DAL.FactoryDal.getDal();
        public BL(int maxPrice = 1000)//need checking
        {
            MAX_PRICE = maxPrice;
        }
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
        public IEnumerable<IGrouping<int, float>> GetProfitByClients()
        {
            return from item in myDal.GetAllDishOrders()
                   group item.DishAmount * myDal.GetDish(item.DishID).Price by myDal.GetOrder(item.OrderID).ClientID;
        }
        public IEnumerable<IGrouping<string, float>> GetProfitByDates()
        {
            return from item in myDal.GetAllDishOrders()
                   group item.DishAmount * myDal.GetDish(item.DishID).Price by myDal.GetOrder(item.OrderID).Date.ToShortDateString();
        }
        #endregion


        bool Include<T>(T item, object obj)//צריך לבדוק אם אפשר להעביר לLinq
        {
            foreach (PropertyInfo p in item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (compere(p.GetValue(item), (p.PropertyType.Name == "String"), (p.PropertyType.Name == "Int32"), obj))
                {
                    return true;
                }
            }
            return false;
        }
        bool compare(object obj,bool IsString,bool IsInt,object subObj)
        {
            if (IsString&&subObj.GetType().Name=="Int32")
                return (obj as string).ToLower().Contains((subObj as string).ToLower());
            else if (IsInt)
                return obj == subObj;
            return false;

        }

        public IEnumerable<T> Search<T>(object str, IEnumerable<T> list)
        {
            return from item in list
                    where Include(item, str)
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

            AddDish(new Dish("Soup", Size.LARGE, 13, Kashrut.HIGH, 957473));
            AddDish(new Dish("Hot Dogs", Size.MEDIUM, 15, Kashrut.LOW, 19273));
            AddDish(new Dish("Bamba", Size.SMALL, 5, Kashrut.HIGH, 1243));
            AddDish(new Dish("Wings", Size.MEDIUM, 20, Kashrut.MEDIUM, 95840));
            AddDish(new Dish("Stake", Size.LARGE, 34, Kashrut.LOW, 21));
            AddClient(new Client("Shy", "Sderot Hertzel 12", 45326, 23, 1921));
            AddClient(new Client("Ezra", "Beit Shemesh", 78695, 65, 10934));
            AddClient(new Client("Itai", "Giv'at Ze'ev", 1938, 18, 493));
            AddClient(new Client("Tal", "Alon Shvut", 91731, 20, 1313));
            AddClient(new Client("Gal", "Ma'ale Adumim", 38267, 19, 20744));
            AddBranch(new Branch("Jerusalem", "malcha 1", "026587463", "morli", 3, 4, Kashrut.MEDIUM, 87465));
            AddBranch(new Branch("Bnei Brak", "sholm 7", "039872611", "kidron", 1, 5, Kashrut.HIGH, 18932));
            AddBranch(new Branch("Eilat", "freedom 98", "078496352", "oshri", 5, 3, Kashrut.LOW, 2));
            AddBranch(new Branch("Tel Aviv", "zion 6", "032648544", "amram", 10, 10, Kashrut.LOW, 0));
            AddBranch(new Branch("Beit Shemesh", "Big Center 1", "073524121", "joffrey", 2, 3, Kashrut.MEDIUM, 9873));
            AddOrder(new Order(2, "Beit Shemesh", DateTime.Now, Kashrut.LOW, 10934, 192334));
            AddDishOrder(new DishOrder(192334, 957473, 3));
            AddDishOrder(new DishOrder(192334, 957473, 2));
            AddDishOrder(new DishOrder(192334, 19273, 2));
        }
        
        //לחשוב אולי אפשר יהיה לעדכן שדות מוסימים גם בזמן שיש הזמנות לדבר
        #region Dish Functions
        internal void CompatibleDish(Dish dish, string str = null)//need checking
        {
            if (dish.Price <= 0)
                throw new Exception(str + " The price of a dish have to be highr then zero!");
            else if (dish.Name == null)
                throw new Exception(str + " A dish has to have a name!");
            //else if (dish.Kosher == null)
            //    throw new Exception(str+" A dish has to have a kosher level!");
            //else if (dish.Size == null)
            //    throw new Exception(str+" A dish has to have a Size level");
        }
        public void AddDish(Dish newDish)//need checking
        {
            CompatibleDish(newDish, "The Dish you are trying to add is incompatible:");
            myDal.AddDish(newDish);
        }
        public void DeleteDish(int id)//need checking
        {
            if (myDal.GetAllDishOrders(item => item.DishID == id).ToList().Count == 0)
                myDal.DeleteDish(id);
            else
                throw new Exception("You can't delete a dish which is being ordered");
        }
        public void DeleteDish(Dish item)//need checking
        {
            DeleteDish(item.ID);
        }
        public void UpdateDish(Dish item)//need checking
        {
            //There isn't an option to change the ID;
            if (myDal.GetAllDishOrders(var => var.DishID == item.ID).ToList().Count == 0)
            {
                CompatibleDish(item, "The Updated Dish you sended to upadte the old one is incompatible:");
                myDal.UpdateDish(item);
            }
            else
                throw new Exception("You can't update a dish which is being ordered");
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
            if (myDal.GetAllOrders(item => item.BranchID == id).ToList<Order>().Count == 0)
                myDal.DeleteBranch(id);
            else
                throw new Exception("you cant delete a branch that has active orders from!");
        }
        public void DeleteBranch(Branch myBranch)//need checking
        {
            DeleteBranch(myBranch.ID);
        }
        public void UpdateBranch(Branch myBranch)//need checking
        {
            if (myDal.GetAllOrders(item => item.BranchID == myBranch.ID).ToList<Order>().Count == 0)
            {
                CompatibleBranch(myBranch, "The updated branch you sended to upadte the old one is incompatible:");
                myDal.UpdateBranch(myBranch);
            }
            else
                throw new Exception("you can't update a bracnh that has active orders from!");
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
                throw new Exception(str + " The Date filed cant be empty!");
            //else if (myOrder.Kosher == null)
            //    throw new Exception(str+" The kashrut filed cant be empty");
            else if (myOrder.Kosher > myDal.GetBranch(myOrder.BranchID).Kosher)//need checking
                throw new Exception(str + " The Kashrut in the branch is not sufficient for the order");
            else if (myDal.GetBranch(myOrder.BranchID).AvailableMessangers == 0)
                throw new Exception(str + " There isnt any available messangers to deliver the order");
        }
        public void AddOrder(Order newOrder)//need checking
        {

            CompatibleOrder(newOrder, "The Order you are trying to add is incompatible:");
                myDal.AddOrder(newOrder);
        }
        public void DeleteOrder(int id)//need checking
        {
            foreach (DishOrder item in myDal.GetAllDishOrders(item => item.OrderID == id).ToList())
                DeleteDishOrder(item);
            myDal.DeleteOrder(id);
        }
        public void DeleteOrder(Order myOrder)//need checking
        {
            DeleteOrder(myOrder.ID);
        }
        public void UpdateOrder(Order newOrder)//fixed! need checking
        {
            //make sure that kashrut doesn't contradict kashrut of branch or dishes
            Order oldOrder = myDal.GetOrder(newOrder.ID);
            if (newOrder.Kosher > oldOrder.Kosher)//בהנחה שהם היו ברמת הכשר שלו עד אז
                if (myDal.GetAllDishOrders(item => item.OrderID == newOrder.ID).Any(item => myDal.GetDish(item.DishID).Kosher < newOrder.Kosher))
                    throw new Exception("You can't change the order's kashrut level because it has dishes which aren't in the new sufficient kashrout level");
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
        public void UpdateDishOrder(DishOrder item)//need checking
        {
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
            if (myDal.GetAllOrders(item => item.ClientID == id).ToList().Count > 0)
                throw new Exception("You cant delete a Client that has active orders");
            myDal.DeleteClient(id);
        }
        public void DeleteClient(Client item)//need checking
        {
            DeleteClient(item.ID);
        }
        public void UpdateClient(Client item)//need checking
        {
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