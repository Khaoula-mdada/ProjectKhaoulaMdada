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
        string querry;
        public string Querry
        {
            get { return querry; }
            set { SetProperty(ref querry, value); }
        }
        string intitule;
        public string Intitule
        {
            get { return intitule; }
            set { SetProperty(ref intitule, value); }
        }
        string code;
        public string Code
        {
            get { return code; }
            set { SetProperty(ref code, value); }
        }
     
        public ICommand GetCommand => new Command(() => Task.Run(LoadcoronaData));
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
                var result = await client.GetAsync($"https://api.thevirustracker.com/free-api?countryTimeline="+Querry);
                var serializedResponse = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Coronaresponse>(serializedResponse);
                Console.WriteLine("result", result);
                if (response?.Countrytimelinedata != null)
                {
                    Intitule = response.Countrytimelinedata[0].Title;
                    Code = response.Countrytimelinedata[0].Code;
                }
         
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            IsBusy = false;
        }
    }
}
