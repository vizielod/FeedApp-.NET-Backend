using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Identityclient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("https://localhost:PORT/");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }
            else
            {
                // request token
                var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
                var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("dotnetrox", "Admin123.", "api1");

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

                        var response = await client.GetAsync("https://localhost:PORT/api/values");
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
            Console.ReadLine();
        }
    }
}
