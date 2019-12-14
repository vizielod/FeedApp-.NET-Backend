using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCore.Entities
{
    [Table("PRODUCT")]
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UnitPrice { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }


        public ICollection<ProductOrder> ProductOrders { get; }
            = new List<ProductOrder>();

    }
}
