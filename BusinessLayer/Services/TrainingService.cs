using DataAccessLayer.Models;
using DataAccessLayer.Repo.IRepositories;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        public TrainingService(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

       public List<Training> RetrieveTraining()
        {
            return _trainingRepository.GetTraining();
        }
        public Training RetrieveTrainingById(int trainingId)
        {
            return _trainingRepository.GetTrainingById( trainingId);
        }

    }
}
