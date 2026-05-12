namespace EBanking.DataAccess.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdCardNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserPin { get; set; }
        public string Password { get; set; }
    }
}
