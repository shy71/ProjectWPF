using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
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
        public Order(int branchID, string address, Kashrut kosher, int clientID, int id = 0)
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
        public Order(int branchID, string address, DateTime date, Kashrut kosher, int clientID, int id = 0)
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
        public Order(Branch branch, Client client, DateTime date, Kashrut kosher, int id = 0)
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
}
