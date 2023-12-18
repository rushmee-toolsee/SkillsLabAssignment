using DataAccessLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Services
{
    public interface ILoginService
    {
        List<ValidationResult> LoggedIn(UserAccount account);
    }
}
