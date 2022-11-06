using CurrencyRate.Interfaces;
using CurrencyRate.PageModels;
using CurrencyRate.Services;
using FreshMvvm;
using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurrencyRate
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitServices();
            Thread.Sleep(3000);
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
