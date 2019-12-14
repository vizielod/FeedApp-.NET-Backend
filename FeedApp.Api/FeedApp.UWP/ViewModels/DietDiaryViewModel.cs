using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedApp.Api.Dtos;
using Windows.UI.Xaml.Navigation;
using FeedApp.UWP.Services;
using Template10.Mvvm;

namespace FeedApp.UWP.ViewModels
{
    public class DietDiaryViewModel : ViewModelBase
    {
        private readonly IDietDiaryService _dietDiaryService;

        public DietDiaryViewModel(IDietDiaryService dietDiaryService)
        {
            _dietDiaryService = dietDiaryService;
        }

        private List<Eating> _eatings;
        public List<Eating> Eatings
        {
            get { return _eatings; }
            set { Set(ref _eatings, value); }
        }

        //adatok lekérése
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Eatings = await _dietDiaryService.GetEatingsAsync();

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

    }
}
