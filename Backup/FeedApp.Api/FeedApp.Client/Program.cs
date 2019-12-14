using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FeedApp.Client
{
    class Program
    {

        static async Task Main(string[] args)
        {
            Console.Write("Food: ");
            var id = Console.ReadLine();

            await GetFood(int.Parse(id));
            //Product product=await GetProduct2(int.Parse(id));
            //Console.WriteLine(product.Name);

            Console.ReadKey();
        }

        public static async Task GetFood(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(new Uri($"http://localhost:53399/api/Food/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(json);
                }
            }
        }

    }
}
