using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.UI.Common.Validacija;
using EBanking.UI.Models;
using EBanking.UI.Views;
using System.Windows;

namespace EBanking.UI.ViewModels.Windows
{
    public class LoginViewModel : BaseViewModel<LoginModel>
    {
        private readonly IKorisnikService _korisnikService;
        private readonly IRacunService _racunService;
        private readonly ITransakcijaService _transakcijaService;

        public LoginViewModel(IKorisnikService korisnikService, IRacunService racunService, ITransakcijaService transakcijaService)
        {
            Validator = new LoginViewValidator<LoginModel>();
            _korisnikService = korisnikService;
            _racunService = racunService;
            _transakcijaService = transakcijaService;

            Model.Title = "Login";

            LoginCommand = new RelayCommand(Login);
            OtvoriRegistracijuCommand = new RelayCommand(Registracija);
            _transakcijaService = transakcijaService;
        }

        public RelayCommand LoginCommand { get; set; }

        public RelayCommand OtvoriRegistracijuCommand { get; set; }

        public void Login()
        {
            Validator.ValidateModel(Model);

            var idKorisnika = _korisnikService.Login(Model.Email, Model.Password);

            if (Validator.IsValidModel && idKorisnika is not 0)
            {
                RacunView tekuciRacunView = new RacunView
                {
                    DataContext = new TekuciRacunViewModel(_racunService, _transakcijaService, idKorisnika)
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
