using System;
using System.Linq;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            using (NorthwindContext ctx= new NorthwindContext())
            {
                var p = ctx.Products.ToList();/*.FirstOrDefault()*/;
            }

        }
    }
}
