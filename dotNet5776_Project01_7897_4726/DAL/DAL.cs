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
        #region Dish Functions
        void AddDish(Dish newDish);
        void DeleteDish(int id);
        void DeleteDish(Dish item);
        void UpdateDish(Dish item);//האם יקבל ID?
        Dish GetDish(int id);
        IEnumerable<Dish> GetAllDishs(Func<Dish, bool> predicate = null);
        #endregion

        #region Branch Functions
        void AddBranch(Branch newBranch);
        void DeleteBranch(int id);
        void DeleteBranch(Branch item);
        void UpdateBranch(Branch item);//האם יקבל ID?
        Branch GetBranch(int id);
        IEnumerable<Branch> GetAllBranchs(Func<Branch, bool> predicate = null);
        #endregion

        #region Order Functions
        void AddOrder(Order newOrder);
        void DeleteOrder(int id);
        void DeleteOrder(Order item);
        void UpdateOrder(Order item);//האם יקבל ID?
        Order GetOrder(int id);
        IEnumerable<Order> GetAllOrders(Func<Order, bool> predicate = null);
        #endregion

        #region DishOrder Functions
        void AddDishOrder(DishOrder newDishOrder);
        void DeleteDishOrder(int id);
        void DeleteDishOrder(DishOrder item);
        void UpdateDishOrder(DishOrder item);//האם יקבל ID?
        DishOrder GetDishOrder(int id);
        IEnumerable<DishOrder> GetAllDishOrders(Func<DishOrder, bool> predicate = null);
        #endregion

        #region Client Functions
        void AddClient(Client newClient);
        void DeleteClient(int id);
        void DeleteClient(Client item);
        void UpdateClient(Client item);//האם יקבל ID?
        Client GetClient(int id);
        bool ContainID<T>(int id) where T : InterID;
        IEnumerable<Client> GetAllClients(Func<Client, bool> predicate = null);
        #endregion

    }
    class Dal_imp: Idal //להוסיף כשנגמור לממש את הכול שהיא יורשת מהאינטרפייס
    {
        Random rand=new Random();

        #region Generic Functions
        /// <summary>
        /// מוסיפה איבר לרשימה, יחד עם כל הבדיקות הנצרכות
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="newItem">האיבר שהפוקנציה תוסיף</param>
        /// <param name="list">הרשימה לה היא תוסיף אותה</param>
        void Add<T>(T newItem) where T : InterID
        {
            List<T> list = getList<T>() as List<T>;
            if (newItem.ID == 0 || ContainID<T>(newItem.ID))
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
            if (ContainID<T>(id) == false)
                throw new Exception("There isnt any item in the list with this id...");
            list.RemoveAt(list.FindIndex(item => item.ID == id));

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
        void Update<T>(T item) where T : InterID
        {
            List<T> list = getList<T>() as List<T>;
            if (ContainID<T>(item.ID) == false)
                throw new Exception("There isnt any item in the list with this id...");
            list.RemoveAt(list.FindIndex(var => var.ID == item.ID));
            list.Add(item);

        }
        T Get<T>(int id) where T : InterID
        {
            T res = (getList<T>() as List<T>).Find((item) => item.ID == id);
            if (res == null)
                throw new Exception("There isnt any item in the list with this id...");
            return res;

        }
        IEnumerable<T> GetAll<T>(Func<T, bool> predicate = null) where T : InterID
        {
            if (predicate == null)
                return (getList<T>() as List<T>).AsEnumerable();
            return from T item in getList<T>() as List<T>
                   where predicate(item)
                   select item;
        }
        /// <summary>
        /// מביאה תעודת זהות פנוייה באופן רנדומלי
        /// </summary>
        /// <typeparam name="T">סוג האיבר שצריך תעדות זהות</typeparam>
        /// <param name="list">הרשימה בה נמצאים שאר האיברים מסוג זה</param>
        /// <returns>מחזירה את תעדות הזהות הפנוייה</returns>
        int NextID<T>(T item) where T : InterID// לבדוק האם יש דרך יותר יעילה לעשות את זה
        {
            int original = item.MakeID(), result = original;
            List<T> list = getList<T>() as List<T>;
            while (ContainID<T>(result))
            {
                result++;
                if (result == original)
                    throw new Exception("there is to many items! we cant assign them an ID");
                else if (result > 99999999)
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
        public bool ContainID<T>(int id) where T : InterID
        {
            return (getList<T>() as List<T>).Any(item => item.ID == id);
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
        #endregion

        #region Dish Functions
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
        public Dish GetDish(int id)
        {
            return Get<Dish>(id);
        }
        public IEnumerable<Dish> GetAllDishs(Func<Dish, bool> predicate = null)
        {
            return GetAll(predicate);
        }
#endregion

        #region Branch Functions
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
        public Branch GetBranch(int id)
        {
            return Get<Branch>(id);
        }
        public IEnumerable<Branch> GetAllBranchs(Func<Branch, bool> predicate = null)
        {
            return GetAll(predicate);
        }
#endregion

        #region Order Functions
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
        public Order GetOrder(int id)
        {
            return Get<Order>(id);
        }
        public IEnumerable<Order> GetAllOrders(Func<Order, bool> predicate = null)
        {
            return GetAll(predicate);
        }
#endregion

        #region DishOrder Functions
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
        public DishOrder GetDishOrder(int id)
        {
            return Get<DishOrder>(id);
        }
        public IEnumerable<DishOrder> GetAllDishOrders(Func<DishOrder, bool> predicate = null)
        {
            return GetAll(predicate);
        }
#endregion

        #region Client Functions
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
        public Client GetClient(int id)
        {
            return Get<Client>(id);
        }
        public IEnumerable<Client> GetAllClients(Func<Client, bool> predicate = null)
        {
            return GetAll(predicate);
        }
#endregion
    }
}
