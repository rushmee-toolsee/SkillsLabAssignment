
namespace DataAccessLayer.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string DepartmentName { get; set; }
        public string ManagerName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}