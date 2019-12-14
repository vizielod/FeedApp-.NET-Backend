using System;
using System.Collections.Generic;

namespace HelloCSharp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Dog banan = new Dog() { Name = "Banan", DateOfBirth = new DateTime(2014, 06, 10) };

            var watson = new Dog { Name = "Watson" };

            var unnamed = new Dog { DateOfBirth = new DateTime ( 2017, 02, 10 ) };
            var unknown = new Dog { };

            /*var dogs = new List<Dog>();
            dogs.Add(banan);
            dogs.Add(watson);
            dogs.Add(unnamed);
            dogs.Add(unknown);*/

            var pimpedli = new Dog()
            {
                Name = "Pimpedli",
                DateOfBirth = new DateTime(2006, 06, 10),
                ["Chip azonosito"] = "123125AJ"
            };

            Console.WriteLine(pimpedli);

            var dogs = new Dictionary<string, Dog>
            {
                ["banan"] = banan,
                ["pimpedli"] = watson,
                ["unnamed"] = unnamed,
                ["unknown"] = unknown
            };

            foreach(var dog in dogs)
                Console.WriteLine(dog);
            Console.ReadLine();
        }
    }
}
