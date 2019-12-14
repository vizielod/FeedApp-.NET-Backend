using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedApp.Api.Dtos
{
    public class Food
    {
        public int ID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Food name is required", AllowEmptyStrings = false)]
        public string FoodName { get; set; }
        [Required]
        public double Calories { get; set; }
        [Required]
        public double Fat { get; set; }
        [Required]
        public double Carbohydrate { get; set; }
        public double Sugar { get; set; }
        public double Fiber { get; set; }
        [Required]
        public double Protein { get; set; }
        public double Salt { get; set; }

        //public List<Eating> EatingList { get; set; }
    }
}
