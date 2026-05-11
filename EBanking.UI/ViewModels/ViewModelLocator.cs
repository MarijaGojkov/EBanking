using CommonServiceLocator;
using EBanking.DataAccess.Repositories;
using EBanking.DataAccess.Repositories.Implementation;
using EBanking.Services;
using EBanking.Services.Implementation;
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
            SimpleIoc.Default.Register<IAccountService, AccountService>();
            SimpleIoc.Default.Register<IUserService, UserService>();
            SimpleIoc.Default.Register<ITransactionService, TransactionService>();
            SimpleIoc.Default.Register<ICurrencyExchangeService, CurrencyExchangeService>();
            SimpleIoc.Default.Register<IAccountRepository, AccountRepository>();
            SimpleIoc.Default.Register<IUserRepository, UserRepository>();
            SimpleIoc.Default.Register<ITransactionRepository, TransactionRepository>();
            SimpleIoc.Default.Register<ICurrencyExchangeRepository, CurrencyExchangeRepository>();
            #endregion

            #region Register Views
            SimpleIoc.Default.Register<AccountViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<RegistrationViewModel>();
            SimpleIoc.Default.Register<PaymentViewModel>();
            SimpleIoc.Default.Register<CurrencyExchangeViewModel>();
            SimpleIoc.Default.Register<TransactionDetailsViewModel>();
            #endregion
        }

        public TransactionDetailsViewModel TransactionDetailsView => ServiceLocator.Current.GetInstance<TransactionDetailsViewModel>();
        public AccountViewModel AccountView => ServiceLocator.Current.GetInstance<AccountViewModel>();
        public LoginViewModel LoginView => ServiceLocator.Current.GetInstance<LoginViewModel>();
        public RegistrationViewModel RegistrationView => ServiceLocator.Current.GetInstance<RegistrationViewModel>();
        public PaymentViewModel PaymentView => ServiceLocator.Current.GetInstance<PaymentViewModel>();
        public CurrencyExchangeViewModel CurrencyExchangeView => ServiceLocator.Current.GetInstance<CurrencyExchangeViewModel>();

        public static void Cleanup() { }
    }
}
