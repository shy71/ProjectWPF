using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Extensions
    {
        internal static int MakeID(params string[] str)
        {
            return str.Select((item, index) => (item.Sum(r => (int)r)) * (int)Math.Pow(10, index)).Sum() % 100000000;
        }
    }
    //public class Gain
    //{
    //    public Gain(int totalProfit, int amount, int dishID,DateTime date,string address)
    //    {
    //        TotalProfit = totalProfit;
    //        DishID = dishID;
    //        Date = date;
    //        Address = address;
    //    }
    //    public Gain(DishOrder dishorder,Order order,Dish dish)
    //    {//תנאי בדיקה
    //        Amount = dishorder.DishAmount;
    //        DishID = dishorder.DishID;
    //        Date = order.Date;
    //        Address = order.Address;
    //        TotalProfit = dish.Price * Amount;
    //    }
    //    public float TotalProfit { get; set; }
    //    public int Amount { get; set; }
    //    public int DishID { get; set; }
    //    public DateTime Date { get; set; }
    //    public string Address { get; set; }
    //}
    public enum Kashrut
    {
        LOW, MEDIUM, HIGH
    }
    public enum Size
    {
        SMALL, MEDIUM, LARGE
    }
}
