using System.Collections.Generic;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Windows.Web.Http;
using System;
//using FeedApp.Api.Dtos;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FeedApp.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
        bool? create { get; set; }
        public SignUpPage()
        {
            this.InitializeComponent();
        }

        private void SignInHyperLinkButton_Click(object sender, RoutedEventArgs e)
        {
            App.RootFrame.Navigate(typeof(MainPage));
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            var user = new FeedApp.Api.Dtos.User
            {
                
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                UserName = UserNameTextBox.Text,
                Email = EmailTextBox.Text,
                Password = PasswordBox.Password
                
            };

            Console.WriteLine(user.ToString());

            if(PasswordBox.Password == ReEnterPasswordBox.Password && EULACheckBox.IsChecked == true)
            {
                using (var client = new HttpClient())
                {
                    var content = JsonConvert.SerializeObject(user);


                    if (create == null || create == true)
                    {
                        Task task = Task.Run(async () =>
                        {
                            var data = new HttpFormUrlEncodedContent(
                                new Dictionary<string, string>
                                {
                                    ["value"] = content
                                }
                            );
                            await client.PostAsync(new Uri("http://localhost:53399/api/User"), data);
                        });
                        task.Wait();
                    }
                }
            }
            App.RootFrame.Navigate(typeof(UserInfoPage));
        }
    }
}
