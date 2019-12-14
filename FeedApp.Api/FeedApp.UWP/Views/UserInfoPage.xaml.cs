using FeedApp.Api.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FeedApp.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserInfoPage : Page
    {

        bool? create { get; set; }
        public UserInfoPage()
        {
            this.InitializeComponent();
        }

        private void ExerciseLevelComboBox_DropDownOpened(object sender, object e)
        {

        }

        private async void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            DailyWeightInfo dwi = new DailyWeightInfo()
            {
                Weight = (PoundsRB.IsChecked == true) ? 0.45359 * Convert.ToInt32(CurrentWeightTextBox.Text) : ((KilosRB.IsChecked == true) ? Convert.ToInt32(CurrentWeightTextBox.Text) : 0),
                DateTime = DateTime.Now
            };
            var userInfo = new FeedApp.Api.Dtos.UserInfo
            {
                Age = Convert.ToInt32(AgeTextBox.Text),
                Gender = (FemaleRB.IsChecked == true) ? Api.Dtos.Gender.Female : ((MaleRB.IsChecked == true) ? Api.Dtos.Gender.Male : Api.Dtos.Gender.Other),
                //WeightByDayList.Add(dwi),
                Height = (CMsRB.IsChecked == true) ? Convert.ToInt32(CMsTextBox.Text) : ((FeetRB.IsChecked == true) ? (Convert.ToInt32(FeetTextBox.Text) * 12 + Convert.ToInt32(InchesTextBox.Text) * 2.54) : 0),
                ExerciseLevel = ExerciseLevel.BasalMetabolicRate              

            };

            if(FemaleRB.IsChecked == true)
                userInfo.CaloriesForMaitenance = Convert.ToInt32(655.0955 + (9.5634 * dwi.Weight) + (1.8496 * userInfo.Height) - (4.67856 * Convert.ToInt32(AgeTextBox.Text)));
            else if (MaleRB.IsChecked == true)
                userInfo.CaloriesForMaitenance = Convert.ToInt32(66.4730 + (13.7516 * dwi.Weight) + (5.0033 * Height) - (6.7550 * Convert.ToInt32(AgeTextBox.Text)));

            using (var client = new HttpClient())
                {
                    var content = JsonConvert.SerializeObject(userInfo);


                    if (create == null || create == true)
                    {
                        //Task task = Task.Run(async () =>
                        //{
                        //    var data = new HttpFormUrlEncodedContent(
                        //        new Dictionary<string, string>
                        //        {
                        //            ["value"] = content
                        //        }
                        //    );
                            //await client.PostAsync(App.BaseUri, data);
                            await client.PostAsync(new Uri("http://localhost:53399/api/UserInfo"), 
                                new StringContent(content, Encoding.UTF8, "application/json"));
                        //});
                        //task.Wait();
                    }
                }
            App.RootFrame.Navigate(typeof(CalorieCalculated));
        //App.RootFrame.Navigate(typeof(CalorieCalculated));
        }
    }
}
