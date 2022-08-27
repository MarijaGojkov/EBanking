namespace EBanking.UI.Models
{
    public class LoginModel : BaseModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        #region Validacija 
        public string EmailError { get; set; }
        public string PasswordError { get; set; }
        #endregion
    }
}
