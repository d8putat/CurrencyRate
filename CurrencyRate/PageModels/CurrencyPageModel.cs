using CurrencyRate.Helpers;
using CurrencyRate.Interfaces;
using CurrencyRate.Models;
using CurrencyRate.Services;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace CurrencyRate.PageModels
{
    public class CurrencyPageModel : FreshBasePageModel
    {
        private readonly ICurrencyService _currencyService;
        public List<Currency> YesturdayCurrencies { get; set; } = new List<Currency>();
        public List<Currency> TodayCurrencies { get; set; } = new List<Currency>();
        public List<Currency> TomorrowCurrencies { get; set; } = new List<Currency>();
        public List<GeneralCurrencyRate> GeneralCurrencies { get; set; } = new List<GeneralCurrencyRate>();
        public List<GeneralCurrencyRate> DisplayedCurrencies { get; set; } = new List<GeneralCurrencyRate>();
        private readonly string dateFormat = "dd.MM.yyyy";
        private DateTime Today = DateTime.Today;
        public string FirstDate { get; set; }
        public string SecondDate { get; set; }
        public ICommand SettingPageCommand => SingleExecutionCommand.FromFunc(GoToSettinsPage);
        public CurrencyPageModel(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        public override void Init(object initData)
        {
            InitCurrencies();
            Preferences.Set("IsTheFirstLaunch", false);
        }
        public override void ReverseInit(object returnedData)
        {
            GeneralCurrencies = (List<GeneralCurrencyRate>)returnedData;
        }

        private async Task GoToSettinsPage()
        {
            await CoreMethods.PushPageModel<SettingsPageModel>(GeneralCurrencies);
        }
        private void InitCurrencies()
        {
            TodayCurrencies = _currencyService.GetCurrencyRates(Today);
            TomorrowCurrencies = _currencyService.GetCurrencyRates(Today.AddDays(1));
            if(TomorrowCurrencies.Count > 0)
            {
                FirstDate = Today.ToString(dateFormat);
                SecondDate = Today.AddDays(1).ToString(dateFormat);
                for (int i = 0; i < TodayCurrencies.Count; i++)
                {
                    var currency = new GeneralCurrencyRate();
                    SetCurrencyValues(currency, i);
                    currency.Cur_OfficialRate = TodayCurrencies[i].Cur_OfficialRate;
                    // Заполняем поле курса на завтра из списка TomorrowCurrencies
                    currency.Cur_SecondRate = TomorrowCurrencies.Find(p => p.Cur_Id == TodayCurrencies[i].Cur_Id).Cur_OfficialRate;
                    GeneralCurrencies.Add(currency);
                }
            }
            else
            {
                var yesturday = Today.AddDays(-1);
                YesturdayCurrencies = _currencyService.GetCurrencyRates(yesturday);
                FirstDate = yesturday.ToString(dateFormat);
                SecondDate = Today.ToString(dateFormat);
                for (int i = 0; i < TodayCurrencies.Count; i++)
                {
                    var currency = new GeneralCurrencyRate();
                    SetCurrencyValues(currency, i);
                    // Заполняем поле курса на вчера из списка YesturdayCurrencies
                    currency.Cur_OfficialRate = YesturdayCurrencies.Find(p => p.Cur_Id == TodayCurrencies[i].Cur_Id).Cur_OfficialRate;
                    // Заполняем поле курса на сегодня из списка TodayCurrencies
                    currency.Cur_SecondRate = TodayCurrencies[i].Cur_OfficialRate;
                    GeneralCurrencies.Add(currency);
                }
            }
            bool hasKey = Preferences.Get("IsTheFirstLaunch",true);
            if (hasKey)
            {
                foreach (var currency in GeneralCurrencies)
                {
                    currency.Cur_IsVisible = false;
                    Preferences.Set(currency.Cur_Abbreviation+"Visibility",false);
                }
                // При первом запуске устанавливаем для usd, eur и rub видимость true
                SetTrueVisibility("USD");
                SetTrueVisibility("EUR");
                SetTrueVisibility("RUB");
            }
            else
            {
                foreach (var currency in GeneralCurrencies)
                    currency.Cur_IsVisible = Preferences.Get(currency.Cur_Abbreviation + "Visibility", false);
            }
            foreach (var currency in GeneralCurrencies)
            {
                if(currency.Cur_IsVisible)
                    DisplayedCurrencies.Add(currency);
            }
        }
        private void SetTrueVisibility(string abbreviation)
        {
            var currency = GeneralCurrencies.Find(p => p.Cur_Abbreviation == abbreviation);
            currency.Cur_IsVisible = true;
            Preferences.Set(currency.Cur_Abbreviation + "Visibility", true);
        }

        private void SetCurrencyValues(GeneralCurrencyRate currency,int i)
        {
            currency.Cur_Id = TodayCurrencies[i].Cur_Id;
            currency.Date = TodayCurrencies[i].Date;
            currency.Cur_Abbreviation = TodayCurrencies[i].Cur_Abbreviation;
            currency.Cur_Scale = TodayCurrencies[i].Cur_Scale;
            currency.Cur_Name = TodayCurrencies[i].Cur_Name;
            currency.Cur_GeneralScaleName = TodayCurrencies[i].Cur_Scale.ToString() + " " + TodayCurrencies[i].Cur_Name;
        }
    }
}
