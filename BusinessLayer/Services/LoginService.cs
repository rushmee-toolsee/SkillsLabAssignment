using DataAccessLayer.Repo.IRepositories;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessLayer.Validators;

namespace BusinessLayer.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILoginValidator _loginValidator;

        public LoginService(IAccountRepository accountRepository, ILoginValidator loginValidator)
        {
            _accountRepository = accountRepository;
            _loginValidator = loginValidator;
        }

        public List<ValidationResult> LoggedIn(UserAccount account)
        {
            List<ValidationResult> result = _loginValidator.ValidateLogin(account);
            if (result.Count > 0)
                return result;
            if (!_accountRepository.IsUserAuthenticated(account))
                result.Add(new ValidationResult("Unable to authenticate user!"));
            return result;
        }
    }
}
