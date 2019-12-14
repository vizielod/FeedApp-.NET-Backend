using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FeedApp.Bll.Entities
{
    
    public class Food
    {
        [Column(Order = 0)]
        public long ID { get; set; }
        [Column(Order = 1)]
        public string FoodName { get; set; }
        [Column(Order = 2)]
        public double Calories { get; set; }
        [Column(Order = 3)]
        public double Fat { get; set; }
        [Column(Order = 4)]
        public double Carbohydrate { get; set; }
        [Column(Order = 5)]
        public double Sugar { get; set; }
        [Column(Order = 6)]
        public double Fiber { get; set; }
        [Column(Order = 7)]
        public double Protein { get; set; }
        [Column(Order = 8)]
        public double Salt { get; set; }

        public ICollection<FoodsForEating> FoodsForEatings { get; } = new List<FoodsForEating>();
    }
}
