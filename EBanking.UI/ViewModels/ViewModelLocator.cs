using CommonServiceLocator;
using EBanking.DataAccess.Repozitorijumi.Korisnik;
using EBanking.DataAccess.Repozitorijumi.Racun;
using EBanking.Services;
using EBanking.Services.Korisnik;
using EBanking.UI.ViewModels.Windows;
using GalaSoft.MvvmLight.Ioc;

namespace EBanking.UI.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            #region Register Services
            SimpleIoc.Default.Register<IRacunService, RacunService>();
            SimpleIoc.Default.Register<IKorisnikService, KorisnikService>();
            SimpleIoc.Default.Register<IRepozitorijumRacuna, RepozitorijumRacuna>();
            SimpleIoc.Default.Register<IRepozitorijumKorisnika, RepozitorijumKorisnika>();
            #endregion

            #region Register Views
            SimpleIoc.Default.Register<RacunViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<RegistracijaViewModel>();
            #endregion
        }

        public RacunViewModel RacunView => ServiceLocator.Current.GetInstance<RacunViewModel>();
        public LoginViewModel LoginView => ServiceLocator.Current.GetInstance<LoginViewModel>();
        public RegistracijaViewModel RegistracijaView => ServiceLocator.Current.GetInstance<RegistracijaViewModel>();

        public static void Cleanup() { }
    }
}
