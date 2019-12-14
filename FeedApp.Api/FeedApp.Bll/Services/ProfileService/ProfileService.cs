using FeedApp.Bll.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Bll.Services
{
    public class ProfileService : IProfileService
    {
        /*protected UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

            var user = await _userManager.FindByIdAsync(subject.GetSubjectId());
            if (user == null)
                throw new ArgumentException("Invalid subject identifier");

            var claims = new List<Claim>
         {
             new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
             new Claim(JwtClaimTypes.PreferredUserName, user.UserName)
         };

            claims.AddRange(await _userManager.GetClaimsAsync(user));
            context.IssuedClaims = claims.ToList();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

            var user = await _userManager.FindByIdAsync(subject.GetSubjectId());

            context.IsActive = false;

            if (user != null)
            {
                if (_userManager.SupportsUserSecurityStamp)
                {
                    var security_stamp = subject.Claims
                        .Where(c => c.Type == "security_stamp")
                        .Select(c => c.Value).SingleOrDefault();
                    if (security_stamp != null)
                    {
                        var db_security_stamp = await _userManager
                            .GetSecurityStampAsync(user);
                        if (db_security_stamp != security_stamp)
                            return;
                    }
                }

                context.IsActive =
                    !user.LockoutEnabled ||
                    !user.LockoutEnd.HasValue ||
                    user.LockoutEnd <= DateTime.Now;
            }
        }*/
    }
}
