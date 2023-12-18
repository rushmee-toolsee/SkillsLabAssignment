
namespace DataAccessLayer.Models
{
    public class UserAccount
   {
        public int AccountID { get;  set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
    }
}