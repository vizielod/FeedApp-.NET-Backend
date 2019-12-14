using FitAppUWP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace FitAppUWP
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Person NewPerson { get; set; }

        public List<Person> People { get; set; }

        


    public MainPage()
        {
            this.InitializeComponent();

            NewPerson = new Person()
            {
                Name = "Eric Clapton",
                Age = 8
            };

            DataContext = NewPerson;

            People = new List<Person>()
            {
            new Person() { Name = "Peter Griffin", Age = 40 },
            new Person() { Name = "Homer Simpson", Age = 42 }

            };
        }

        private void DecreaseButton_OnClick(object sender, RoutedEventArgs e)
        {
            NewPerson.Age--;
        }

        private void IncreaseButton_OnClick(object sender, RoutedEventArgs e)
        {
            NewPerson.Age++;
        }
    }
}
