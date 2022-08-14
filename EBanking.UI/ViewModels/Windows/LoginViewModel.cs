using CommunityToolkit.Mvvm.Input;
using EBanking.Services.Korisnik;
using EBanking.UI.Models;
using EBanking.UI.Views;

namespace EBanking.UI.ViewModels.Windows
{
    public class LoginViewModel : BaseViewModel<LoginModel>
    {
        private readonly IKorisnikService _korisnikService;

        public LoginViewModel(IKorisnikService korisnikService)
        {
            _korisnikService = korisnikService;

            Model.Title = "Login";

            LoginCommand = new RelayCommand(Login);
            RegistracijaCommand = new RelayCommand(Registracija);
        }

        public RelayCommand LoginCommand { get; set; }

        public RelayCommand RegistracijaCommand { get; set; }

        public void Login()
        {
            bool isTrue = _korisnikService.Login(Model.Email, Model.Password);
            if(isTrue)
            {
                TekuciRacunView tekuciRacunView = new TekuciRacunView();
                tekuciRacunView.Show();

            }
        }


        public void Registracija()
        {
            RegistracijaView registracijaView = new RegistracijaView();
            registracijaView.Show();
        }

    }
}
