using CommunityToolkit.Mvvm.Input;
using EBanking.Services.Korisnik;
using EBanking.UI.Models;

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
        }

        public RelayCommand LoginCommand { get; set; }

        public void Login()
        {
            bool isTrue = _korisnikService.Login(Model.Email, Model.Password);
        }
    }
}
