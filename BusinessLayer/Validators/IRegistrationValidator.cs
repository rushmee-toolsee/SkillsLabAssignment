using DataAccessLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Validators
{
    public interface IRegistrationValidator
    {
        List<ValidationResult> ValidateRegistration(User user);
        void ValidateNic(string nic, List<ValidationResult> results);
        void ValidateEmail(string email, List<ValidationResult> results);
        void ValidateMatchedFields(string value, string compareValue, string errorMessage, List<ValidationResult> results);
        void ValidateRequiredField(string value, string errorMessage, string memberName, List<ValidationResult> results);
    }
}
