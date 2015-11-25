using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface Idal
    {
        void AddDish(BE.Dish newDish);
        void DeleteDish(int id);
        void DeleteDish(BE.Dish d);
        void updateDish(int id, BE.Dish d);//האם יקבל ID?

        void AddBranch(BE.Branch newBranch);
        void DeleteBranch(int id);
        void DeleteBranch(BE.Branch b);
        void updateBranch(int id, BE.Branch b);//האם יקבל ID?

        void AddOrder(BE.Order newOrder);
        void DeleteOrder(int id);
        void DeleteOrder(BE.Order o);
        void updateOrder(int id,BE.Order o);//האם יקבל ID?

        void AddDishOrder(BE.DishOrder newDishOrder);
        void DeleteDishOrder(int id);
        void DeleteDishOrder(BE.DishOrder d);
        void updateDishOrder(int id, BE.DishOrder d);//האם יקבל ID?

        List<BE.Dish> getDishs();
        List<BE.Branch> getBranchs();
        List<BE.Order> getOrders();  
    }
    public class DAL
    {
    }
}
