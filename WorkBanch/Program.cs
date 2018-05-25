using System;
using System.Collections.Generic;

namespace WorkBanch
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj1 = new { name = "obj1" };
            var obj2 = new { name = "obj2" };
            var myList = CreateList(obj1, obj2);

            myList.ForEach(el => Console.WriteLine(el.name));
        }

        public static List<T> CreateList<T>(params T[] elements)
        {
            return new List<T>(elements);
        }
    }
}
