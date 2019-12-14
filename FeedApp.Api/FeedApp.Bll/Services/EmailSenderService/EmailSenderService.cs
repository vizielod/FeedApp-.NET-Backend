
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Bll.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
