using DataAccessLayer.Models;
using DataAccessLayer.Repo.IRepositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BusinessLayer.Validators
{
    public class RegistrationValidator : IRegistrationValidator
    {
        private readonly IAccountRepository _accountRepository;

        public RegistrationValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public List<ValidationResult> ValidateRegistration(User user)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidateRequiredField(user.FirstName, "First Name is required", "user.FirstName", results);
            ValidateRequiredField(user.LastName, "Last Name is required", "user.LastName", results);
            ValidateRequiredField(user.Password, "Please enter a password", null, results);
            ValidateRequiredField(user.ConfirmPassword, "Please confirm your password.", null, results);
            ValidateMatchedFields(user.Password, user.ConfirmPassword, "The passwords you entered do not match.", results);
            ValidateEmail(user.EmailAddress, results);
            ValidateRequiredField(user.NationalIdentityNumber, "Please enter a National Identity Number.", null, results);
            ValidateNic(user.NationalIdentityNumber, results);
            ValidateRequiredField(user.PhoneNumber, "Please enter a phone number.", null, results);

            return results;
        }

        public void ValidateRequiredField(string value, string errorMessage, string memberName, List<ValidationResult> results)
        {
            if (string.IsNullOrEmpty(value))
                results.Add(new ValidationResult(errorMessage, new[] { memberName }));
        }

        public void ValidateMatchedFields(string value, string compareValue, string errorMessage, List<ValidationResult> results)
        {
            if (!string.IsNullOrEmpty(value) && !value.Equals(compareValue))
                results.Add(new ValidationResult(errorMessage));
        }

        public void ValidateEmail(string email, List<ValidationResult> results)
        {
            Regex EmailRegex = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            string existingEmail = _accountRepository.EmailExists(email);

            ValidateRequiredField(email, "Please enter an email address.", null, results);
            if (!EmailRegex.IsMatch(email))
                results.Add(new ValidationResult("Invalid Email Address!"));
            else if (existingEmail != null)
                results.Add(new ValidationResult("The email address provided is already associated with an existing user account."));
        }

        public void ValidateNic(string nic, List<ValidationResult> results)
        {
            string existingNic = _accountRepository.NicExists(nic);

            ValidateRequiredField(nic, "Please enter a National Identity Number.", null, results);
            if (existingNic != null)
                results.Add(new ValidationResult("The National Identity Number provided is already associated with an existing user account."));
        }
 
    }
}
