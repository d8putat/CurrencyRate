using CurrencyRate.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using CurrencyRate.Models;
using System.Net;
using Newtonsoft.Json;

namespace CurrencyRate.Services
{
    public class CurrencyFromAPIService : ICurrencyService
    {
        private List<Currency> Currencies;
        private readonly string addressDateFormat = "yyyy-MM-dd";
        private readonly string address = "https://www.nbrb.by/api/exrates/rates?periodicity=0&ondate=";
        public List<Currency> GetCurrencyRates(DateTime date)
        {
            var request = new GetRequest(address + date.ToString(addressDateFormat));
            var responce = request.RunRequest();
            Currencies = JsonConvert.DeserializeObject<List<Currency>>(responce);
            return Currencies;
        }
        public CurrencyFromAPIService() { }
    }
}
