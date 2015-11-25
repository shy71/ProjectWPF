﻿using System;
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
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        string name;
        public string Name
        {
            get { return Name; }
            set { Name = value; }
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
        public override string ToString()
        {
            return "Client number: " + ID
                +  "\nClient name: " + Name
                +  "\nClient address: " + Address
                +  "\nCredit card number: " + CreditCard;
        }
    }
    public class Order
    {
        int clientID;
        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
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
        Kashrut kosher;
        public Kashrut Kosher
        {
            get { return kosher; }
            set { kosher = value; }
        }
        string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        /// <summary>
        /// ממיר את ההזמנה למחרוזת
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Order ID: " + ID
                    + "\nDate: " + Date
                    + "\nBranch: " + BranchNum
                    + "\nKashrut: " + Kosher
                    + "\nClient ID: " + ClientID
                    + "\nOrder address: " + Address;
        }
    }
    /// <summary>
    /// מנה
    /// </summary>
    public class Dish
    {
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
        int size;
        public int Size
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
            return "Dish number: " + ID
                 + "\nDish name: " + Name
                 + "\nDish size: " + Size
                 + "\nDish price: " + Price;
        }
    }
    public class Branch
    {
        int number;
        public int Number
        {
            get { return number; }
            set { number = value; }
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
            return "Branch number: " + Number
                  + "\nBranch name: " + Name
                  + "\nBranch address: " + Address
                  + "\nPhone number: " + PhoneNumber
                  + "\nBranch bos: " + Boss
                  + "\nEmployee Count: " + EmployeeCount
                  + "\nAvilable messanger count: " + AvailableMessangers
                  + "\nBranch kashrut: " + Kosher;
        }
    }
    public class DishOrder
    {
        int orderID;
        public int OrderNum
        {
            get { return orderID; }
            set { orderID = value; }
        }
        int dishID;
        public int DishNum
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
            return "Order number: " + OrderNum
                 + "\nDish number: " + DishNum
                 + "\nAmount of dishes ordered: " + DishAmount;
        }
    }
}
