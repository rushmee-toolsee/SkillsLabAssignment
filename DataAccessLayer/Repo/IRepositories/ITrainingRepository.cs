using DataAccessLayer.Models;
using System.Collections.Generic;
namespace DataAccessLayer.Repo.IRepositories
{
    public interface ITrainingRepository
    {
        List<Training> GetTraining();
        Training GetTrainingById(int trainingId);
    }
}
