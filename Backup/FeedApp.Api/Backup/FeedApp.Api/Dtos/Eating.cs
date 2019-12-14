using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedApp.Api.Dtos
{
    public enum Meal
    {
        Breakfast,
        Lunch,
        Dinner,
        Other
    }

    public class Eating
    {

        public int ID { get; set; }
        [Required(ErrorMessage = "Meal type is required", AllowEmptyStrings = false)]
        public Nullable<Meal> Meal { get; set; }

        [Required(ErrorMessage = "Quantity is required", AllowEmptyStrings = false)]
        public int Quantity { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DateTime { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public List<Food> FoodList { get; set; }
        //[Required(ErrorMessage = "Food is required", AllowEmptyStrings = false)]
        //public List<Food> Foods { get; set; }
        //public Food FoodForEating { get; set; }
    }
}
