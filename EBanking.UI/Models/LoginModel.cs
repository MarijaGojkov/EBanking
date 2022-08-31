namespace EBanking.UI.Models
{
    public class LoginModel : BaseModel
    {
        public string Email { get; set; } = "jovanjov";

        public string Password { get; set; } = "jova123";

        #region Validacija 
        public string EmailError { get; set; }
        public string PasswordError { get; set; }
        #endregion
    }
}
