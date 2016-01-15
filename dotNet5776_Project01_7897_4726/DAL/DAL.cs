using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using BE;

namespace DAL
{
    public class FactoryDal
    {
        private static Idal instance;
        /// <summary>
        /// Function to Get the DAL
        /// </summary>
        /// <returns></returns>
        public static Idal getDal()
        {
            if (instance == null)
                instance = new Dal_imp();
            return instance;
        }
    }

    public interface Idal
    {
        #region Dish Functions
        /// <summary>
        /// Adds a dish
        /// </summary>
        /// <param name="newDish"></param>
        void AddDish(Dish newDish);
        /// <summary>
        /// Deletes a dish
        /// </summary>
        /// <param name="id">the id of the dish</param>
        void DeleteDish(int id);
        /// <summary>
        /// deletes a dish
        /// </summary>
        /// <param name="item">the dish item</param>
        void DeleteDish(Dish item);
        /// <summary>
        /// updates a certain dish
        /// </summary>
        /// <param name="item"></param>
        void UpdateDish(Dish item);
        /// <summary>
        /// Finds a dish from the DataBase by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dish GetDish(int id);
        /// <summary>
        /// Gets all dishes that pass the predicate test (if there is one)
        /// </summary>
        /// <param name="predicate">a test for the dishes</param>
        /// <returns>An Enumerable of all of the Dishs that pass the predicate</returns>
        IEnumerable<Dish> GetAllDishs(Func<Dish, bool> predicate = null);
        #endregion

        #region Branch Functions
        /// <summary>
        /// adds a branch to the system
        /// </summary>
        /// <param name="newBranch"></param>
        void AddBranch(Branch newBranch);
        /// <summary>
        /// deletes a branch
        /// </summary>
        /// <param name="id">the branch ID</param>
        void DeleteBranch(int id);
        /// <summary>
        /// deletes a branch
        /// </summary>
        /// <param name="item"></param>
        void DeleteBranch(Branch item);
        /// <summary>
        /// updates a branch
        /// </summary>
        /// <param name="item"></param>
        void UpdateBranch(Branch item);
        /// <summary>
        /// gets a specific branch by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Branch GetBranch(int id);
        /// <summary>
        /// gets all branches that pass the predicate test (if there is one)
        /// </summary>
        /// <param name="predicate">the predicate test</param>
        /// <returns>An Enumerable of all of the Branchs that pass the predicate</returns>
        IEnumerable<Branch> GetAllBranchs(Func<Branch, bool> predicate = null);
        #endregion

        #region Order Functions
        /// <summary>
        /// adds an order
        /// </summary>
        /// <param name="newOrder"></param>
        void AddOrder(Order newOrder);
        /// <summary>
        /// deletes an order by it's id
        /// </summary>
        /// <param name="id"></param>
        void DeleteOrder(int id);
        /// <summary>
        /// Changes status of the order to delivered
        /// </summary>
        /// <param name="id"></param>
        void DeliveredOrder(int id);
        /// <summary>
        /// deletes an order
        /// </summary>
        /// <param name="item"></param>
        void DeleteOrder(Order item);
        /// <summary>
        /// updates an order
        /// </summary>
        /// <param name="item"></param>
        void UpdateOrder(Order item);
        /// <summary>
        /// Finds an order in the DataBase by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Order GetOrder(int id);
        /// <summary>
        /// Gets all orders that pass the predicate test (if there is one).
        /// </summary>
        /// <param name="predicate">the predicate test</param>
        /// <returns>An Enumerable of all of the Orders that pass the predicate</returns>
        IEnumerable<Order> GetAllOrders(Func<Order, bool> predicate = null);
        #endregion

