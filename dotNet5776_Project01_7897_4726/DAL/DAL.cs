using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class FactoryDal
    {
        public static Idal getDal()
        {
            return new Dal_imp();
        }

    }
    public interface Idal
    {
        void AddDish(Dish newDish);
        void DeleteDish(int id);
        void DeleteDish(Dish item);
        void UpdateDish(Dish item);//האם יקבל ID?
        Dish GetDish(int id);
        IEnumerable<Dish> GetAllDishs(Func<Dish, bool> predicate = null);

        void AddBranch(Branch newBranch);
        void DeleteBranch(int id);
        void DeleteBranch(Branch item);
        void UpdateBranch(Branch item);//האם יקבל ID?
        Branch GetBranch(int id);
        IEnumerable<Branch> GetAllBranchs(Func<Branch, bool> predicate = null);

        void AddOrder(Order newOrder);
        void DeleteOrder(int id);
        void DeleteOrder(Order item);
        void UpdateOrder(Order item);//האם יקבל ID?
        Order GetOrder(int id);
        IEnumerable<Order> GetAllOrders(Func<Order, bool> predicate = null);

        void AddDishOrder(DishOrder newDishOrder);
        void DeleteDishOrder(int id);
        void DeleteDishOrder(DishOrder item);
        void UpdateDishOrder(DishOrder item);//האם יקבל ID?
        DishOrder GetDishOrder(int id);
        IEnumerable<DishOrder> GetAllDishOrders(Func<DishOrder, bool> predicate = null);

        void AddClient(Client newClient);
        void DeleteClient(int id);
        void DeleteClient(Client item);
        void UpdateClient(Client item);//האם יקבל ID?
        Client GetClient(int id);
        IEnumerable<Client> GetAllClients(Func<Client, bool> predicate = null);

        T getByID<T>(int id) where T : InterID;
        List<DishOrder> getAllDishOrders(Order order);

    }
    class Dal_imp: Idal //להוסיף כשנגמור לממש את הכול שהיא יורשת מהאינטרפייס
    {
        Random rand=new Random();
        /// <summary>
        /// מוסיפה איבר לרשימה, יחד עם כל הבדיקות הנצרכות
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="newItem">האיבר שהפוקנציה תוסיף</param>
        /// <param name="list">הרשימה לה היא תוסיף אותה</param>
        void Add<T>(T newItem)where T : InterID
        {
            List<T> list = getList<T>() as List<T>;
            if (newItem.ID == 0 || ContainID(newItem.ID, list))
                newItem.ID = NextID(newItem);
            list.Add(newItem);
        }
        /// <summary>
        /// מוחקת איבר מהרשימה באמצעות תעודת הזהות
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="id">תעודת הזהות של האיבר אותו אנו מבקשים למחוק</param>
        /// <param name="list">הרשימה ממנה נמחוק אותו</param>
        void Delete<T>(int id) where T : InterID
        {
            List<T> list = getList<T>() as List<T>;
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
        void Delete<T>(T item) where T : InterID { Delete<T>(item.ID); }
        /// <summary>
        /// עדכון איבר מהרשימה באמצעות איבר מעודכן
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="item">האיבר המעודכן שבעזרת תעדות הזהות מסמן על האיבר שנעדכן</param>
        /// <param name="list">הרשימה בא נמצא האיבר אותו נעדכן</param>
        void Update<T>(T item)where T : InterID
        {
            List<T> list = getList<T>() as List<T>;
            if (ContainID(item.ID, list) == false)
                return; //ERROR
            list.RemoveAt(IndexByID(item.ID, list));
            list.Add(item);

        }
        T Get<T>(int id)where T: InterID
        {
            List<T> list = getList<T>() as List<T>;
            return list.Find((item)=>item.ID==id);
        }
        /// <summary>
        /// מביאה תעודת זהות פנוייה באופן רנדומלי
        /// </summary>
        /// <typeparam name="T">סוג האיבר שצריך תעדות זהות</typeparam>
        /// <param name="list">הרשימה בה נמצאים שאר האיברים מסוג זה</param>
        /// <returns>מחזירה את תעדות הזהות הפנוייה</returns>
        int NextID<T>(T item) where T : InterID// לבדוק האם יש דרך יותר יעילה לעשות את זה
        {
            int original = item.MakeID(),result = original;
            List<T> list = getList<T>() as List<T>;
            while (ContainID(result,list))
            {
                result++;
                if (result == original)
                    break; //ERROR
                else if(result>99999999)
                    result = 0;
            }
            return result;
        }
        /// <summary>
        /// בודקת האם קיים איבר ברשימה עם תעודת הזהות הזאת
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="id">תעדות הזהות</param>
        /// <param name="list">הרשימה בה נמצאים האיברים</param>
        /// <returns>מחזירה משתנה בוליאני המציין האם קיים איבר עם תעודת הזהות הזאת</returns>
        bool ContainID<T>(int id, List<T> list) where T : InterID 
        {
            return list.Any(item => item.ID == id); 
        }
        /// <summary>
        /// בודקת באזיה אינדקס נמצא האיבר בעל תעודת הזהות הזו
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="id">תעדות הזהות</param>
        /// <param name="list">הרשימה בה נממצאים האיברים </param>
        /// <returns>מחזירה אינדקס של מיקום האיבר</returns>
        int IndexByID<T>(int id, List<T> list) where T : InterID 
        {
            return list.FindIndex((item) => (item.ID == id));
        }

        public void AddDish(Dish newDish)
        {
            Add(newDish);
        }
        public void DeleteDish(int id)
        {
            Delete<Dish>(id);
        }
        public void DeleteDish(Dish item)
        {
            DeleteDish(item.ID);
        }
        public void UpdateDish(Dish item)
        {
            Update(item);
        }

        public void AddBranch(Branch newBranch)
        {
            Add(newBranch);
        }
        public void DeleteBranch(int id)
        {
            Delete<Branch>(id);
        }
        public void DeleteBranch(Branch item)
        {
            DeleteBranch(item.ID);
        }
        public void UpdateBranch(Branch item)
        {
            Update(item);
        }
        public void AddOrder(Order newOrder)
        {
            Add(newOrder);
        }
        public void DeleteOrder(int id)
        {
            Delete<Order>(id);
        }
        public void DeleteOrder(Order item)
        {
            DeleteOrder(item.ID);
        }
        public void UpdateOrder(Order item)
        {
            Update(item);
        }
        public void AddDishOrder(DishOrder newDishOrder)
        {
            Add(newDishOrder);
        }
        public void DeleteDishOrder(int id)
        {
            Delete<DishOrder>(id);
        }
        public void DeleteDishOrder(DishOrder item)
        {
            DeleteDishOrder(item.ID);
        }
        public void UpdateDishOrder(DishOrder item)
        {
            Update(item);
        }
        public void AddClient(Client newClient)
        {
            Add(newClient);
        }
        public void DeleteClient(int id)
        {
            Delete<Client>(id);
        }
        public void DeleteClient(Client item)
        {
            DeleteClient(item.ID);
        }
        public void UpdateClient(Client item)
        {
            Update(item);
        }

        object getList<T>()
        {
            if (typeof(T) == typeof(Dish))
                return DS.DataSource.DishList;
            if (typeof(T) == typeof(Branch))
                return DS.DataSource.BranchList;
            if (typeof(T) == typeof(Client))
                return DS.DataSource.ClientList;
            if (typeof(T) == typeof(DishOrder))
                return DS.DataSource.DishOrderList;
            if (typeof(T) == typeof(Order))
                return DS.DataSource.OrderList;
            return null;

        }
        public T getByID<T>(int id)  where T : InterID
        {
            return (getList<T>() as List<T>).Find((item) => (item.ID == id));
        }
        public List<DishOrder> getAllDishOrders(Order order)
        {
            return (getList<DishOrder>() as List<DishOrder>).FindAll((item) => (item.OrderID == order.ID));
        }

    }
}
