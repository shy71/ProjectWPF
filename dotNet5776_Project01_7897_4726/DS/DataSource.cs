using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public class DataSource
    {
        static public List<BE.Dish> DishList = new List<BE.Dish>();
        static public int DishIDCounter;
        static public List<BE.Branch> BranchList = new List<BE.Branch>();
        static public int BranchIDCounter;
        static public List<BE.Client> ClientList = new List<BE.Client>();
        static public int ClientIDCounter;
        static public List<BE.DishOrder> DishOrderList = new List<BE.DishOrder>();
        static public int DishOrderIDCounter;
        static public List<BE.Order> OrderList = new List<BE.Order>();
        static public int OrderIDCounter;
    }
}
