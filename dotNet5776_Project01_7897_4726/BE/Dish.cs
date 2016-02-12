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
            get { return name + ((name.Any(item => item == '|')) ? "" : (((Active) ? "" : "|Unactive"))); }
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
            + "\n\tActive: " + Active;
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
}
