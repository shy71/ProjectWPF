using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    /// <summary>
    /// A class for conection between a dish and an order
    /// </summary>
    public class DishOrder : InterID
    {
        /// <summary>
        /// The default constructor
        /// </summary>
        public DishOrder() { }
        /// <summary>
        /// regular constructor
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="dishID"></param>
        /// <param name="dishAmount"></param>
        /// <param name="id"></param>
        public DishOrder(int orderID, int dishID, int dishAmount = 1, int id = 0)
        {
            ID = id;
            OrderID = orderID;
            DishID = dishID;
            DishAmount = dishAmount;
        }
        /// <summary>
        /// regular constructor
        /// </summary>
        /// <param name="order"></param>
        /// <param name="dish"></param>
        /// <param name="amout"></param>
        /// <param name="id"></param>
        public DishOrder(Order order, Dish dish, int amout = 1, int id = 0)
        {
            OrderID = order.ID;
            DishID = dish.ID;
            DishAmount = amout;
            ID = id;

        }

        /// <summary>
        /// The ID of the DishOrder
        /// </summary>
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// The order of the DishOrder's ID
        /// </summary>
        int orderID;
        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }
        /// <summary>
        /// The dish of the DishOrder's ID
        /// </summary>
        int dishID;
        public int DishID
        {
            get { return dishID; }
            set { dishID = value; }
        }
        /// <summary>
        /// The amount of dishes in of this type in the order
        /// </summary>
        int dishAmount;
        public int DishAmount
        {
            get { return dishAmount; }
            set { dishAmount = value; }
        }


        /// <summary>
        /// ממיר את ההזמנת מנה למחרוזת
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Dish In Order"
                 + "\n\tOrder number: " + OrderID
                 + "\n\tDish number: " + DishID
                 + "\n\tAmount of dishes ordered: " + DishAmount;
        }
        /// <summary>
        /// Generates a unique ID based on the strings in the class
        /// </summary>
        /// <returns>The unique ID</returns>
        public int MakeID()
        {
            return Extensions.MakeID(OrderID.ToString(), DishID.ToString(), DishAmount.ToString());
        }
    }
}
