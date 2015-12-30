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
            float result=0;
            List<DishOrder> list = myDal.GetAllDishOrders(item=>item.OrderID==order.ID).ToList<DishOrder>();
            foreach (DishOrder item in list)
                result += item.DishAmount * myDal.GetDish(item.DishID).Price;
            return result;
        }

        #region Dish Functions // להוסיף חריגות
        internal bool CompatableDish(Dish dish)
        {
            return !(dish.Price <= 0 ||dish.Name==null||dish.Kosher==null|dish.Size==null);
        }
        public void AddDish(Dish newDish)
        {
            if(CompatableDish(newDish))
                myDal.AddDish(newDish);
        }
        internal void CheckIfDishOrder(int id)
        {
            IEnumerable<int> DishIDList = from item in myDal.GetAllDishOrders(var => (var.DishID==id))
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
        internal bool CompatableBranch(Branch branch)
        {
            return !(branch.Address==null || branch.Boss==null || branch.EmployeeCount<=0 || branch.PhoneNumber==null || branch.Name==null);
        }
        public void AddBranch(Branch newBranch)
        {
            if(CompatableBranch(newBranch))
                myDal.AddBranch(newBranch);
        }
        public void DeleteBranch(int id)
        {
            List<Order> orderList = myDal.GetAllOrders(item => item.BranchID == id).ToList<Order>();
            if (orderList.Count == 0)
                myDal.DeleteBranch(id);
        }
        public void DeleteBranch(Branch myBranch)
        {
            DeleteBranch(myBranch.ID);
        }
        public void UpdateBranch(Branch myBranch)
        {
            List<Order> orderList = myDal.GetAllOrders(item => item.BranchID == myBranch.ID).ToList<Order>();
            if (orderList.Count == 0)
                myDal.UpdateBranch(myBranch);
        }
        #endregion

        #region Order Functions //להוסיף חריגות
        internal bool CompatableOrder(Order myOrder)//now finished
        {
            return (myOrder.Address != "" && myOrder.Date!=null
                && myDal.ContainID<Client>(myOrder.ClientID) && myDal.ContainID<Branch>(myOrder.BranchID));//kashrut check
        }
        public void AddOrder(Order newOrder)
        {
            if (CompatableOrder(newOrder))
                myDal.AddOrder(newOrder);
            //change kashrut
        }
        public void DeleteOrder(int id)//not finshed
        {

        }
        #endregion

        #region Client Functions
        internal bool CompatableClient(Client client)//האם יש הגבלות יותר מוסימות על כרטיס אשראי?
        {
            return (client.Address != null && client.CreditCard != null && client.Name != null);
        }
        void AddClient(Client newClient)
        {
            if (!CompatableClient(newClient))
                throw new Exception("The filed of the Client were filed incorrectly");
            myDal.AddClient(newClient);
        }
        void DeleteClient(int id)
        {
            if (myDal.GetAllOrders(item => item.ClientID == id).ToList().Count > 0)
                throw new Exception("You cant delete a Client that has active orders");
            myDal.DeleteClient(id);
        }
        void DeleteClient(Client item)
        {
            DeleteClient(item.ID);
        }
        void UpdateClient(Client item)
        {
            if (!CompatableClient(item))
                throw new Exception("The filed of the update for the client were filed incorrectly");
            myDal.UpdateClient(item);
        }
        #endregion

        public void inti()
        {

           AddDish(new Dish("Soup", Size.LARGE, 13, Kashrut.HIGH));
            AddDish(new Dish("Hot Dogs", Size.MEDIUM, 15, Kashrut.LOW));
            AddDish(new Dish("Bamba", Size.SMALL, 5, Kashrut.HIGH));
            AddDish(new Dish("Wings", Size.MEDIUM, 20, Kashrut.MEDIUM));
            AddDish(new Dish("Stake", Size.LARGE, 34, Kashrut.LOW));
           AddClient(new Client("Shy", "Sdarot herzl 12", 45326));
            AddClient(new Client("ezra", "bait shmes(chor)", 78695));
            AddClient(new Client("itai", "zev hill", 1938));
            AddClient(new Client("tal", "alon svut", 91731));
            AddClient(new Client("gal", "male adomim", 38267));
            AddBranch(new Branch("jerusalem", "malcha 1", "026587463", "morli", 3, 4, Kashrut.MEDIUM));
            AddBranch(new Branch("bni brack", "sholm 7", "039872611", "kidron",1, 5, Kashrut.HIGH));
            AddBranch(new Branch("ailte", "freedom 98", "078496352", "oshri", 5, 3, Kashrut.LOW));
            AddBranch(new Branch("tel aviv", "zion 6", "032648544", "amram", 10, 10, Kashrut.LOW));
            AddBranch(new Branch("bear sheva", "shbub street", "073524121", "joffrey", 2, 3, Kashrut.MEDIUM));
            
        }
    }
}