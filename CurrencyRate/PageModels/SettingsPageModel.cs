using CurrencyRate.Helpers;
using CurrencyRate.Models;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CurrencyRate.PageModels
{
    public class SettingsPageModel : FreshBasePageModel
    {
        public List<GeneralCurrencyRate> GeneralCurrencies { get; set; } = new List<GeneralCurrencyRate>();
        public ICommand BackToMainPageCommand => SingleExecutionCommand.FromFunc(BackToMainPage);
        public ICommand VisibilityChangedCommand => new Command<GeneralCurrencyRate>(ChangeVisibility);
        public ICommand SaveChangesCommand => new Command(SaveChanges);
        private readonly string SaveSettingsName = "SaveSettings";
        public SettingsPageModel() { }
        public override void Init(object initData)
        {

            GeneralCurrencies = (List<GeneralCurrencyRate>)initData;
        }
        private async Task BackToMainPage()
        {
            if(Preferences.Get(SaveSettingsName, false))
            {
                Preferences.Set(SaveSettingsName, false);
                await CoreMethods.PopPageModel(GeneralCurrencies);
            }
            else
            {
                await CoreMethods.PopPageModel();
                foreach (var currency in GeneralCurrencies)
                {
                    currency.Cur_IsVisible = Preferences.Get(currency.Cur_Abbreviation + "Visibility", false);
                }
            }   
        }
        private void ChangeVisibility(GeneralCurrencyRate currency)
        {
            currency.Cur_IsVisible = !currency.Cur_IsVisible;
        }

        private void SaveChanges()
        {
            foreach(var currenсy in GeneralCurrencies)
            {
                Preferences.Set(currenсy.Cur_Abbreviation + "Visibility", currenсy.Cur_IsVisible);
            }
            Preferences.Set(SaveSettingsName, true);
        }
    }
}
