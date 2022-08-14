using CommunityToolkit.Mvvm.ComponentModel;
using EBanking.UI.Models;

namespace EBanking.UI.ViewModels
{
    public class BaseViewModel<TModel> : ObservableRecipient where TModel : BaseModel, new()
	{
		public BaseViewModel()
		{
			Model = new TModel();
		}

		public TModel Model { get; set; }
	}
}
