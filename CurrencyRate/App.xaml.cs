using CurrencyRate.Interfaces;
using CurrencyRate.PageModels;
using CurrencyRate.Services;
using FreshMvvm;
using System;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CurrencyRate.LocalizationResources;

namespace CurrencyRate
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitServices();
            if (Device.RuntimePlatform != Device.UWP)
            {
               Resource.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            }
            MainPage = InitStartPage();
        }

        private void InitServices()
        {
            FreshIOC.Container.Register<ICurrencyService, CurrencyFromAPIService>().AsSingleton();
        }

        private Page InitStartPage()
        {
            var page = FreshPageModelResolver.ResolvePageModel<CurrencyPageModel>();
            return new FreshNavigationContainer(page);
        }
    }
}
