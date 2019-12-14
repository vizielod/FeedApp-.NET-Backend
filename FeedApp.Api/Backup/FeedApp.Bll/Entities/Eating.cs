using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FeedApp.Bll.Entities
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
        public long ID { get; set; }
        public Nullable<Meal> Meal { get; set; }
        public double Quantity { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        public ICollection<FoodsForEating> FoodsForEatings { get; } = new List<FoodsForEating>();
        //public ICollection<Food> FoodsForEating { get; } = new List<Food>();
        /*public int FoodId { get; set; }
        public Food Food { get; set; }*/

    }
}
