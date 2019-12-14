using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.UWP.ViewModels
{
    public  class LoginViewModel : FeedApp.Api.Dtos.User
    {
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
