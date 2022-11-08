using System;
using System.Collections.Generic;
using System.Text;
using CurrencyRate.Models;

namespace CurrencyRate.Interfaces
{
    public interface ICurrencyService
    {
        List<Currency> GetCurrencyRates(DateTime date);
    }
}
