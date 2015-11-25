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
        void UpdateDish(BE.Dish d);//האם יקבל ID?

        void AddBranch(BE.Branch newBranch);
        void DeleteBranch(int id);
        void DeleteBranch(BE.Branch b);
        void UpdateBranch(BE.Branch b);//האם יקבל ID?

        void AddOrder(BE.Order newOrder);
        void DeleteOrder(int id);
        void DeleteOrder(BE.Order o);
        void UpdateOrder(BE.Order o);//האם יקבל ID?

        void AddDishOrder(BE.DishOrder newDishOrder);
        void DeleteDishOrder(int id);
        void DeleteDishOrder(BE.DishOrder d);
        void UpdateDishOrder(BE.DishOrder d);//האם יקבל ID?

        void AddClient(BE.Client newClient);
        void DeleteClient(int id);
        void DeleteClient(BE.Client c);
        void UpdateClient(BE.Client c);//האם יקבל ID?

        List<BE.Dish> getDishs();
        List<BE.Branch> getBranchs();
        List<BE.Order> getOrders();
        List<BE.Client> getClients();
        List<BE.DishOrder> getDishOrders();
    }
    class Dal_imp//: Idal //להוסיף כשנגמור לממש את הכול שהיא יורשת מהאינטרפייס
    {
        Random rand=new Random();
        /// <summary>
        /// מוסיפה איבר לרשימה, יחד עם כל הבדיקות הנצרכות
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="newItem">האיבר שהפוקנציה תוסיף</param>
        /// <param name="list">הרשימה לה היא תוסיף אותה</param>
        void Add<T>(T newItem,List<T> list)where T : BE.InterID
        {
            if (newItem.ID == 0 || ContainID(newItem.ID, list))
                newItem.ID = NextID(list);
            list.Add(newItem);
        }
        /// <summary>
        /// מוחקת איבר מהרשימה באמצעות תעודת הזהות
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="id">תעודת הזהות של האיבר אותו אנו מבקשים למחוק</param>
        /// <param name="list">הרשימה ממנה נמחוק אותו</param>
        void Delete<T>(int id, List<T> list) where T : BE.InterID
        {
            if (ContainID(id, list) == false)
                return; //ERROR
            list.RemoveAt(IndexByID(id, list));
        }
       /// <summary>
        /// מוחקת איבר מהרשימה באמצעות האיבר עצמו
       /// </summary>
       /// <typeparam name="T">סוג האיבר</typeparam>
       /// <param name="item">האיבר אותו אנו מבקשים למחוק</param>
       /// <param name="list">הרשימה ממנה נמחק אותו</param>
        void Delete<T>(T item, List<T> list) where T : BE.InterID { Delete(item.ID, list); }
        /// <summary>
        /// עדכון איבר מהרשימה באמצעות איבר מעודכן
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="item">האיבר המעודכן שבעזרת תעדות הזהות מסמן על האיבר שנעדכן</param>
        /// <param name="list">הרשימה בא נמצא האיבר אותו נעדכן</param>
        void Update<T>(T item,List<T> list)where T : BE.InterID
        {
            if (ContainID(item.ID, list) == false)
                return; //ERROR
            list.RemoveAt(IndexByID(item.ID, list));
            list.Add(item);

        }
        /// <summary>
        /// מביאה תעודת זהות פנוייה באופן רנדומלי
        /// </summary>
        /// <typeparam name="T">סוג האיבר שצריך תעדות זהות</typeparam>
        /// <param name="list">הרשימה בה נמצאים שאר האיברים מסוג זה</param>
        /// <returns>מחזירה את תעדות הזהות הפנוייה</returns>
        int NextID<T>(List<T> list) where T : BE.InterID 
        {
            int id = rand.Next(1, 100000000);
            while (ContainID(id, getDishs()) == true)
                id = rand.Next(1, 100000000);
            return id;
        }
        /// <summary>
        /// בודקת האם קיים איבר ברשימה עם תעודת הזהות הזאת
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="id">תעדות הזהות</param>
        /// <param name="list">הרשימה בה נמצאים האיברים</param>
        /// <returns>מחזירה משתנה בוליאני המציין האם קיים איבר עם תעודת הזהות הזאת</returns>
        bool ContainID<T>(int id, List<T> list) where T : BE.InterID 
        {
            foreach (T item in list)
                if (item.ID == id)
                    return true;
            return false;
        }
        /// <summary>
        /// בודקת באזיה אינדקס נמצא האיבר בעל תעודת הזהות הזו
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="id">תעדות הזהות</param>
        /// <param name="list">הרשימה בה נממצאים האיברים </param>
        /// <returns>מחזירה אינדקס של מיקום האיבר</returns>
        int IndexByID<T>(int id, List<T> list) where T : BE.InterID 
        {
            int num=0;
            foreach (T item in list)
            {
                if (item.ID == id)
                    return num;
                num++;
            }
            return -1;
        }

        void AddDish(BE.Dish newDish)//דוגמא להפניות לפונקציות שצריך לעשות...
        {
            Add(newDish, getDishs());
        }


        List<BE.Dish> getDishs()
        {
            return DS.DataSource.DishList;
        }
        List<BE.Branch> getBranchs()
        {
            return DS.DataSource.BranchList;
        }
        List<BE.Order> getOrders()
        {
            return DS.DataSource.OrderList;
        }
        List<BE.Client> getClients()
        {
            return DS.DataSource.ClientList;
        }
        List<BE.DishOrder> getDishOrders()
        {
            return DS.DataSource.DishOrderList;
        }
    }
}
