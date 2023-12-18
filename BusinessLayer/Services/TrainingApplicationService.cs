using DataAccessLayer.Models;
using DataAccessLayer.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLayer.Services
{
    public class TrainingApplicationService : ITrainingApplicationService
    {
        private readonly ITrainingApplicationRepository _trainingApplicationRepository;
        public TrainingApplicationService(ITrainingApplicationRepository trainingApplicationRepository)
        {
            _trainingApplicationRepository = trainingApplicationRepository;
        }
        public bool SubmitTrainingApplication(int trainingId, string fileName, Stream fileStream)
        {
            return _trainingApplicationRepository.SaveApplication(trainingId, fileName, fileStream);
        }
        public List<TrainingApplication> GetUserApplications(int userId)
        {
            return _trainingApplicationRepository.GetUserApplications(userId);
        }
    }
}
