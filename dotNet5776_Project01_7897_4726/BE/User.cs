using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class User
    {
        public User() { }
        public User(UserType type, string username, string password, string name, int itemID = 0)
        {
            Type = type;
            UserName = username;
            Password = password;
            Name = name;
            if (type == UserType.NetworkManger && itemID != 0)
                throw new Exception("NetwokManger doesnt have an ID!");
            ItemID = itemID;

        }
        public UserType Type { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int ItemID { get; set; }
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
