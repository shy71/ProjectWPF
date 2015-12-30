using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public delegate bool SortOutOrdersFunc(Order order);
    public interface IBL
    {
        #region Dish Functions
        void AddDish(Dish newDish);
        void DeleteDish(int id);
        void DeleteDish(Dish item);
        void UpdateDish(Dish item);
        #endregion

        #region Branch Functions
        void AddBranch(Branch newBranch);
        void DeleteBranch(int id);
        void DeleteBranch(Branch item);
        void UpdateBranch(Branch item);
        #endregion

        #region Order Functions
        void AddOrder(Order newOrder);
        void DeleteOrder(int id);
        void DeleteOrder(Order item);
        void UpdateOrder(Order item);
        #endregion

        #region DishOrder Functions
        void AddDishOrder(DishOrder newDishOrder);
        void DeleteDishOrder(int id);
        void DeleteDishOrder(DishOrder item);
        void UpdateDishOrder(DishOrder item);
        #endregion

        #region Client Functions
        void AddClient(Client newClient);
        void DeleteClient(int id);
        void DeleteClient(Client item);
        void UpdateClient(Client item);
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
        bool CheckMaximumPrice(Order order);
        /// <summary>
        /// Checks if the order has a curtain hechshe
        /// </summary>
        /// <param name="order">order being checked</param>
        /// <param name="hechsher">kashrut it has to be</param>
        /// <returns>False if the order isn't the right hechsher
        ///          True if it is</returns>
        bool CheckForHechsher(Order order, Kashrut hechsher);
        /// <summary>
        /// Sorts out spicific orders from the list that is true in the condition
        /// from the condition fuction
        /// </summary>
        /// <param name="condition">Condition function</param>
        /// <returns>List of all the orders that the condition returns True</returns>
        List<Order> GetSpicificOrders(SortOutOrdersFunc condition);

        //Add grouping functions 

    }
    public class BL //: IBL
    {
        DAL.Idal myDal = DAL.FactoryDal.getDal();
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

            AddDish(new Dish("Soup", Size.LARGE, 13, Kashrut.HIGH));
            AddDish(new Dish("Hot Dogs", Size.MEDIUM, 15, Kashrut.LOW));
            AddDish(new Dish("Bamba", Size.SMALL, 5, Kashrut.HIGH));
            AddDish(new Dish("Wings", Size.MEDIUM, 20, Kashrut.MEDIUM));
            AddDish(new Dish("Stake", Size.LARGE, 34, Kashrut.LOW));
            AddClient(new Client("Shy", "Sderot Hertzel 12", 45326));
            AddClient(new Client("Ezra", "Beit Shemesh", 78695));
            AddClient(new Client("Itai", "Giv'at Ze'ev", 1938));
            AddClient(new Client("Tal", "Alon Shvut", 91731));
            AddClient(new Client("Gal", "Ma'ale Adumim", 38267));
            AddBranch(new Branch("Jerusalem", "malcha 1", "026587463", "morli", 3, 4, Kashrut.MEDIUM));
            AddBranch(new Branch("Bnei Brak", "sholm 7", "039872611", "kidron", 1, 5, Kashrut.HIGH));
            AddBranch(new Branch("Eilat", "freedom 98", "078496352", "oshri", 5, 3, Kashrut.LOW));
            AddBranch(new Branch("Tel Aviv", "zion 6", "032648544", "amram", 10, 10, Kashrut.LOW));
            AddBranch(new Branch("Beit Shemesh", "Big Center 1", "073524121", "joffrey", 2, 3, Kashrut.MEDIUM));
        }

        #region Dish Functions // להוסיף חריגות
        internal bool CompatibleDish(Dish dish)
        {
            return !(dish.Price <= 0 || dish.Name == null || dish.Kosher == null | dish.Size == null);
        }
        public void AddDish(Dish newDish)
        {
            if (CompatibleDish(newDish))
                myDal.AddDish(newDish);
            else
                throw new Exception("The dish is incompatible");
        }
        internal void CheckIfDishOrder(int id)
        {
            IEnumerable<int> DishIDList = from item in myDal.GetAllDishOrders(var => (var.DishID == id))
                                          select item.DishID;
            if (DishIDList.ToList<int>().Count != 0)
                throw new Exception("You can't delete a dish which is being ordered");
        }
        public void DeleteDish(int id)
        {
            CheckIfDishOrder(id);
            myDal.DeleteDish(id);
        }
        public void DeleteDish(Dish item)
        {
            DeleteDish(item.ID);
        }
        public void UpdateDish(Dish item)
        {
            //There isn't an option to change the ID;
            CheckIfDishOrder(item.ID);
            myDal.UpdateDish(item);
        }
        #endregion

        #region Branch Functions // להוסיף חריגות
        internal bool CompatibleBranch(Branch branch)
        {
            return !(branch.Address == null || branch.Boss == null || branch.EmployeeCount <= 0 || branch.PhoneNumber == null || branch.Name == null);
        }
        public void AddBranch(Branch newBranch)
        {
            if (CompatibleBranch(newBranch))
                myDal.AddBranch(newBranch);
            else
                throw new Exception("The branch is incompatible");
        }
        public void DeleteBranch(int id)
        {
            if (myDal.GetAllOrders(item => item.BranchID == id).ToList<Order>().Count == 0)
                myDal.DeleteBranch(id);
        }
        public void DeleteBranch(Branch myBranch)
        {
            DeleteBranch(myBranch.ID);
        }
        public void UpdateBranch(Branch myBranch)
        {
            if (myDal.GetAllOrders(item => item.BranchID == myBranch.ID).ToList<Order>().Count == 0)
                myDal.UpdateBranch(myBranch);
        }
        #endregion

        #region Order Functions
        internal bool CompatibleOrder(Order myOrder)
        {
            return (myOrder.Address != "" && myOrder.Date != null
                && myDal.ContainID<Client>(myOrder.ClientID) && myDal.ContainID<Branch>(myOrder.BranchID)
                && myOrder.Kosher == myDal.GetBranch(myOrder.BranchID).Kosher);
        }
        public void AddOrder(Order newOrder)
        {
            if (CompatibleOrder(newOrder))
                myDal.AddOrder(newOrder);
            else
                throw new Exception("The order is incompatible");
        }
        public void DeleteOrder(int id)
        {
            IEnumerable<DishOrder> ordersDishes = from item in myDal.GetAllDishOrders()
                                                  where item.OrderID == id
                                                  select item;
            foreach (DishOrder item in ordersDishes)
                DeleteDishOrder(item);
            myDal.DeleteOrder(id);
        }
        public void DeleteOrder(Order myOrder)
        {
            IEnumerable<DishOrder> ordersDishes = from item in myDal.GetAllDishOrders()
                                                  where item.OrderID == myOrder.ID
                                                  select item;
            foreach (DishOrder item in ordersDishes)
                DeleteDishOrder(item);
            myDal.DeleteOrder(myOrder);
        }
        public void UpdateOrder(Order newOrder)
        {
            //make sure that kashrut doesn't contradict kashrut of branch pr dishes
            Order oldOrder = myDal.GetOrder(newOrder.ID);
            if(newOrder.Kosher != oldOrder.Kosher)//בהנחה שהם היו ברמת הכשר שלו עד אז
                if (myDal.ContainID<DishOrder>(newOrder.ID) || myDal.ContainID<Dish>(newOrder.ID))
                    throw new Exception("You can't change the order's kashrut level because it has dishes which aren't the same kahrut level");
            myDal.UpdateOrder(newOrder);
        }
        #endregion

        #region DishOrder Functions
        internal bool CompatibleDishOrder(DishOrder theDishOrder)
        {
            return (theDishOrder.DishAmount > 0 
                && myDal.ContainID<Dish>(theDishOrder.DishID) 
                && myDal.ContainID<Order>(theDishOrder.OrderID)) ;
        }
        public void AddDishOrder(DishOrder newDishOrder)
        {
            if (CompatibleDishOrder(newDishOrder))
                myDal.AddDishOrder(newDishOrder);
            else
                throw new Exception("The DishOrder is incompatible");
        }
        public void DeleteDishOrder(DishOrder item)//not finished
        {
            myDal.DeleteDishOrder(item);
        }
        public void DeleteDishOrder(int id)
        {
            myDal.DeleteDishOrder(id);
        }
        public void UpdateDishOrder(DishOrder item)
        {
            myDal.UpdateDishOrder(item);
        }
        #endregion
        #region Client Functions
        internal bool CompatibleClient(Client client)//האם יש הגבלות יותר מוסימות על כרטיס אשראי?
        {
            return (client.Address != null && client.CreditCard != null && client.Name != null);
        }
        public void AddClient(Client newClient)
        {
            if (!CompatibleClient(newClient))
                throw new Exception("The filed of the Client were filed incorrectly");
            myDal.AddClient(newClient);
        }
        public void DeleteClient(int id)
        {
            if (myDal.GetAllOrders(item => item.ClientID == id).ToList().Count > 0)
                throw new Exception("You cant delete a Client that has active orders");
            myDal.DeleteClient(id);
        }
        public void DeleteClient(Client item)
        {
            DeleteClient(item.ID);
        }
        public void UpdateClient(Client item)
        {
            if (!CompatableClient(item))
                throw new Exception("The filed of the update for the client were filed incorrectly");
            myDal.UpdateClient(item);
        }
        #endregion
    }
}