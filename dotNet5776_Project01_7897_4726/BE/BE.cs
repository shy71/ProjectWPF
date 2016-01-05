using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * להוסיף לפקופטי מגבלה

 * */
namespace BE
{
    public interface InterID
    {
        int ID { get; set; }
        int MakeID();//צריך להוסיף את המימוש ביורשים!
    }
    public class Dish : InterID
    {
        public Dish()
        { }
        public Dish(string name,Size size,float price,Kashrut kosher,int id = 0)
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
        public int MakeID()
        {
            return Extensions.MakeID(Name, Size.ToString(), Price.ToString(), Kosher.ToString());
        }
    }
    public class Order : InterID
    {//לבדוק שהכתובת עובדת טוב
        public Order()
        { }
        public Order(int branchID, string address, DateTime date, Kashrut kosher, int clientID, int id = 0)
        {
            ID = id;
            BranchID=branchID;
            Address=address;
            Date=date;
            Kosher = kosher;
            ClientID=clientID;
        }
        //אפשרות שליחה ב-5 דקות
        bool fastOrder;
        public bool FastOrder
        {
            get { return fastOrder; }
            set { fastOrder = value; }
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
                    + "\n\tOrder address: " + Address;
        }
        public int MakeID()
        {
            return Extensions.MakeID(BranchID.ToString(),Address,Date.ToString(),Kosher.ToString(),ClientID.ToString());
        }
    }
    public class DishOrder : InterID
    {
        public DishOrder()
        { }
        public DishOrder(int orderID,int dishID,int dishAmount, int id = 0)
        {
            ID = id;
            OrderID = orderID;
            DishID = dishID;
            DishAmount = dishAmount;
        }
        public DishOrder(Dish dish,Order order,int amout=1,int id=0)
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
            return "Dish In Order "+ "ID: "+ID 
                 +"\n\tOrder number: " + OrderID
                 + "\n\tDish number: " + DishID
                 + "\n\tAmount of dishes ordered: " + DishAmount;
        }
        public int MakeID()
        {
            return Extensions.MakeID(OrderID.ToString(), DishID.ToString(), DishAmount.ToString());
        }
    }
    public class Branch : InterID
    {
        public Branch()
        { }
        public Branch(string name, string address, string phoneNumber,string boss,int employeeCount,int availableMessangers,Kashrut kosher, int id = 0)
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
            return "Branch "+Name+":"
                  +"\n\tID: " + ID
                  + "\n\tName: " + Name
                  + "\n\tAddress: " + Address
                  + "\n\tPhone number: " + PhoneNumber
                  + "\n\tBoss: " + Boss
                  + "\n\tEmployee Count: " + EmployeeCount
                  + "\n\tAvilable messanger count: " + AvailableMessangers
                  + "\n\tKashrut: " + Kosher; 
        }
        public int MakeID()
        {
            return Extensions.MakeID(Name, Address, PhoneNumber, Boss, EmployeeCount.ToString(), AvailableMessangers.ToString(), Kosher.ToString());
        }
    }
    public class Client : InterID
    {
        public Client()
        { }
        public Client(string name, string address, int creditCard,int age, int id = 0)
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
            return "Client "+Name+":"
                + "\n\tID: " + ID
                + "\n\tName: " + Name
                + "\n\tAddress: " + Address
                + "\n\tCredit card number: " + CreditCard
                + "\n\tAge: " + Age;
        }
        public int MakeID()
        {
            return Extensions.MakeID(Name, Address, CreditCard.ToString(),Age.ToString());
        }
    }
    
}
