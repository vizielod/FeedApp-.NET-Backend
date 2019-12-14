using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiLabor.Bll.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
