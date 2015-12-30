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
            CompatableBranch(newBranch);
            myDal.AddBranch(newBranch);
        }
        public void DeleteBranch(int id)//unfinished
        {
            List<Order> orderList = myDal.GetAllOrders(item => item.BranchID == id).ToList<Order>();
            if (orderList.Count == 0)
                myDal.DeleteBranch(id);
        }
        #endregion
    }
}