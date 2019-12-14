using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //IEnumerable
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
