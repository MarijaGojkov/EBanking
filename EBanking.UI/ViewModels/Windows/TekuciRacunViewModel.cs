using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.Services.Modeli;
using EBanking.UI.Models;
using EBanking.UI.Views;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EBanking.UI.ViewModels.Windows
{
    public class TekuciRacunViewModel : BaseViewModel<TekuciRacunModel>
    {
        private readonly IRacunService _racunService;
        private readonly ITransakcijaService _transakcijaService;

        [PreferredConstructor]
        public TekuciRacunViewModel()
        {
            Model.Title = "Tekuci racun";
        }

        public TekuciRacunViewModel(IRacunService racunService, ITransakcijaService transakcijaService, int idKorisnika)
        {
            Model.Title = "Tekuci racun";
            _racunService = racunService;
            _transakcijaService = transakcijaService;
            IdKorisnika = idKorisnika;
            GetRacun(idKorisnika);

            PlacanjeCommand = new RelayCommand(Placanje);
        }

        public RelayCommand PlacanjeCommand { get; set; }
        public int IdKorisnika { get; set; }

        // Metoda koja se poziva pri otvaranju prozora koja nam daje sve informacije o racunima za korisnika
        public void GetRacun(int idKorisnika)
        {
            Model.Racuni = _racunService.GetRacunForKorsnik(idKorisnika);

            Model.ImeKorsnika = Model.Racuni.First().Korisnik.Ime + " " + Model.Racuni.First().Korisnik.Prezime;
        }

        public async void Placanje()
        {
            if(Model.IzabranRacun is not null)
            {
                PlacanjeView view = new PlacanjeView
                {
                    DataContext = new PlacanjeViewModel(_transakcijaService, _racunService,
                                new TransakcijaInfo { BrojRacuna = Model.IzabranRacun.BrojRacuna, TrenutniBalans = Model.IzabranRacun.Balans,
                                ImeKorisnika = Model.Racuni.First().Korisnik.Ime + " " + Model.Racuni.First().Korisnik.Prezime })
                };
                //Pri kreiranju novog prozora, subskrajbujemo se na event "Closing". Kada se on okine, tokom zatvaranja child stranice
                //Kod nas se okine metoda GetRacun
                view.Closing += (s, o) =>
                {
                    GetRacun(IdKorisnika);
                };
                view.Show();

            } 
            else
            {
                MessageBox.Show("Morate izabrati racun", "Greska");
            }  
        }
    }
}
