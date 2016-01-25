using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Dish : InterID
    {
        public Dish() { }
        public Dish(string name, Size size, float price, Kashrut kosher, int id = 0)
        {
            ID = id;
            Name = name;
            Size = size;
            Price = price;
            Kosher = kosher;
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
        Size size;
        public Size Size
        {
            get { return size; }
            set { size = value; }
        }
        float price;
        public float Price
        {
            get { return price; }
            set { price = value; }
        }
        Kashrut kosher;
        public Kashrut Kosher
        {
            get { return kosher; }
            set { kosher = value; }
        }
        /// <summary>
        /// ממיר את המנה למחרוזת
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Dish " + Name + ":"
                 + "\n\tID: " + ID
                 + "\n\tName: " + Name
                 + "\n\tSize: " + Size
                 + "\n\tPrice: " + Price;
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
    public class Order : InterID
    {
        public Order() { }
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
        public Order(Branch branch, Client client, Kashrut kosher, int id = 0)
        {
            ID = id;
            BranchID = branch.ID;
            Address = client.Address;
            Date = DateTime.MinValue;
            Kosher = kosher;
            ClientID = client.ID;
        }
        public Order(Branch branch, Client client,DateTime date, Kashrut kosher, int id = 0)
        {
            ID = id;
            BranchID = branch.ID;
            Address = client.Address;
            Date = date;
            Kosher = kosher;
            ClientID = client.ID;
        }

        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        int branchID;
        public int BranchID
        {
            get { return branchID; }
            set { branchID = value; }
        }
        string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        Kashrut kosher;
        public Kashrut Kosher
        {
            get { return kosher; }
            set { kosher = value; }
        }
        int clientID;
        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }
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
                    + "\n\tDate: " + Date
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
    public class DishOrder : InterID
    {
        public DishOrder() { }
        public DishOrder(int orderID, int dishID, int dishAmount = 1, int id = 0)
        {
            ID = id;
            OrderID = orderID;
            DishID = dishID;
            DishAmount = dishAmount;
        }
        public DishOrder(Order order, Dish dish, int amout = 1, int id = 0)
        {
            OrderID = order.ID;
            DishID = dish.ID;
            DishAmount = amout;
            ID = id;

        }


        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        int orderID;
        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }
        int dishID;
        public int DishID
        {
            get { return dishID; }
            set { dishID = value; }
        }
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
    public class Branch : InterID
    {
                public Branch() { }
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
        public override string ToString()
        {
            return 
                "Username: " + UserName
                + "\nFull Name: " + Name
                  + "\nType: " + Type;
        }

    }
}
