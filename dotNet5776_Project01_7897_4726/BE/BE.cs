using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// A class that represents a dish from the restaurant network
    /// </summary>
    public class Dish : InterID
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public Dish() { }
        /// <summary>
        /// Regular constructor.
        /// </summary>
        /// <param name="name">the dish name</param>
        /// <param name="size">the size of the dish</param>
        /// <param name="price">the price of the dish</param>
        /// <param name="kosher">the level of kashrut of the dish</param>
        /// <param name="id">the ID number for the dish in the database</param>
        /// <param name="active">a flag that represents the current state of the dish</param>
        public Dish(string name, Size size, float price, Kashrut kosher, int id = 0, bool active = true)
        {
            ID = id;
            Name = name;
            Size = size;
            Price = price;
            Kosher = kosher;
            Active = active;
        }
        /// <summary>
        /// the ID number of the dish
        /// </summary>
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// The Dish name (in some cases it also returns the state of the dish)
        /// </summary>
        string name;
        public string Name
        {
            get { return name+((name.Any(item=>item=='|'))?"":(((Active)?"":"|Unactive"))); }
            set { name = value; }
        }
        /// <summary>
        /// The dish size
        /// </summary>
        Size size;
        public Size Size
        {
            get { return size; }
            set { size = value; }
        }
        /// <summary>
        /// The dish price
        /// </summary>
        float price;
        public float Price
        {
            get { return price; }
            set { price = value; }
        }
        /// <summary>
        /// The dish kashrut level
        /// </summary>
        Kashrut kosher;
        public Kashrut Kosher
        {
            get { return kosher; }
            set { kosher = value; }
        }
        /// <summary>
        /// the dish activity state flag
        /// </summary>
        private bool active;

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        
        /// <summary>
        /// ToString function
        /// </summary>
        /// <returns>returns the dish as a string</returns>
        public override string ToString()
        {
            return "Dish " + Name
                 + "\n\tID: " + ID
                 + "\n\tName: " + name
                 + "\n\tSize: " + Size
                 + "\n\tPrice: " + Price
                 + "\n\tKashrut: " + kosher
            +"\n\tActive: " + Active;
        }
        /// <summary>
        /// Generates a unique ID based on the strings in the class
        /// </summary>
        /// <returns>The unique ID</returns>
        public int MakeID()
        {
            return Extensions.MakeID(Name, Size.ToString(), Price.ToString(), Kosher.ToString());
        }
    }
    /// <summary>
    /// a class that represents an order in the restaurant network
    /// </summary>
    public class Order : InterID
    {
        /// <summary>
        /// The order default constructor
        /// </summary>
        public Order() { }
        /// <summary>
        /// The regular constructor
        /// </summary>
        /// <param name="branchID">The ID of the branch ordered from</param>
        /// <param name="address">The address of the place where it's being sent to</param>
        /// <param name="kosher">The orders level of kashrut</param>
        /// <param name="clientID">The client who ordered</param>
        /// <param name="id">The order ID</param>
        public Order(int branchID, string address, Kashrut kosher, int clientID,int id = 0)
        {
            Delivered = false;
            ID = id;
            BranchID = branchID;
            Address = address;
            Date = DateTime.MinValue;
            Kosher = kosher;
            ClientID = clientID;
        }
        /// <summary>
        /// constructor by date
        /// </summary>
        /// <param name="branchID">The ID of the branch ordered from</param>
        /// <param name="address">The address of the place where it's being sent to</param>
        /// <param name="date">The date of the order</param>
        /// <param name="kosher">The order kashrut level</param>
        /// <param name="clientID">The client who ordered</param>
        /// <param name="id">the order ID</param>
        public Order(int branchID, string address,DateTime date, Kashrut kosher, int clientID, int id = 0)
        {
            Delivered = false;
            ID = id;
            BranchID = branchID;
            Address = address;
            Date = date;
            Kosher = kosher;
            ClientID = clientID;
        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="branch"></param>
        /// <param name="client"></param>
        /// <param name="kosher"></param>
        /// <param name="id"></param>
        public Order(Branch branch, Client client, Kashrut kosher, int id = 0)
        {
            ID = id;
            BranchID = branch.ID;
            Address = client.Address;
            Date = DateTime.MinValue;
            Kosher = kosher;
            ClientID = client.ID;
        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="branch"></param>
        /// <param name="client"></param>
        /// <param name="date"></param>
        /// <param name="kosher"></param>
        /// <param name="id"></param>
        public Order(Branch branch, Client client,DateTime date, Kashrut kosher, int id = 0)
        {
            ID = id;
            BranchID = branch.ID;
            Address = client.Address;
            Date = date;
            Kosher = kosher;
            ClientID = client.ID;
        }
        /// <summary>
        /// The order ID
        /// </summary>
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// The Branch of the order's ID
        /// </summary>
        int branchID;
        public int BranchID
        {
            get { return branchID; }
            set { branchID = value; }
        }
        /// <summary>
        /// The address where it's being sent to
        /// </summary>
        string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        /// <summary>
        /// The date it was sent
        /// </summary>
        DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        /// <summary>
        /// THe kashrut level of the order
        /// </summary>
        Kashrut kosher;
        public Kashrut Kosher
        {
            get { return kosher; }
            set { kosher = value; }
        }
        /// <summary>
        /// The client who ordered's ID
        /// </summary>
        int clientID;
        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }
        /// <summary>
        /// Flag representing the state of the order (delivered or not)
        /// </summary>
        bool delivered;
        public bool Delivered
        {
            get { return delivered; }
            set { delivered = value; }
        }


        /// <summary>
        /// ממיר את ההזמנה למחרוזת
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            return "Order ID: " + ID
                + "\n\tDate: " + ((Date == DateTime.MinValue) ? "Not sent" : Date.ToShortDateString() + " : " + Date.ToShortTimeString())
                    + "\n\tBranch: " + BranchID
                    + "\n\tKashrut: " + Kosher
                    + "\n\tClient ID: " + ClientID
                    + "\n\tOrder address: " + Address
                    + "\n\tDelivered Already?: " + Delivered;
        }
        /// <summary>
        /// Generates a unique ID based on the strings in the class
        /// </summary>
        /// <returns>The unique ID</returns>
        public int MakeID()
        {
            return Extensions.MakeID(BranchID.ToString(), Address, Date.ToString(), Kosher.ToString(), ClientID.ToString());
        }
    }
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
    /// <summary>
    /// A class that represnts a branch in the restaurant network
    /// </summary>
    public class Branch : InterID
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public Branch() { }
        /// <summary>
        /// regular constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="boss"></param>
        /// <param name="employeeCount"></param>
        /// <param name="availableMessangers"></param>
        /// <param name="kosher"></param>
        /// <param name="id"></param>
        public Branch(string name, string address, string phoneNumber, string boss, int employeeCount, int availableMessangers, Kashrut kosher, int id = 0)
        {
            ID = id;
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Boss = boss;
            EmployeeCount = employeeCount;
            AvailableMessangers = availableMessangers;
            Kosher = kosher;
        }
       
        /// <summary>
        /// The ID of the Branch
        /// </summary>
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// The branch's name
        /// </summary>
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        string boss;
        public string Boss
        {
            get { return boss; }
            set { boss = value; }
        }
        int employeeCount;
        public int EmployeeCount
        {
            get { return employeeCount; }
            set { employeeCount = value; }
        }
        int availableMessangers;
        public int AvailableMessangers
        {
            get { return availableMessangers; }
            set { availableMessangers = value; }
        }
        Kashrut kosher;
        public Kashrut Kosher
        {
            get { return kosher; }
            set { kosher = value; }
        }


        /// <summary>
        /// ממיר את הסניף למחרוזת
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Branch " + Name + ":"
                  + "\n\tID: " + ID
                  + "\n\tName: " + Name
                  + "\n\tAddress: " + Address
                  + "\n\tPhone number: " + PhoneNumber
                  + "\n\tBoss: " + Boss.Substring(0,Boss.IndexOf('@'))
                  + "\n\tEmployee Count: " + EmployeeCount
                  + "\n\tAvilable messanger count: " + AvailableMessangers
                  + "\n\tKashrut: " + Kosher;
        }
        /// <summary>
        /// Generates a unique ID based on the strings in the class
        /// </summary>
        /// <returns>The unique ID</returns>
        public int MakeID()
        {
            return Extensions.MakeID(Name, Address, PhoneNumber, Boss, EmployeeCount.ToString(), AvailableMessangers.ToString(), Kosher.ToString());
        }
    }
    public class Client : InterID
    {
        public Client() { }
        public Client(string name, string address, int creditCard, int age, int id = 0)
        {
            ID = id;
            Name = name;
            Address = address;
            CreditCard = creditCard;
            Age = age;
        }


        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        int creditCard;
        public int CreditCard
        {
            get { return creditCard; }
            set { creditCard = value; }
        }
        int age;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        /// <summary>
        /// changes it to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Client " + Name + ":"
                + "\n\tID: " + ID
                + "\n\tName: " + Name
                + "\n\tAddress: " + Address
                + "\n\tCredit card number: " + CreditCard
                + "\n\tAge: " + Age;
        }
        /// <summary>
        /// Generates a unique ID based on the strings in the class
        /// </summary>
        /// <returns>The unique ID</returns>
        public int MakeID()
        {
            return Extensions.MakeID(Name, Address, CreditCard.ToString(), Age.ToString());
        }
    }
    public class User
    {
        public User() { }
        public User(UserType type,string username,string password,string name,int itemID=0)
        {
            Type = type;
            UserName = username;
            Password = password;
            Name = name;
            if (type == UserType.NetworkManger && itemID != 0)
                throw new Exception("NetwokManger doesnt have an ID!");
            ItemID =itemID;

        }
        public UserType Type { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int ItemID{get;set;}
        /// <summary>
        /// changes it to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return 
                "Username: " + UserName
                + "\nFull Name: " + Name
                  + "\nType: " + Type;
        }

    }
}
