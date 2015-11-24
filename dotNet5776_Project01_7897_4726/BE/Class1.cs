using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    //הצלחתי!
    enum Kashrut
    {
        LOW, MEDIUM, HIGH
    }
    public class Class1
    {
        /// <summary>
        /// הזמנה
        /// </summary>
        public class Order
        {
            int orderNum;
            DateTime date;
            int branchNum;
            Kashrut orderKashrut;
            string customerName;
            string address;
        }
    }
}
