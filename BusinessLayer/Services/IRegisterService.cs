using DataAccessLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Services
{
    public interface IRegisterService
    {
        List<ValidationResult> AccountRegistered(User user);
    }
}
