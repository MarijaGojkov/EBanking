using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.Services.Modeli;
using EBanking.UI.Common.Validacija;
using EBanking.UI.Models;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Windows;

namespace EBanking.UI.ViewModels.Windows
{
    public class PlacanjeViewModel : BaseViewModel<PlacanjeModel>
    {
        private readonly ITransakcijaService _transakcijaService;
        private readonly IRacunService _racunService;

        [PreferredConstructor]
        public PlacanjeViewModel()
        {
        }

        public PlacanjeViewModel(ITransakcijaService transakcijaService, IRacunService racunService, TransakcijaInfo transakcijaInfo)
        {
            Validator = new PlacanjeViewValidator<PlacanjeModel>();
            _transakcijaService = transakcijaService;
            _racunService = racunService;
            Model.Title = "Novo Placanje";
            Model.BrojRacunaPlatioca = transakcijaInfo.BrojRacuna;
            Model.TrenutnoStanje = transakcijaInfo.TrenutniBalans;
            PlacanjeCommand = new RelayCommand(Plati);
        }

        public RelayCommand PlacanjeCommand { get; set; }

        public void Plati()
        {
            if (Model.TrenutnoStanje > Model.KolicinaNovca)
            {
                if (Validator.ValidateModel(Model))
                {
                    _transakcijaService.CreateTransakcija(new TransakcijaModel
                    {
                        BrojRacuna = Model.BrojRacunaPlatioca,
                        KolicinaNovca = Model.KolicinaNovca,
                        BrojRacunaSekundarnogAktera = Model.BrojRacunaPrimaoca,
                        NazivSekundarnogAktera = Model.NazivPrimaoca,
                        BalansNakonTransakcije = Model.TrenutnoStanje - Model.KolicinaNovca,
                        Datum = System.DateTime.UtcNow

                    });

                    _racunService.UpdateBalance(Model.TrenutnoStanje - Model.KolicinaNovca, Model.BrojRacunaPlatioca);
                    Close();
                }
            }
            else {
                MessageBox.Show("Nemate dovoljno sredstava na racunu");
            }
        }
    }
}
