namespace EBanking.UI.Common.Validacija
{
    public interface IValidator<TModel>
    {

        void ValidateModel(TModel model);

        bool IsValidModel { get; set; }
    }
}
