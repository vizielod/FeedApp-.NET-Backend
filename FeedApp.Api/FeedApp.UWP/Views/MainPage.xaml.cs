﻿//using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FeedApp.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            /*// discover endpoints from metadata
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
            Console.ReadLine();*/
        }

        private void SignUpHyperLinkButton_Click(object sender, RoutedEventArgs e)
        {
            App.RootFrame.Navigate(typeof(SignUpPage));
        }
    }
}
