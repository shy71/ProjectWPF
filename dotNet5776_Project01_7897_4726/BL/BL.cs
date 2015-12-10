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
        void AddDish(Dish newDish);
        void DeleteDish(int id);
        void DeleteDish(Dish item);
        void UpdateDish(Dish item);

        void AddBranch(Branch newBranch);
        void DeleteBranch(int id);
        void DeleteBranch(Branch item);
        void UpdateBranch(Branch item);

        void AddOrder(Order newOrder);
        void DeleteOrder(int id);
        void DeleteOrder(Order item);
        void UpdateOrder(Order item);

        void AddDishOrder(DishOrder newDishOrder);
        void DeleteDishOrder(int id);
        void DeleteDishOrder(DishOrder item);
        void UpdateDishOrder(DishOrder item);

        void AddClient(Client newClient);
        void DeleteClient(int id);
        void DeleteClient(Client item);
        void UpdateClient(Client item);

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


        /// <summary>
        /// Checks if a client's age is over 18
        /// </summary>
        /// <param name="customer">the client being checked</param>
        /// <returns>True if the customer's age is over 18,
        ///          False if he is under 18</returns>
        bool CheckClientAge(Client customer);

    }
    public class BL //: IBL
    {
        DAL.Idal myDal = DAL.FactoryDal.getDal();
        public float PriceOfOrder(Order order)
        {
            float result=0;
            List<DishOrder> list = myDal.getAllDishOrders(order);
            foreach (DishOrder item in list)
                result += item.DishAmount * myDal.getByID<Dish>(item.DishID).Price;
            return result;
        }

    }
}
