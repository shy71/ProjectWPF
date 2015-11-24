using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Class1
    {
        enum kashrut
        {
            low,medium,high
        }
        /// <summary>
        /// הזמנה
        /// </summary>
        public class Order
        {
            int orderNum;
            DateTime date;
            int branchNum;
            kashrut orderKashrut;
            string customerName;
            string address;
        }
    }
}
