﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;

namespace tOD_FFD
{
    enum MyEnum
    {
        LOW, MED, HIGH
    }
    interface Inta
    { }
    class Dish : Inta
    {
        Random r = new Random();

        public Dish()
        {
            DishID = r.Next(1000);
        }
        public int DishID;
    }
    class Program
    {
        public static float DishProfit(int ID)
        {
            return (ID * 2) - 3;
        }
        public static void Main()
        {
            var s = new XElement("Shy");
            s.Save("@SHY.xml");
            var d = XElement.Load("@SHY.xml");
        }


        public static IEnumerable<IGrouping<int, int>> GetProfitByDishs()
        {
            List<Dish> list = new List<Dish>();
            list.Add(new Dish());
            list.Add(new Dish());
            list.Add(new Dish());
            list.Add(new Dish());
            list.Add(new Dish());
            list.Add(new Dish());
            var v = (from item in list
                     select item.DishID).Distinct();
            var d = from item in v
                    group DishProfit(item) by item;
            return null;

        }
    }
}
