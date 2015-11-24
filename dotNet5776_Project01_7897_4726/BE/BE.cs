using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * מוסכמות לגבי פרופטי
 * שמות המשתנים
 * NUM->ID
 * */
namespace BE
{
    public enum Kashrut
    {
        LOW, MEDIUM, HIGH
    }
    public class Client
    {
        int clientNumber;
        public int ClientNumber
        {
            get { return clientNumber; }
            set { clientNumber = value; }
        }
        string clientName;
        public string ClientName
        {
            get { return ClientName; }
            set { ClientName = value; }
        }
        string clientAddress;
        public string ClientAddress
        {
            get { return clientAddress; }
            set { clientAddress = value; }
        }
        int creditCard;
        public int CreditCard
        {
            get { return creditCard; }
            set { creditCard = value; }
        }
        public override string ToString()
        {
            return "Client number: " + ClientNumber
                +  "\nClient name: " + ClientName
                +  "\nClient address: " + ClientAddress
                +  "\nCredit card number: " + CreditCard;
        }
    }
    public class Order
    {
        int clientNum;
        public int ClientNum
        {
            get { return clientNum; }
            set { clientNum = value; }
        }
        int orderNum;
        public int OrderNum
        {
            get { return orderNum; }
            set { orderNum = value; }
        }
        DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        int branchNum;
        public int BranchNum
        {
            get { return branchNum; }
            set { branchNum = value; }
        }
        Kashrut orderKashrut;
        public Kashrut OrderKashrut
        {
            get { return orderKashrut; }
            set { orderKashrut = value; }
        }
        
        string orderAddress;
        public string OrderAddress
        {
            get { return orderAddress; }
            set { orderAddress = value; }
        }
        /// <summary>
        /// ממיר את ההזמנה למחרוזת
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Order number: " + OrderAddress
                    + "\nDate: " + Date
                    + "\nBranch: " + BranchNum
                    + "\nKashrut: " + OrderKashrut
                    + "\nClient number: " + ClientNum
                    + "\nOrder address: " + OrderAddress;
        }
    }
    /// <summary>
    /// מנה
    /// </summary>
    public class Dish
    {
        int dishNum;
        public int DishNum
        {
            get { return dishNum; }
            set { dishNum = value; }
        }
        string dishName;
        public string DishName
        {
            get { return dishName; }
            set { dishName = value; }
        }
        int dishSize;
        public int DishSize
        {
            get { return dishSize; }
            set { dishSize = value; }
        }
        float dishPrice;
        public float DishPrice
        {
            get { return dishPrice; }
            set { dishPrice = value; }
        }
        Kashrut dishKashrut;
        public Kashrut DishKashrut
        {
            get { return dishKashrut; }
            set { dishKashrut = value; }
        }
        /// <summary>
        /// ממיר את המנה למחרוזת
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Dish number: " + DishNum
                 + "\nDish name: " + DishName
                 + "\nDish size: " + DishSize
                 + "\nDish price: " + DishPrice;
        }
    }
    public class Branch
    {
        int branchNum;
        public int BranchNum
        {
            get { return branchNum; }
            set { branchNum = value; }
        }
        string branchName;
        public string BranchName
        {
            get { return branchName; }
            set { branchName = value; }
        }
        string branchAddress;
        public string BranchAddress
        {
            get { return branchAddress; }
            set { branchAddress = value; }
        }
        string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        string branchBoss;
        public string BranchBoss
        {
            get { return branchBoss; }
            set { branchBoss = value; }
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
        Kashrut branchKashrut;
        public Kashrut BranchKashrut
        {
            get { return branchKashrut; }
            set { branchKashrut = value; }
        }
        /// <summary>
        /// ממיר את הסניף למחרוזת
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Branch number: " + BranchNum
                  + "\nBranch name: " + BranchName
                  + "\nBranch address: " + BranchAddress
                  + "\nPhone number: " + PhoneNumber
                  + "\nBranch bos: " + BranchBoss
                  + "\nEmployee Count: " + EmployeeCount
                  + "\nAvilable messanger count: " + AvailableMessangers
                  + "\nBranch kashrut: " + BranchKashrut;
        }
    }
    public class OrderDish
    {
        int orderNum;
        public int OrderNum
        {
            get { return orderNum; }
            set { orderNum = value; }
        }
        int dishNum;
        public int DishNum
        {
            get { return dishNum; }
            set { dishNum = value; }
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
            return "Order number: " + OrderNum
                 + "\nDish number: " + DishNum
                 + "\nAmount of dishes ordered: " + DishAmount;
        }
    }
}
