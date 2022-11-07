using CurrencyRate.Helpers;
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
        public SettingsPageModel() { }
        private async Task BackToMainPage()
        {
            await CoreMethods.PopPageModel();
        }
    }
}
