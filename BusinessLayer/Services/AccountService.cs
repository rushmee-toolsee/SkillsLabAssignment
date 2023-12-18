using DataAccessLayer.Repo.IRepositories;

namespace BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public int GetLoginAccountID(string emailAddress)
        {
            return _accountRepository.GetAccountIdByEmailAddress(emailAddress);
        }
        public string GetUsername(string emailAddress)
        {
            return _accountRepository.GetUserNameOfCurrentSession(emailAddress);
        }
        public int GetUserId()
        {
            return _accountRepository.GetUserIDOfCurrentSession();
        }
    }
}
