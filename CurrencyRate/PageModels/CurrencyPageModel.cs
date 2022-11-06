using CurrencyRate.Interfaces;
using CurrencyRate.Models;
using CurrencyRate.Services;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyRate.PageModels
{
    public class CurrencyPageModel : FreshBasePageModel
    {
        private readonly ICurrencyService _currencyService;
        public List<Currency> TodayCurrencies { get; set; } = new List<Currency>();
        public List<Currency> TomorrowCurrencies { get; set; } = new List<Currency>();
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
        }
    }
}
