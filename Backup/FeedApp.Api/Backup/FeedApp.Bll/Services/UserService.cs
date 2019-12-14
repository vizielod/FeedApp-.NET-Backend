using FeedApp.Bll.Entities;
using FeedApp.Bll.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeedApp.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;


        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        //DELETE
        public void DeleteUser(int UserId)
        {
            _context.Users.Remove(new User { ID = UserId });

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("User not found");
            }

        }

        //GET entity
        public User GetUser(int UserId)
        {
            return _context.Users.SingleOrDefault(u => u.ID == UserId) ?? throw new EntityNotFoundException("User not found!");
        }

        public IEnumerable<User> GetUsers()
        {
            var Users = _context.Users.ToList();

            return Users;
        }

        public User InsertUser(User newUser)
        {
            _context.Users.Add(newUser);

            _context.SaveChanges();

            return newUser;
        }

        public void UpdateUser(int UserId, User updatedUser)
        {
            updatedUser.ID = UserId;
            var entry = _context.Attach(updatedUser);
            entry.State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("User not found!");
            }
        }
    }
}
