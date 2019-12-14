using FeedApp.Api.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.UWP.Services
{
    public class DietDiaryService : IDietDiaryService
    {
        private const string ServerUrl = "http://localhost:53399/";
        private const string GetEatingsUrl = ServerUrl + "api/Eating";

        public async Task<List<Eating>> GetEatingsAsync()
        {
            return await GetRequestAsync<List<Eating>>(GetEatingsUrl);
        }

        private async Task<T> GetRequestAsync<T>(string uri)
        {
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(new Uri(uri));
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }
    }
}
