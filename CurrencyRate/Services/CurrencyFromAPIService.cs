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
        private CurrencyEngName CurrencyEngName;
        private readonly string addressDateFormat = "yyyy-MM-dd";
        private readonly string address = "https://www.nbrb.by/api/exrates/rates?periodicity=0&ondate=";
        private readonly string adressEngCurrencyName = "https://www.nbrb.by/api/exrates/currencies/";
        public List<Currency> GetCurrencyRates(DateTime date)
        {
            var request = new GetRequest(address + date.ToString(addressDateFormat));
            var responce = request.RunRequest();
            Currencies = JsonConvert.DeserializeObject<List<Currency>>(responce);
            return Currencies;
        }
        public string GetCurrencyEngNameById(string cur_id)
        {
            var request = new GetRequest(adressEngCurrencyName + cur_id.ToString());
            var responce = request.RunRequest();
            CurrencyEngName = JsonConvert.DeserializeObject<CurrencyEngName>(responce);
            return CurrencyEngName.Cur_QuotName_Eng;
        }
        public CurrencyFromAPIService() { }
    }
}
