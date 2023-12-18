using DataAccessLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BusinessLayer.Validators
{
    public interface ILoginValidator
    {
        List<ValidationResult> ValidateLogin(UserAccount account);
    }
}
