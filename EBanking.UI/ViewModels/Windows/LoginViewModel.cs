using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.Services.Korisnik;
using EBanking.UI.Models;
using EBanking.UI.Views;
using System.Windows;

namespace EBanking.UI.ViewModels.Windows
{
    public class LoginViewModel : BaseViewModel<LoginModel>
    {
        private readonly IKorisnikService _korisnikService;
        private readonly IRacunService _racunService;

        public LoginViewModel(IKorisnikService korisnikService, IRacunService racunService)
        {
            _korisnikService = korisnikService;
            _racunService = racunService;

            Model.Title = "Login";

            LoginCommand = new RelayCommand(Login);
            OtvoriRegistracijuCommand = new RelayCommand(Registracija);
        }

        public RelayCommand LoginCommand { get; set; }

        public RelayCommand OtvoriRegistracijuCommand { get; set; }

        public void Login()
        {
            if(_korisnikService.Login(Model.Email, Model.Password))
            {
                TekuciRacunView tekuciRacunView = new TekuciRacunView{
                    DataContext = new RacunViewModel(_racunService, new RacunModel())
                };
                tekuciRacunView.Show();
                Close();

            }
        }

        public void Registracija()
        {
            RegistracijaView registracijaView = new RegistracijaView();
            registracijaView.Show();
            Close();
        }
    }
}
