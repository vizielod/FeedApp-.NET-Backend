using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FeedApp.Bll.Entities
{

    public class User
    {

        //[Key]
        //[Column(name: "ID", Order = 0)]
        //public int ApplicationUserId { get; set; }
        //[Key]
        //[ForeignKey("ApplicationUser")]
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /*public long UserInfoId { get; set; }
        public UserInfo UserInfo { get; set; }*/

        //public int ApplicationUserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Eating> Eatings { get; } = new List<Eating>();
        //public ICollection<DailyConsumption> dailyConsumptions { get; } = new List<DailyConsumption>();
    }
}
