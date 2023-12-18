using BusinessLayer.Validators;
using DataAccessLayer.Models;
using DataAccessLayer.Repo.IRepositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BusinessLayer.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRegistrationValidator _registrationValidator;

        public RegisterService(IAccountRepository accountRepository, IRegistrationValidator registrationValidator)
        {
            _accountRepository = accountRepository;
            _registrationValidator = registrationValidator;
        }

        public List<ValidationResult> AccountRegistered(User user)
        {
            List<ValidationResult> result = _registrationValidator.ValidateRegistration(user);
            if (result.Count == 0)
            {
                _accountRepository.RegisterAccount(user);
            }
            return result;
        }
    }
}