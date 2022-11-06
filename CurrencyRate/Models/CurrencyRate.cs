using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyRate.Models
{
    public class Currency
    {
        public string Cur_Id { get; set; }
        public string Date { get; set; }
        public string Cur_Abbreviation { get; set; }
        public int Cur_Scale { get; set; }
        public string Cur_Name { get; set; }
        public decimal Cur_OfficialRate { get; set; }
    }
}
