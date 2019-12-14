using System;
using System.Collections.Generic;
using System.Text;

namespace FeedApp.Bll.Entities
{
    public class FoodsForEating
    {
        public long ID { get; set; }

        public long FoodId { get; set; }
        public Food Food { get; set; }

        public long EatingId { get; set; }
        public Eating Eating { get; set; }
    }
}
