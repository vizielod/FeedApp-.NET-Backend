using FeedApp.Bll.Entities;
using FeedApp.Bll.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeedApp.Bll.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly ApplicationDbContext _context;


        public UserInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        //DELETE
        public void DeleteUserInfo(int UserInfoId)
        {
            _context.UserInfos.Remove(new UserInfo { ID = UserInfoId });

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("UserInfo not found");
            }

        }

        //GET entity
        public UserInfo GetUserInfo(int UserInfoId)
        {
            return _context.UserInfos.SingleOrDefault(ui => ui.ID == UserInfoId) ?? throw new EntityNotFoundException("UserInfo not found!");
        }

        public IEnumerable<UserInfo> GetUserInfos()
        {
            var UserInfos = _context.UserInfos.ToList();

            return UserInfos;
        }

        public UserInfo InsertUserInfo(UserInfo newUserInfo)
        {
            _context.UserInfos.Add(newUserInfo);

            _context.SaveChanges();

            return newUserInfo;
        }

        public void UpdateUserInfo(int UserInfoId, UserInfo updatedUserInfo)
        {
            updatedUserInfo.ID = UserInfoId;
            var entry = _context.Attach(updatedUserInfo);
            entry.State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("UserInfo not found!");
            }
        }

    }
}
