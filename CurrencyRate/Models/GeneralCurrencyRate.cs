using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyRate.Models
{
    public class GeneralCurrencyRate : Currency
    {
        public decimal Cur_TomorrowRate { get; set; }
        public string Cur_GeneralScaleName { get; set; }
    }
}
