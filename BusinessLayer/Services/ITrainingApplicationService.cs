
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.IO;

namespace BusinessLayer.Services
{
    public interface ITrainingApplicationService
    {
        bool SubmitTrainingApplication(int trainingId, string fileName, Stream fileStream);
        List<TrainingApplication> GetUserApplications(int userId);
    }
}
