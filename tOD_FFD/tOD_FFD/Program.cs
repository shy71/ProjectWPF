using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tOD_FFD
{
    static class MyClass
    {
        public static int MakeID(string Name, string Address, string PhoneNumber, string Boss, string EmployeeCount, string Kosher)
        {
            string[] BranchComponents = new string[6];
            BranchComponents[0] = Name;
            BranchComponents[1] = Address;
            BranchComponents[2] = PhoneNumber;
            BranchComponents[3] = Boss;
            BranchComponents[4] = EmployeeCount;
            BranchComponents[5] = Kosher;
            //return BranchComponents.Sum((item) => (item.Sum(r => (int)r)));
            var a=BranchComponents.Select((item, index) => (item.Sum(r => (int)r) * Math.Pow(10, index))).Sum();
            return 0;
        }
    }
    class Program
    {
        static public void Main(params string[] str)
        {
            MyClass.MakeID("afsdaf", "fdfdf", "dsdsdas", "dftgrht", "hjkuyt", "wwfa");
        }
    }
}
