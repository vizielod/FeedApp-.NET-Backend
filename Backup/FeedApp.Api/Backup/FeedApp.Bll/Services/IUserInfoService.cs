using FeedApp.Bll.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedApp.Bll.Services
{
    public interface IUserInfoService
    {
        UserInfo GetUserInfo(int userInfoId);
        IEnumerable<UserInfo> GetUserInfos();
        UserInfo InsertUserInfo(UserInfo newUserInfo);
        void UpdateUserInfo(int userInfoId, UserInfo updatedUserInfo);
        void DeleteUserInfo(int userInfoId);
    }
}