        #region DishOrder Functions
        /// <summary>
        /// adds a dish-order (as a link)
        /// </summary>
        /// <param name="newDishOrder"></param>
        void AddDishOrder(DishOrder newDishOrder);
        /// <summary>
        /// deletes a dish-order by ID
        /// </summary>
        /// <param name="id"></param>
        void DeleteDishOrder(int id);
        /// <summary>
        /// deletes a dish-order
        /// </summary>
        /// <param name="item"></param>
        void DeleteDishOrder(DishOrder item);
        /// <summary>
        /// updates a dish-order
        /// </summary>
        /// <param name="item"></param>
        void UpdateDishOrder(DishOrder item);
        /// <summary>
        /// gets a dish-order by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DishOrder GetDishOrder(int id);
        /// <summary>
        /// gets all dish-orders that pass the predicate test (if there is one)
        /// </summary>
        /// <param name="predicate">the predicate test</param>
        /// <returns>An Enumerable of all of the DishOrders that pass the predicate</returns>
        IEnumerable<DishOrder> GetAllDishOrders(Func<DishOrder, bool> predicate = null);
        #endregion

        #region Client Functions

        /// <summary>
        /// adds a client
        /// </summary>
        /// <param name="newClient"></param>
        void AddClient(Client newClient);
        /// <summary>
        /// deletes a client by ID
        /// </summary>
        /// <param name="id"></param>
        void DeleteClient(int id);
        /// <summary>
        /// deletes a client
        /// </summary>
        /// <param name="item"></param>
        void DeleteClient(Client item);
        /// <summary>
        /// updates a client
        /// </summary>
        /// <param name="item"></param>
        void UpdateClient(Client item);
        /// <summary>
        /// gets a client from the DataBase by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Client GetClient(int id);
        /// <summary>
        /// gets all clients that pass the predicate test (if there is one)
        /// </summary>
        /// <param name="predicate">the predicate test</param>
        /// <returns>An Enumerable of all of the clients that pass the predicate </returns>
        IEnumerable<Client> GetAllClients(Func<Client, bool> predicate = null);

        #endregion


        /// <summary>
        /// checks if there is a specific ID in the T list in the DataBase
        /// </summary>
        /// <typeparam name="T">The type of item you are looking to see if he exist</typeparam>
        /// <param name="id">The id of the item</param>
        /// <returns>True: there is an item with this ID , False: There isnt</returns>
        bool ContainID<T>(int id) where T : InterID;

        //New
        #region User Functions

