using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiLaborAPI;
using WebApiLaborAPI.Models;

namespace WebApiLabor.Client
{
    class Program
    {

        private static Uri baseURI = new Uri("http://localhost:51189/");
        static async Task Main(string[] args)
        {
            Console.Write("Product: ");
            var id = Console.ReadLine();

            await GetProduct(int.Parse(id));
            //Product product=await GetProduct2(int.Parse(id));
            //Console.WriteLine(product.Name);

            Console.ReadKey();
        }

        /*static async Task<Product> GetProduct2(int id)
        {
            using (var client = new WebApiLaborAPIClient(
                new Uri("http://localhost:51189/")
                ,new TokenCredentials("mock")))
            {
                return await client.ApiProductByIdGetAsync(id);
            }
        }*/

        static async Task GetProduct(int id)
        {
            using (var client = new HttpClient())
            {
                var response=
                    await client.GetAsync(new Uri($"http://localhost:51189/api/Product/{id}"));
                if(response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(jsonString);
                }
                 
            }
        }
    }
}
