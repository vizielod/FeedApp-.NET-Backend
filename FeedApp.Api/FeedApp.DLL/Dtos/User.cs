﻿//using FeedApp.Bll.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedApp.Api.Dtos
{
    public class User
    {
        public long ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string Level { get; set; }
        //Itt nem pontosan tudom, hogy ezt kene-e tarolni, szerintem nem
        //public UserInfo UserInfo { get; set; }

        public List<Eating> Eatings { get; set; }

        public override string ToString()
        {
            return "ID: " + ID + " Firstname: " + FirstName + " Lastname: " + LastName + " Email: " + Email;
        }

    }

}