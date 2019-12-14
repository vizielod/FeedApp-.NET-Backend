using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; } = new List<ProductOrder>();
    }
}
