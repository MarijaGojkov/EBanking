namespace EBanking.UI.Models
{
    public class RegistrationModel : BaseModel
    {
        public string Email { get; set; }
        public string UserPin { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Label { get; set; }

        #region Validation
        public string EmailError { get; set; }
        public string UserPinError { get; set; }
        public string PasswordError { get; set; }
        public string ConfirmPasswordError { get; set; }
        #endregion
    }
}
