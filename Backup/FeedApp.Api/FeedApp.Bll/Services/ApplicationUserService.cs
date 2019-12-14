using FeedApp.Bll.Entities;
using FeedApp.Bll.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeedApp.Bll.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationDbContext _context;


        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        //DELETE
        public void DeleteApplicationUser(int ApplicationUserId)
        {
            _context.Users.Remove(new ApplicationUser { Id = ApplicationUserId });

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("ApplicationUser not found");
            }

        }

        //GET entity
        public ApplicationUser GetApplicationUser(int ApplicationUserId)
        {
            return _context.Users
                .Include(u => u.Eatings)
                .SingleOrDefault(u => u.Id == ApplicationUserId) ?? throw new EntityNotFoundException("ApplicationUser not found!");
        }

        public IEnumerable<ApplicationUser> GetApplicationUsers()
        {
            var ApplicationUsers = _context.Users
                .Include(u => u.Eatings)
                .ToList();

            return ApplicationUsers;
        }

        public ApplicationUser InsertApplicationUser(ApplicationUser newApplicationUser)
        {
            _context.Users.Add(newApplicationUser);

            _context.SaveChanges();

            return newApplicationUser;
        }

        public void UpdateApplicationUser(int ApplicationUserId, ApplicationUser updatedApplicationUser)
        {
            updatedApplicationUser.Id = ApplicationUserId;
            var entry = _context.Attach(updatedApplicationUser);
            entry.State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("ApplicationUser not found!");
            }
        }
    }
}
