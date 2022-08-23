using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.UI.Models;
using GalaSoft.MvvmLight.Ioc;

namespace EBanking.UI.ViewModels.Windows
{
    public class RacunViewModel : BaseViewModel<RacunModel>
    {
        private readonly IRacunService _racunService;

        [PreferredConstructor]
        public RacunViewModel(IRacunService racunService)
        {
            GetRacunCommand = new RelayCommand(GetRacun);

            Model.Title = "Tekuci racun";
            
            _racunService = racunService;           
        }

        public RacunViewModel(IRacunService racunService, RacunModel model)
        {
            _racunService = racunService;
            Model = model;
            GetRacun();
        }

        public RelayCommand GetRacunCommand { get; set; }

        public RacunModel Model { get; set; }

        public void GetRacun()
        {
            Services.Modeli.RacunModel racunModel = _racunService.GetRacun("1112");

            Model.BrojRacuna = racunModel.BrojRacuna;
        }
    }
}
