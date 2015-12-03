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
        void DeleteDish(BE.Dish item);
        void UpdateDish(BE.Dish item);//האם יקבל ID?

        void AddBranch(BE.Branch newBranch);
        void DeleteBranch(int id);
        void DeleteBranch(BE.Branch item);
        void UpdateBranch(BE.Branch item);//האם יקבל ID?

        void AddOrder(BE.Order newOrder);
        void DeleteOrder(int id);
        void DeleteOrder(BE.Order item);
        void UpdateOrder(BE.Order item);//האם יקבל ID?

        void AddDishOrder(BE.DishOrder newDishOrder);
        void DeleteDishOrder(int id);
        void DeleteDishOrder(BE.DishOrder item);
        void UpdateDishOrder(BE.DishOrder item);//האם יקבל ID?

        void AddClient(BE.Client newClient);
        void DeleteClient(int id);
        void DeleteClient(BE.Client item);
        void UpdateClient(BE.Client item);//האם יקבל ID?

        List<BE.Dish> getDishs();
        List<BE.Branch> getBranchs();
        List<BE.Order> getOrders();
        List<BE.Client> getClients();
        List<BE.DishOrder> getDishOrders();
    }
    class Dal_imp: Idal //להוסיף כשנגמור לממש את הכול שהיא יורשת מהאינטרפייס
    {
        static int IDCounter = 0;
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
            bool repeated=false;
            int id = setCounter(typeof(T));
            if (id >= 100000000)
                id = 1;
            while (ContainID(id, list) == true && !repeated)
            {
                if (id >= 100000000)
                {
                    id = 1;
                    repeated = true;
                }
                id++;
            }
            if (repeated && id >= 100000000)
                return -1;//ERROR
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

        int getCounter(Type obj)
        {
            if (obj == typeof(BE.Dish))
                return DS.DataSource.DishIDCounter;
            else if (obj == typeof(BE.Branch))
                return DS.DataSource.BranchIDCounter;
            else if (obj == typeof(BE.Client))
                return DS.DataSource.ClientIDCounter;
            else if (obj == typeof(BE.DishOrder))
                return DS.DataSource.DishOrderIDCounter;
            else if (obj == typeof(BE.Order))
                return DS.DataSource.OrderIDCounter;
            return -1;
        }
        int setCounter(Type obj)
        {
            if (obj == typeof(BE.Dish))
                return ++DS.DataSource.DishIDCounter;
            else if (obj == typeof(BE.Branch))
                return ++DS.DataSource.BranchIDCounter;
            else if (obj == typeof(BE.Client))
                return ++DS.DataSource.ClientIDCounter;
            else if (obj == typeof(BE.DishOrder))
                return ++DS.DataSource.DishOrderIDCounter;
            else if (obj == typeof(BE.Order))
                return ++DS.DataSource.OrderIDCounter;
            return -1;
        }
        public void AddDish(BE.Dish newDish)
        {
            Add(newDish, getDishs());
        }
        public void DeleteDish(int id)
        {
            Delete(id, getDishs());
        }
        public void DeleteDish(BE.Dish item)
        {
            DeleteDish(item.ID);
        }
        public void UpdateDish(BE.Dish item)
        {
            Update(item, getDishs());
        }

        public void AddBranch(BE.Branch newBranch)
        {
            Add(newBranch, getBranchs());
        }
        public void DeleteBranch(int id)
        {
            Delete(id, getBranchs());
        }
        public void DeleteBranch(BE.Branch item)
        {
            DeleteBranch(item.ID);
        }
        public void UpdateBranch(BE.Branch item)
        {
            Update(item, getBranchs());
        }
        public void AddOrder(BE.Order newOrder)
        {
            Add(newOrder, getOrders());
        }
        public void DeleteOrder(int id)
        {
            Delete(id, getOrders());
        }
        public void DeleteOrder(BE.Order item)
        {
            DeleteOrder(item.ID);
        }
        public void UpdateOrder(BE.Order item)
        {
            Update(item, getOrders());
        }
        public void AddDishOrder(BE.DishOrder newDishOrder)
        {
            Add(newDishOrder, getDishOrders());
        }
        public void DeleteDishOrder(int id)
        {
            Delete(id, getDishOrders());
        }
        public void DeleteDishOrder(BE.DishOrder item)
        {
            DeleteDishOrder(item.ID);
        }
        public void UpdateDishOrder(BE.DishOrder item)
        {
            Update(item, getDishOrders());
        }
        public void AddClient(BE.Client newClient)
        {
            Add(newClient, getClients());
        }
        public void DeleteClient(int id)
        {
            Delete(id, getClients());
        }
        public void DeleteClient(BE.Client item)
        {
            DeleteClient(item.ID);
        }
        public void UpdateClient(BE.Client item)
        {
            Update(item, getClients());
        }


        public List<BE.Dish> getDishs()
        {
            return DS.DataSource.DishList;
        }
        public List<BE.Branch> getBranchs()
        {
            return DS.DataSource.BranchList;
        }
        public List<BE.Order> getOrders()
        {
            return DS.DataSource.OrderList;
        }
        public List<BE.Client> getClients()
        {
            return DS.DataSource.ClientList;
        }
        public List<BE.DishOrder> getDishOrders()
        {
            return DS.DataSource.DishOrderList;
        }
    }
}
