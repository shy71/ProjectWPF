using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL 
{
    public delegate bool SortOutOrdersFunc(BE.Order order);
    public interface IBL
    {
        /// <summary>
        /// Returns Price of the order
        /// </summary>
        /// <param name="order">Order being priced</param>
        /// <returns>Price</returns>
        int OrderPrice(BE.Order order);
        /// <summary>
        /// Checks if the price exceeds maximum price for an order
        /// </summary>
        /// <param name="order">Order being checked</param>
        /// <returns>False if it exceeds the maximum price,
        ///          True if it doesn't exceed the maximum price</returns>
        bool CheckMaximumPrice(BE.Order order);
        /// <summary>
        /// Checks if the order has a curtain hechshe
        /// </summary>
        /// <param name="order">order being checked</param>
        /// <param name="hechsher">kashrut it has to be</param>
        /// <returns>False if the order isn't the right hechsher
        ///          True if it is</returns>
        bool CheckForHechsher(BE.Order order, BE.Kashrut hechsher);

        
        /// <summary>
        /// Sorts out spicific orders from the list that is true in the condition
        /// from the condition fuction
        /// </summary>
        /// <param name="condition">Condition function</param>
        /// <returns>List of all the orders that the condition returns True</returns>
        List<BE.Order> GetSpicificOrders(SortOutOrdersFunc condition);

        //Add grouping functions 


        /// <summary>
        /// Checks if a client's age is over 18
        /// </summary>
        /// <param name="customer">the client being checked</param>
        /// <returns>True if the customer's age is over 18,
        ///          False if he is under 18</returns>
        bool CheckClientAge(BE.Client customer);

    }
    public class BL
    {

    }
}
