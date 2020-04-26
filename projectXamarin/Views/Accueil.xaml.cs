using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projectXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Accueil : ContentPage
    {
        public Accueil()
        {
            InitializeComponent();

        }
        public Xamarin.Forms.INavigation Navigation { get; }
        public MainPage mainPage = new MainPage();
        async void GoToCreationPage(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => {
                await Application.Current.MainPage.Navigation.PushAsync(mainPage);
            });
        }
    }
}