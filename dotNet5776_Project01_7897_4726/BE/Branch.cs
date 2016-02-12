using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
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
                  + "\n\tBoss: " + Boss.Substring(0, Boss.IndexOf('@'))
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
}
