using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EBanking.Services.Korisnik;
using EBanking.UI.Models;
using EBanking.UI.Views;

namespace EBanking.UI.ViewModels.Windows
{
    public class RegistracijaViewModel : BaseViewModel<RegistracijaModel>
    {
        private readonly IKorisnikService _korisnikService;

        public RegistracijaViewModel(IKorisnikService korisnikService)
        {
            Model.Title = "Registracija";
            _korisnikService = korisnikService;
            RegistracijaCommand = new RelayCommand(ShowLoginWindow);
        }

        public RelayCommand RegistracijaCommand { get; set; }

        public void ShowLoginWindow()
        {
            LoginView loginView = new LoginView();
            loginView.Show();
        }
        
        public void KreirajKorisnika()



    }
}

