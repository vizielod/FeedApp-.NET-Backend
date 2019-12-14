using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FeedApp.Bll.Entities
{
    public class ApplicationUser : IdentityUser<long>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Eating> Eatings { get; } = new List<Eating>();
        /*public long UserID { get; set; }
        public User User { get; set; }*/



    }
}
