using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.UI.Models;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.Generic;
using System.Linq;

namespace EBanking.UI.ViewModels.Windows
{
    public class RacunViewModel : BaseViewModel<RacunModel>
    {
        private readonly IRacunService _racunService;

        [PreferredConstructor]
        public RacunViewModel()
        {
            Model.Title = "Tekuci racun";    
        }

        public RacunViewModel(IRacunService racunService, string idKorisnika)
        {
            Model.Title = "Tekuci racun";
            _racunService = racunService;
            IdKorisnika = idKorisnika;
            GetRacun(idKorisnika);
        }

        public RelayCommand GetRacunCommand { get; set; }
        public string IdKorisnika { get; set; }

        public void GetRacun(string idKorisnika)
        {
            List<Services.Modeli.RacunModel> racunModel = _racunService.GetRacunForKorsnik(idKorisnika);

            Model.ImeKorsnika = racunModel.First().Korisnik.Ime + " " + racunModel.First().Korisnik.Prezime;
        }
    }
}
