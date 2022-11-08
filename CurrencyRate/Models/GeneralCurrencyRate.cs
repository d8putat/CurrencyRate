using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyRate.Models
{
    public class GeneralCurrencyRate : Currency
    {
        public decimal Cur_SecondRate { get; set; }
        public string Cur_GeneralScaleName { get; set; }
        public bool Cur_IsVisible { get; set; }
    }
}
