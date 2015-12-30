using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace tOD_FFD
{
    class Program
    {/*
         public interface IKey {     int Key { get; } }  
        public class Name {     public string FirstName { get; set; }     public string LastName { get; set; } } 
        public enum Gender { male, female } public class Person : IKey{     public int Id { get; set; }     public Name PersonName { get; set; }     public Gender PersonGender { get; set; }     public bool Married { get; set; }      public int Key     {         get         {             return Id;         }     } }
        public enum Gender { male, female }
        public class Person : IKey { public int Id { get; set; } public Name PersonName { get; set; } public Gender PersonGender { get; set; } public bool Married { get; set; } public int Key { get { return Id; } } }  
        public class MyList<T> where T : IKey { List<T> list; public MyList() { T temp; list = new List<T>(); } public bool add(T t) { if (list.Exists(v => v.Key == t.Key))             return false; list.Add(t); return true; } public bool remove(T t) { T temp = list.Find(v => v.Key == t.Key); return list.Remove(temp); } }  

        static void Main(string[] args) { Person p = new Person { Id = 123, Married = true, PersonGender = Gender.male, PersonName = new Name { FirstName = "oshri", LastName = " Cohen" } }; MyList<Person> personList = new MyList<Person>(); Console.WriteLine(personList.add(p)); Person p2 = new Person { Id = 123, Married = false, PersonGender = Gender.female, PersonName = new Name { FirstName = "oshri", LastName = " Cohen" } }; Console.WriteLine(personList.add(p2)); Person p3 = new Person { Id = 1234, Married = false, PersonGender = Gender.female, PersonName = new Name { FirstName = "oshri", LastName = " Cohen" } }; Console.WriteLine(personList.add(p3)); 
        }  */
    }
}
