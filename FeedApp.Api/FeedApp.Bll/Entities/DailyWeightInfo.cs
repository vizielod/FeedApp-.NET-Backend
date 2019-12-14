using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FeedApp.Bll.Entities
{
    public class DailyWeightInfo
    {
        public long ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        public double Weight { get; set; }
    }
}
