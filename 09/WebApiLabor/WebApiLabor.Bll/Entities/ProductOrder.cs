﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiLabor.Bll.Entities
{
    public class ProductOrder
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
