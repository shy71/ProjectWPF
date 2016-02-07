using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
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
}
