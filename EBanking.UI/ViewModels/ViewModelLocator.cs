using CommonServiceLocator;
using EBanking.DataAccess.Repozitorijumi.Racun;
using EBanking.Services;
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
            SimpleIoc.Default.Register<IRepozitorijumRacuna, RepozitorijumRacuna>();
            #endregion

            #region Register Views
            SimpleIoc.Default.Register<RacunViewModel>();
            #endregion
        }

        public RacunViewModel RacunView => ServiceLocator.Current.GetInstance<RacunViewModel>();

        public static void Cleanup() { }
    }
}
