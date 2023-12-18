using DataAccessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface ITrainingService
    {
        List<Training> RetrieveTraining();
        Training RetrieveTrainingById(int trainingId);
    }
}
