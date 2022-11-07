using CurrencyRate.Helpers;
using CurrencyRate.Models;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CurrencyRate.PageModels
{
    public class SettingsPageModel : FreshBasePageModel
    {
        public ICommand BackToMainPageCommand => SingleExecutionCommand.FromFunc(BackToMainPage);
        public List<GeneralCurrencyRate> GeneralCurrencies { get; set; } = new List<GeneralCurrencyRate>();
        public SettingsPageModel() { }
        public override void Init(object initData)
        {
            GeneralCurrencies = (List<GeneralCurrencyRate>)initData;
        }
        private async Task BackToMainPage()
        {
            await CoreMethods.PopPageModel(GeneralCurrencies);
        }
    }
}
