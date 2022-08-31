using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.UI.Models;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.Generic;
using System.Linq;

namespace EBanking.UI.ViewModels.Windows
{
    public class TekuciRacunViewModel : BaseViewModel<TekuciRacunModel>
    {
        private readonly IRacunService _racunService;

        [PreferredConstructor]
        public TekuciRacunViewModel()
        {
            Model.Title = "Tekuci racun";    
        }

        public TekuciRacunViewModel(IRacunService racunService, int idKorisnika)
        {
            Model.Title = "Tekuci racun";
            _racunService = racunService;
            IdKorisnika = idKorisnika;
            GetRacun(idKorisnika);
        }

        public RelayCommand GetRacunCommand { get; set; }
        public int IdKorisnika { get; set; }

        // Metoda koja se poziva pri otvaranju prozora koja nam daje sve informacije o racunima za korisnika
        public void GetRacun(int idKorisnika)
        {
            Model.Racuni = _racunService.GetRacunForKorsnik(idKorisnika);

            Model.ImeKorsnika = Model.Racuni.First().Korisnik.Ime + " " + Model.Racuni.First().Korisnik.Prezime;
        }
    }
}
