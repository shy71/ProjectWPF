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

        #region Dish Functions
        internal bool CompatableDish(Dish theDish)
        {
            return !(theDish.ID < 0 || theDish.Price <= 0);
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
            CheckIfDishOrder(item.ID);
            myDal.DeleteDish(item.ID);
        }
        public void UpdateDish(Dish item)
        {
            //There isn't an option to change the ID;
            CheckIfDishOrder(item.ID);
            myDal.UpdateDish(item);
        }
        #endregion

        #region Branch Functions
        internal bool CompatableBranch(Branch theBranch)
        {
            return !(theBranch.Address==null || theBranch.Boss==null || theBranch.EmployeeCount<=0 || theBranch.ID<=0 || theBranch.PhoneNumber==null || theBranch.Name==null);
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
            List<Order> orderList = myDal.GetAllOrders(item => item.BranchID == myBranch.ID).ToList<Order>();
            if (orderList.Count == 0)
                myDal.DeleteBranch(myBranch.ID);
        }
        public void UpdateBranch(Branch myBranch)
        {
            List<Order> orderList = myDal.GetAllOrders(item => item.BranchID == myBranch.ID).ToList<Order>();
            if (orderList.Count == 0)
                myDal.UpdateBranch(myBranch);
        }
        #endregion

        #region OrderFunctions
        internal bool CompatableOrder(Order myOrder)//now finished
        {
            return (myOrder.Address != "" && myOrder.BranchID >= 0 && myOrder.ClientID >= 0 
                && myOrder.ID >= 0 && myDal.ContainID<Client>(myOrder.ClientID) && myDal.ContainID<Branch>(myOrder.BranchID));//kashrut check
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


        public void inti()
        {
            myDal.AddDish(new Dish("Soup", Size.LARGE, 13, Kashrut.HIGH));
            myDal.AddDish(new Dish("Hot Dogs", Size.MEDIUM, 15, Kashrut.LOW));
            myDal.AddDish(new Dish("Bamba", Size.SMALL, 5, Kashrut.HIGH));
            myDal.AddDish(new Dish("Wings", Size.MEDIUM, 20, Kashrut.MEDIUM));
            myDal.AddDish(new Dish("Stake", Size.LARGE, 34, Kashrut.LOW));
            myDal.AddClient(new Client("Shy", "Sdarot herzl 12", 45326));
            myDal.AddClient(new Client("ezra", "bait shmes(chor)", 78695));
            myDal.AddClient(new Client("itai", "zev hill", 1938));
            myDal.AddClient(new Client("tal", "alon svut", 91731));
            myDal.AddClient(new Client("gal", "male adomim", 38267));
            myDal.AddBranch(new Branch("jerusalem", "malcha 1", "026587463", "morli", 3, 4, Kashrut.MEDIUM));
            myDal.AddBranch(new Branch("bni brack", "sholm 7", "039872611", "kidron",1, 5, Kashrut.HIGH));
            myDal.AddBranch(new Branch("ailte", "freedom 98", "078496352", "oshri", 5, 3, Kashrut.LOW));
            myDal.AddBranch(new Branch("tel aviv", "zion 6", "032648544", "amram", 10, 10, Kashrut.LOW));
            myDal.AddBranch(new Branch("bear sheva", "shbub street", "073524121", "joffrey", 2, 3, Kashrut.MEDIUM));
            
        }
    }
}