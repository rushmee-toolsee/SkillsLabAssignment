using DataAccessLayer.Models;

namespace DataAccessLayer.Repo.IRepositories
{
    public interface IAccountRepository
    {
        bool IsUserAuthenticated(UserAccount account);
        bool RegisterAccount(User user);
        string EmailExists(string email);
        string NicExists(string nic);
        int GetAccountIdByEmailAddress(string emailAddress);
        string GetUserNameOfCurrentSession(string emailAddress);
        int GetUserIDOfCurrentSession();
    }
}
