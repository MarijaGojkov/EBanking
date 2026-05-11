using CommunityToolkit.Mvvm.ComponentModel;
using EBanking.UI.Common.Validation;
using EBanking.UI.Models;
using System.Windows;

namespace EBanking.UI.ViewModels
{
    public class BaseViewModel<TModel> : ObservableRecipient where TModel : BaseModel, new()
    {
        public BaseViewModel()
        {
            Model = new TModel();
        }

        public TModel Model { get; set; }

        public IValidator<TModel>? Validator { get; set; }

        public void Close()
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }
    }
}
