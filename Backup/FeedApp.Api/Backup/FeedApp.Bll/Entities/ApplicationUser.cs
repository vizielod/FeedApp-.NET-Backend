using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FeedApp.Bll.Entities
{
    public class ApplicationUser : IdentityUser<long>
    {
        
        public long UserID { get; set; }
        public User User { get; set; }

    }
}
