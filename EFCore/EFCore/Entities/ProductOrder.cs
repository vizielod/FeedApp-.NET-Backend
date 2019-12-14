﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Entities
{
    public class ProductOrder
    {
        public int Id { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }

    }
}
