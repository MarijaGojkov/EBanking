using CommunityToolkit.Mvvm.ComponentModel;
using EBanking.UI.Models;

namespace EBanking.UI.ViewModels.Windows
{
    public class RegistracijaViewModel : BaseViewModel<RegistracijaModel>
    {
        public RegistracijaViewModel()
        {
            Model.Title = "Registracija";
        }
    }
}

