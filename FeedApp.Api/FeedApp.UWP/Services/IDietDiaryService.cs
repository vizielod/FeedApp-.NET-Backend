using FeedApp.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.UWP.Services
{
    public interface IDietDiaryService
    {
        Task<List<Eating>> GetEatingsAsync();
    }
}
