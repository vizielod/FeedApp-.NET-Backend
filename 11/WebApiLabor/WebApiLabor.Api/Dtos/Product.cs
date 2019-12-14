using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiLabor.Bll.Entities;

namespace WebApiLabor.Api.Dtos
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required", AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Unit price must be higher than 0.")]
        public int UnitPrice { get; set; }
        public ShipmentRegion ShipmentRegion { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Order> Orders { get; set; }
    }
}
