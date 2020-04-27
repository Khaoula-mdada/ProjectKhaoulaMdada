using Newtonsoft.Json;
using projectXamarin.Models;
using projectXamarin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace projectXamarin.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public bool IsNotBusy
        {
            get { return !IsBusy; }
        }

        bool showInfo;
        public bool ShowInfo
        {
            get { return showInfo; }
            set { SetProperty(ref showInfo, value); }
        }

        string querry;
        public string Querry
        {
            get { return querry; }
            set { SetProperty(ref querry, value); }
        }

        string date;
        public string Date
        {
            get { return date; }
            set { SetProperty(ref date, value); }
        }

        string countryName;
        public string CountryName
        {
            get { return countryName; }
            set { SetProperty(ref countryName, value); }
        }

        string newCases;
        public string NewCases
        {
            get { return newCases; }
            set { SetProperty(ref newCases, value); }
        }

        string newDeaths;
        public string NewDeaths
        {
            get { return newDeaths; }
            set { SetProperty(ref newDeaths, value); }
        }

        string totalCases;
        public string TotalCases
        {
            get { return totalCases; }
            set { SetProperty(ref totalCases, value); }
        }

        string totalRecoveries;
        public string TotalRecoveries
        {
            get { return totalRecoveries; }
            set { SetProperty(ref totalRecoveries, value); }
        }

        string totalDeaths;
        public string TotalDeaths
        {
            get { return totalDeaths; }
            set { SetProperty(ref totalDeaths, value); }
        }

        public ICommand GetCommand => new Command(() => Task.Run(LoadcoronaData));
        public ICommand Reset => new Command(() => Task.Run(ResetForm));
        async Task LoadcoronaData()
        {
            if (String.IsNullOrEmpty(Querry))
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Vous n'avez pas saisi.", "OK");
            }
            if (IsBusy) return;
            IsBusy = true;
            var client = HttpService.GetInstance();
            try
            {
                var result = await client.GetAsync($"https://api.thevirustracker.com/free-api?countryTimeline="+ Querry);
                var serializedResponse = await result.Content.ReadAsStringAsync();
                CoronaResponse response = JsonConvert.DeserializeObject<CoronaResponse>(serializedResponse);

                if (response.CountryData == null || response.CountryStats == null)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await App.Current.MainPage.DisplayAlert("Erreur", "Pays non trouvé", "OK");
                    });
                    IsBusy = false;
                    return;
                }

                //Transformation de la propriété CountryStats de CoronaResponse en Dictionnaire car ses propriétés sont dynamiques (dates)
                Dictionary<string, object> dicStats = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.CountryStats.FirstOrDefault().ToString());
                //Récupération de l'objet correspondant à la date de la veille dans le dictionnaire
                var value = dicStats[DateTime.Now.AddDays(-1).ToString("M/d/yy")];
                //Deserialisation de l'objet en CountryStatistic
                CountryStatistic stats = JsonConvert.DeserializeObject<CountryStatistic>(value.ToString());

                ShowInfo = true;
                CountryName = response.CountryData.FirstOrDefault().Info.Name;
                NewCases = stats.NewCases;
                NewDeaths = stats.NewDeaths;
                TotalCases = stats.TotalCases;
                TotalRecoveries = stats.TotalRecoveries;
                TotalDeaths = stats.TotalDeaths;
                Date = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");

                IsBusy = false;
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Erreur", "Pays non trouvé", "OK");
                });
                IsBusy = false;
            }
        }
        public void ResetForm()
        {
            ShowInfo = false;
            CountryName = "";
            NewCases = "";
            NewDeaths = "";
            TotalCases = "";
            TotalRecoveries = "";
            TotalDeaths = "";
            Date = "";
            Querry = "";
        }
    }
}
