using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FeedApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

            /*var disco = await DiscoveryClient.GetAsync("https://localhost:53399/");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }
            else
            {
                // request token
                var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
                var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("dotnetGURU", "Admin123.", "api1");

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                }
                else
                {
                    Console.WriteLine(tokenResponse.Json);
                    using (var client = new HttpClient())
                    {
                        client.SetBearerToken(tokenResponse.AccessToken);

                        var response = await client.GetAsync("https://localhost:53399/api/values");
                        if (!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine(response.StatusCode);
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(content);
                        }
                    }
                }
            }
            Console.ReadLine();*/

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
