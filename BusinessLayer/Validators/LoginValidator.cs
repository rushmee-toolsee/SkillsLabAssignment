using DataAccessLayer.Models;
using DataAccessLayer.Repo.IRepositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace BusinessLayer.Validators
{
    public class LoginValidator : ILoginValidator
    {
        private readonly IAccountRepository _accountRepository;

        public LoginValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public List<ValidationResult> ValidateLogin(UserAccount account)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidateRequiredField(account.EmailAddress, "Email Address is required", "account.EmailAddress", results);
            ValidateEmailFormat(account.EmailAddress, "Invalid Email Address", results);
            ValidateRequiredField(account.Password, "Password is required", "account.Password", results);
            //ValidatePasswordComplexity(account.Password, "Password must be at least 8 characters long and include both letters and numbers", results);
            return results;
        }

        private void ValidateRequiredField(string value, string errorMessage, string memberName, List<ValidationResult> results)
        {
            if (string.IsNullOrEmpty(value))
                results.Add(new ValidationResult(errorMessage, new[] { memberName }));
        }

        private void ValidateEmailFormat(string email, string errorMessage, List<ValidationResult> results)
        {
            if (!string.IsNullOrEmpty(email) && !IsValidEmail(email))
                results.Add(new ValidationResult(errorMessage));
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }

        private void ValidatePasswordComplexity(string password, string errorMessage, List<ValidationResult> results)
        {
            if (password.Length < 8 || !password.Any(char.IsLetter) || !password.Any(char.IsDigit))
            {
                results.Add(new ValidationResult(errorMessage));
            }
        }
    }
}