        /// <summary>
        /// adds a User
        /// </summary>
        /// <param name="newUser"></param>
        void AddUser(User newUser);
        /// <summary>
        /// deletes a User by username
        /// </summary>
        /// <param name="id"></param>
        void DeleteUser(string username);
        /// <summary>
        /// deletes a User
        /// </summary>
        /// <param name="item"></param>
        void DeleteUser(User item);
        /// <summary>
        /// updates a User 
        /// </summary>
        /// <param name="item"></param>
        void UpdateUser(User item);
        /// <summary>
        /// gets a User from the DataBase by its Username
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUser(string username);
        /// <summary>
        /// gets all Users that pass the predicate test (if there is one)
        /// </summary>
        /// <param name="predicate">the predicate test</param>
        /// <returns>An Enumerable of all of the Users that pass the predicate </returns>
        IEnumerable<User> GetAllUsers(Func<User, bool> predicate = null);
        #endregion 

    }
    class XmlSample
    {
        XElement fileRoot;
        public XElement FileRoot
        { get { return fileRoot; } set { fileRoot = value; } }
        string fPath, name;
        public string FPath
        { get { return fPath; } set { fPath = value; } }
        public string Name
        { get { return name; } set { name = value; } }
        public XmlSample(string path,string name)
        {
            FPath = path;
            if (!File.Exists(path))
                CreateFile();
            else
                LoadFile();
        }
        public void LoadFile()
        {
            try
            {
                fileRoot = XElement.Load(fPath);
            }
            catch
            {
                throw new Exception("File upload problem.");
            }
        }
        void CreateFile()
        {
            fileRoot = new XElement(name);
            fileRoot.Save(fPath);
        }
        public void Save()
        {
            fileRoot.Save(fPath);
        }
        //T Get<T>(int ID)where T:InterID
        //{
        //    try
        //    {
        //        //List<T> Tlist=(from p in fileRoot.Elements()
        //        //               where Convert.ToInt32(p.Element("id").Value) == ID
        //        //               select /*copy item into Tlist*/p).ToList();
        //        /*foreach(var item in fileRoot.Elements())
        //        {
        //            foreach(var p in res.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
        //            {
        //                if(Convert.ToInt32(item.Element("id").Value) == ID)
        //                {
        //                    //copy the item into res
        //                }
        //            }
        //        }*/
        //    }
        //    catch
        //    {
        //        throw new Exception("Problem getting the item");
        //    }
        //}
    }
    class Dal_XML_imp
    {
        Random rand = new Random();
        XmlSample xmlDish = new XmlSample(@"XmlFiles\DishXml.xml", "Dishes"), 
                  xmlBranch = new XmlSample(@"XmlFiles\BranchXml.xml", "Branches"), 
                  xmlOrder = new XmlSample(@"XmlFiles\OrderXml.xml", "Order"),
                  xmlDishOrder = new XmlSample(@"XmlFiles\DishOrderXml.xml", "DishOrders"), 
                  xmlClient = new XmlSample(@"XmlFiles\ClientXml.xml", "Clients"),
                  xmlUser = new XmlSample(@"XmlFiles\UserXml.xml", "Users");
        #region Generic Functions
        /// <summary>
        /// מוסיפה איבר לרשימה, יחד עם כל הבדיקות הנצרכות
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="newItem">האיבר שהפוקנציה תוסיף</param>
        /// <param name="list">הרשימה לה היא תוסיף אותה</param>
        void Add<T>(T newItem, XmlSample file) where T : InterID
        {
            if (newItem.ID < 0 || newItem.ID >= 100000000)
                throw new Exception("The ID must be a positive number with at most 8 digits");
            file.LoadFile();
            if (ContainID(newItem.ID,file) || newItem.ID == 0)
                newItem.ID = NextID(newItem,file);
            file.FileRoot.Add(new XElement(newItem.GetType().ToString(), 
                                           from item in newItem.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                           select new XElement(item.Name, item.GetValue(newItem))));
            file.Save();
        }
        /// <summary>
        /// מוחקת איבר מהרשימה באמצעות תעודת הזהות
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="id">תעודת הזהות של האיבר אותו אנו מבקשים למחוק</param>
        /// <param name="list">הרשימה ממנה נמחוק אותו</param>
        void Delete(int id,XmlSample file)
        {
            file.LoadFile();
            if (!ContainID(id,file))
                throw new Exception("There isnt any item in the list with this id...");
            try
            {
                XElement TElement = (from p in file.FileRoot.Elements()
                                     where Convert.ToInt32(p.Element("ID").Value) == id
                                     select p).FirstOrDefault();
                TElement.Remove();
                file.Save();
            }
            catch
            {
                throw new Exception("Problem deleting the item.");
            }

        }
        /// <summary>
        /// מוחקת איבר מהרשימה באמצעות האיבר עצמו
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="item">האיבר אותו אנו מבקשים למחוק</param>
        /// <param name="list">הרשימה ממנה נמחק אותו</param>
        void Delete<T>(T item, XmlSample file) where T:InterID { Delete(item.ID,file); }
        /// <summary>
        /// עדכון איבר מהרשימה באמצעות איבר מעודכן
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="item">האיבר המעודכן שבעזרת תעדות הזהות מסמן על האיבר שנעדכן</param>
        /// <param name="list">הרשימה בא נמצא האיבר אותו נעדכן</param>
        void Update<T>(T item,XmlSample file) where T : InterID
        {
            file.LoadFile();
            if (!ContainID(item.ID,file))
                throw new Exception("There isnt any item in the list with this id...");
            Delete(item, file);
            Add(item, file);
        }
        /// <summary>
        /// Get an item by his ID
        /// </summary>
        /// <typeparam name="T">The type of the item you want to get</typeparam>
        /// <param name="id">The ID of the item</param>
        /// <returns>The item that matchs this ID</returns>
        T Get<T>(int id,XmlSample file) where T : InterID,new()
        {
            file.LoadFile();
            if (!ContainID(id, file))
                throw new Exception("There isnt any item in the datdbase with this id...");
            try
            {
                T res = new T();
                var s = (from p in file.FileRoot.Elements()
                         where Convert.ToInt32(p.Element("ID").Value) == id
                         select p).FirstOrDefault();
                foreach (var item in res.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    item.SetValue(res, s.Element(item.Name));
                }
                return res;
            }
            catch
            {
                throw new Exception("Failed to load item.");
            }

        }
        /// <summary>
        /// Get all the item that matches the predicate function
        /// </summary>
        /// <typeparam name="T">The type of items you want to get</typeparam>
        /// <param name="predicate">The function that will chekc if you want them or not</param>
        /// <returns>The list of all of the item that matches the predicate function</returns>
        IEnumerable<T> GetAll<T>(XmlSample file, Func<T, bool> predicate = null) where T : InterID , new()
        {
            
        }
        /// <summary>
        /// מביאה תעודת זהות פנוייה באופן רנדומלי
        /// </summary>
        /// <typeparam name="T">סוג האיבר שצריך תעדות זהות</typeparam>
        /// <param name="list">הרשימה בה נמצאים שאר האיברים מסוג זה</param>
        /// <returns>מחזירה את תעדות הזהות הפנוייה</returns>
        int NextID<T>(T item,XmlSample file) where T : InterID
        {
            int original = item.MakeID(), result = original;
            while (ContainID(result,file))
            {
                result++;
                if (result == original)
                    throw new Exception("there is to many items! we cant assign them an ID");
                result %= 100000000;
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
        public bool ContainID(int id,XmlSample file)
        {
            return (from p in file.FileRoot.Elements()
                    where Convert.ToInt32(p.Element("ID").Value) == id
                    select p).ToList().Count > 0;
        }
        List<T> getList<T>()
        {
            if (typeof(T) == typeof(Dish))
                return DS.DataSource.DishList as List<T>;
            if (typeof(T) == typeof(Branch))
                return DS.DataSource.BranchList as List<T>;
            if (typeof(T) == typeof(Client))
                return DS.DataSource.ClientList as List<T>;
            if (typeof(T) == typeof(DishOrder))
                return DS.DataSource.DishOrderList as List<T>;
            if (typeof(T) == typeof(Order))
                return DS.DataSource.OrderList as List<T>;
            if (typeof(T) == typeof(User))
                return DS.DataSource.UserList as List<T>;
            return null;

        }
        #endregion

        #region Dish Functions
        public void AddDish(Dish newDish)
        {
            Add(newDish,xmlDish);
        }
        public void DeleteDish(int id)
        {
            Delete(id,xmlDish);
        }
        public void DeleteDish(Dish item)
        {
            DeleteDish(item.ID);
        }
        public void UpdateDish(Dish item)
        {
            Update(item,xmlDish);
        }
        public Dish GetDish(int id)
        {
            
        }
        public IEnumerable<Dish> GetAllDishs(Func<Dish, bool> predicate = null)
        {
            
        }
        #endregion

        #region Branch Functions
        public void AddBranch(Branch newBranch)
        {
            Add(newBranch, xmlBranch);
        }
        public void DeleteBranch(int id)
        {
            Delete(id,xmlBranch);
        }
        public void DeleteBranch(Branch item)
        {
            DeleteBranch(item.ID);
        }
        public void UpdateBranch(Branch item)
        {
            Update(item,xmlBranch);
        }
        public Branch GetBranch(int id)
        {
            
        }
        public IEnumerable<Branch> GetAllBranchs(Func<Branch, bool> predicate = null)
        {
            return GetAll(predicate);
        }
        #endregion

        #region Order Functions
        public void DeliveredOrder(int id)
        {
            GetOrder(id).Delivered = true;
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

        //New

        #region User Functions

        /// <summary>
        /// adds a User
        /// </summary>
        /// <param name="newUser"></param>
        public void AddUser(User newUser)
        {
            if (getList<User>().Any(item => item.UserName == newUser.UserName))
                throw new Exception("there is already a user with that username");
            getList<User>().Add(newUser);

        }
        /// <summary>
        /// deletes a User by username
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUser(string username)
        {
            if (getList<User>().Any(item => item.UserName == username))
                getList<User>().RemoveAll(item => item.UserName == username);
            else
                throw new Exception("There isnt any user with that username in the database");
        }
        /// <summary>
        /// deletes a User
        /// </summary>
        /// <param name="item"></param>
        public void DeleteUser(User item)
        {
            DeleteUser(item.UserName);
        }
        /// <summary>
        /// updates a User 
        /// </summary>
        /// <param name="item"></param>
        public void UpdateUser(User item)
        {
            DeleteUser(item.UserName);
            UpdateUser(item);
        }
        /// <summary>
        /// gets a User from the DataBase by its Username
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUser(string username)
        {
            return getList<User>().FirstOrDefault(item => item.UserName == username);
        }
        /// <summary>
        /// gets all Users that pass the predicate test (if there is one)
        /// </summary>
        /// <param name="predicate">the predicate test</param>
        /// <returns>An Enumerable of all of the Users that pass the predicate </returns>
        public IEnumerable<User> GetAllUsers(Func<User, bool> predicate = null)
        {
            if (predicate == null)
                return getList<User>().AsEnumerable();
            return from User item in getList<User>()
                   where predicate(item)
                   select item;
        }
        #endregion
    }
    class Dal_imp : Idal
    {
        Random rand = new Random();


        #region Generic Functions
        /// <summary>
        /// מוסיפה איבר לרשימה, יחד עם כל הבדיקות הנצרכות
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="newItem">האיבר שהפוקנציה תוסיף</param>
        /// <param name="list">הרשימה לה היא תוסיף אותה</param>
        void Add<T>(T newItem) where T : InterID
        {
            if (newItem.ID < 0 || newItem.ID >= 100000000)
                throw new Exception("The ID must be a positi ve number with at most 8 digits");

            if (ContainID<T>(newItem.ID) || newItem.ID == 0)
                newItem.ID = NextID(newItem);
            getList<T>().Add(newItem);
        }
        /// <summary>
        /// מוחקת איבר מהרשימה באמצעות תעודת הזהות
        /// </summary>
        /// <typeparam name="T">סוג האיבר</typeparam>
        /// <param name="id">תעודת הזהות של האיבר אותו אנו מבקשים למחוק</param>
        /// <param name="list">הרשימה ממנה נמחוק אותו</param>
        void Delete<T>(int id) where T : InterID
        {
            List<T> list = getList<T>();
            if (ContainID<T>(id) == false)
                throw new Exception("There isnt any item in the list with this id...");
            list.Remove(list.FirstOrDefault(item => item.ID == id));

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
            List<T> list = getList<T>();
            if (ContainID<T>(item.ID) == false)
                throw new Exception("There isnt any item in the list with this id...");
            list.Remove(list.FirstOrDefault(var => var.ID == item.ID));
            list.Add(item);

        }
        /// <summary>
        /// Get an item by his ID
        /// </summary>
        /// <typeparam name="T">The type of the item you want to get</typeparam>
        /// <param name="id">The ID of the item</param>
        /// <returns>The item that matchs this ID</returns>
        T Get<T>(int id) where T : InterID
        {
            T res = getList<T>().FirstOrDefault(item => item.ID == id);
            if (res == null)
                throw new Exception("There isnt any item in the list with this id...");
            return res;

        }
        /// <summary>
        /// Get all the item that matches the predicate function
        /// </summary>
        /// <typeparam name="T">The type of items you want to get</typeparam>
        /// <param name="predicate">The function that will chekc if you want them or not</param>
        /// <returns>The list of all of the item that matches the predicate function</returns>
        IEnumerable<T> GetAll<T>(Func<T, bool> predicate = null) where T : InterID
        {
            if (predicate == null)
                return getList<T>().AsEnumerable();
            return from T item in getList<T>()
                   where predicate(item)
                   select item;
        }
        /// <summary>
        /// מביאה תעודת זהות פנוייה באופן רנדומלי
        /// </summary>
        /// <typeparam name="T">סוג האיבר שצריך תעדות זהות</typeparam>
        /// <param name="list">הרשימה בה נמצאים שאר האיברים מסוג זה</param>
        /// <returns>מחזירה את תעדות הזהות הפנוייה</returns>
        int NextID<T>(T item) where T : InterID
        {
            int original = item.MakeID(), result = original;
            while (ContainID<T>(result))
            {
                result++;
                if (result == original)
                    throw new Exception("there is to many items! we cant assign them an ID");
                result %= 100000000;
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
            return getList<T>().Any(item => item.ID == id);
        }
        List<T> getList<T>()
        {
            if (typeof(T) == typeof(Dish))
                return DS.DataSource.DishList as List<T>;
            if (typeof(T) == typeof(Branch))
                return DS.DataSource.BranchList as List<T>;
            if (typeof(T) == typeof(Client))
                return DS.DataSource.ClientList as List<T>;
            if (typeof(T) == typeof(DishOrder))
                return DS.DataSource.DishOrderList as List<T>;
            if (typeof(T) == typeof(Order))
                return DS.DataSource.OrderList as List<T>;
            if (typeof(T) == typeof(User))
                return DS.DataSource.UserList as List<T>;
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
        public void DeliveredOrder(int id)
        {
            GetOrder(id).Delivered = true;
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

        //New

        #region User Functions

        /// <summary>
        /// adds a User
        /// </summary>
        /// <param name="newUser"></param>
        public void AddUser(User newUser)
        {
            if (getList<User>().Any(item => item.UserName == newUser.UserName))
                throw new Exception("there is already a user with that username");
            getList<User>().Add(newUser);

        }
        /// <summary>
        /// deletes a User by username
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUser(string username)
        {
            if (getList<User>().Any(item => item.UserName == username))
                getList<User>().RemoveAll(item => item.UserName == username);
            else
                throw new Exception("There isnt any user with that username in the database");
        }
        /// <summary>
        /// deletes a User
        /// </summary>
        /// <param name="item"></param>
        public void DeleteUser(User item)
        {
            DeleteUser(item.UserName);
        }
        /// <summary>
        /// updates a User 
        /// </summary>
        /// <param name="item"></param>
        public void UpdateUser(User item)
        {
            DeleteUser(item.UserName);
            UpdateUser(item);
        }
        /// <summary>
        /// gets a User from the DataBase by its Username
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUser(string username)
        {
            return getList<User>().FirstOrDefault(item => item.UserName == username);
        }
        /// <summary>
        /// gets all Users that pass the predicate test (if there is one)
        /// </summary>
        /// <param name="predicate">the predicate test</param>
        /// <returns>An Enumerable of all of the Users that pass the predicate </returns>
        public IEnumerable<User> GetAllUsers(Func<User, bool> predicate = null)
        {
            if (predicate == null)
                return getList<User>().AsEnumerable();
            return from User item in getList<User>()
                   where predicate(item)
                   select item;
        }
        #endregion 
    }
}
