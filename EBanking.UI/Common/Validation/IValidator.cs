namespace EBanking.UI.Common.Validation
{
    public interface IValidator<TModel>
    {
        bool ValidateModel(TModel model);
    }
}
