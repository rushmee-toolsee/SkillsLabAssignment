
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.IO;

namespace DataAccessLayer.Repo.IRepositories
{
    public interface ITrainingApplicationRepository
    {
        bool SaveApplication(int trainingId, string fileName, Stream fileStream);
        List<TrainingApplication> GetUserApplications(int userId);
    }
}
