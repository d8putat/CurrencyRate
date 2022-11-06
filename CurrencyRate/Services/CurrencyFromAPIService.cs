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
        private (List<Currency>, List<Currency>) Currencies;
        private readonly string address = "https://www.nbrb.by/api/exrates/rates?periodicity=0&ondate=";
        public (List<Currency>, List<Currency>) GetCurrencyRates()
        {
            return Currencies;
        }
        public CurrencyFromAPIService()
        {
            // конечные адреса, по которым будет приходить ответ с сервера на сегодня и завтра
            var todayAddress = address + DateTime.Today;
            var tomorrowAddress = address + DateTime.Today.AddDays(1);

            // Запрос на сегодняшнюю дату 
            var todayRequest = new GetRequest(todayAddress);
            var todayResponce = todayRequest.RunRequest();
            var todayCurrency = JsonConvert.DeserializeObject<List<Currency>>(todayResponce);

            // Запрос на завтрашнюю дату
            var tomorrowRequest = new GetRequest(tomorrowAddress);
            var tomorrowResponce = tomorrowRequest.RunRequest();
            var tomorrowCurrency = JsonConvert.DeserializeObject<List<Currency>>(tomorrowResponce);

            // создание кортежа со списками курсов 2 дней
            Currencies = (todayCurrency, tomorrowCurrency);
        }
    }
}
