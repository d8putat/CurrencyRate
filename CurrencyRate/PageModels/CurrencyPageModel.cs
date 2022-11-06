using CurrencyRate.Interfaces;
using CurrencyRate.Models;
using CurrencyRate.Services;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurrencyRate.PageModels
{
    public class CurrencyPageModel : FreshBasePageModel
    {
        private readonly ICurrencyService _currencyService;
        public List<Currency> TodayCurrencies { get; set; } = new List<Currency>();
        public List<Currency> TomorrowCurrencies { get; set; } = new List<Currency>();
        public List<GeneralCurrencyRate> GeneralCurrencies { get; set; } = new List<GeneralCurrencyRate>();
        public string Today { get; set; } = DateTime.Today.ToString("dd.MM.yyyy");
        public string Tomorrow { get; set; } = DateTime.Today.AddDays(1).ToString("dd.MM.yyyy");
        public CurrencyPageModel(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        public override void Init(object initData)
        {
            InitCurrencies();
        }   

        private void InitCurrencies()
        {
            TodayCurrencies = _currencyService.GetCurrencyRates().Item1;
            TomorrowCurrencies = _currencyService.GetCurrencyRates().Item2;
            for (int i = 0; i < TodayCurrencies.Count; i++)
            {
                var currency = new GeneralCurrencyRate();
                currency.Cur_Id = TodayCurrencies[i].Cur_Id;
                currency.Date = TodayCurrencies[i].Date;
                currency.Cur_Abbreviation = TodayCurrencies[i].Cur_Abbreviation;
                currency.Cur_Scale = TodayCurrencies[i].Cur_Scale;
                currency.Cur_Name = TodayCurrencies[i].Cur_Name;
                currency.Cur_OfficialRate = TodayCurrencies[i].Cur_OfficialRate;
                currency.Cur_GeneralScaleName = TodayCurrencies[i].Cur_Scale.ToString() + " " + TodayCurrencies[i].Cur_Name;
                // Заполняем поле курса на завтра из списка TomorrowCurrencies
                var item = TomorrowCurrencies.Find(p => p.Cur_Id == TodayCurrencies[i].Cur_Id);
                currency.Cur_TomorrowRate = item.Cur_OfficialRate;
                GeneralCurrencies.Add(currency);
            }
        }
    }
}
