using FeedApp.Bll.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedApp.Bll.Services
{
    public interface IApplicationUserService
    {
        ApplicationUser GetApplicationUser(int ApplicationUserId);
        IEnumerable<ApplicationUser> GetApplicationUsers();
        ApplicationUser InsertApplicationUser(ApplicationUser newApplicationUser);
        void UpdateApplicationUser(int ApplicationUserId, ApplicationUser updatedApplicationUser);
        void DeleteApplicationUser(int ApplicationUserId);
    }
}
