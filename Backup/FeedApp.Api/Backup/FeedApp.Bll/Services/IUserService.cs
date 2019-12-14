using FeedApp.Bll.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedApp.Bll.Services
{
    public interface IUserService
    {
        User GetUser(int UserId);
        IEnumerable<User> GetUsers();
        User InsertUser(User newUser);
        void UpdateUser(int UserId, User updatedUser);
        void DeleteUser(int UserId);
    }
}
