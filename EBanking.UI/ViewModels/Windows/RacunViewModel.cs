using CommunityToolkit.Mvvm.Input;
using EBanking.Services;
using EBanking.UI.Models;

namespace EBanking.UI.ViewModels.Windows
{
    public class RacunViewModel : BaseViewModel<RacunModel>
    {
        private readonly IRacunService _racunService;

        public RacunViewModel(IRacunService racunService)
        {
            GetRacunCommand = new RelayCommand(GetRacun);

            Model.Title = "Tekuci racun";

            _racunService = racunService;           
        }

        public RelayCommand GetRacunCommand { get; set; }

        public void GetRacun()
        {
            Services.Modeli.RacunModel racunModel = _racunService.GetRacun("12131241");

            Model.BrojRacuna = racunModel.BrojRacuna;
        }
    }
}
