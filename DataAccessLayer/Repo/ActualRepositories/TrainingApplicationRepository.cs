using DataAccessLayer.Common;
using DataAccessLayer.Models;
using DataAccessLayer.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer.Repo
{
    public class TrainingApplicationRepository : ITrainingApplicationRepository
    {
        private readonly IDAL _dataAccessLayer;
        private readonly IAccountRepository _accountRepository;
        public TrainingApplicationRepository(IDAL dataAccessLayer, IAccountRepository accountRepository)
        {
            _dataAccessLayer = dataAccessLayer;
            _accountRepository = accountRepository;
        }
         
        public bool SaveApplication(int trainingId, string fileName, Stream fileStream)
        {
            string uploadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            string filePath = Path.Combine(uploadPath, uniqueFileName);
            using (var fileStreamLocal = File.Create(filePath))
            {
                fileStream.CopyTo(fileStreamLocal);
            }
            string registerAccountSql = @"INSERT INTO ApplicationsTemp (ApplicationStatus, SubmissionDate, ManagerApprovalStatus, DeclineReason, UserID, TrainingID, AttachmentPath) VALUES ('Pending', GETDATE(), 'Pending', null, @userID, @trainingId, @attachmentPath)";

            int currentUserID = _accountRepository.GetUserIDOfCurrentSession();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@userID", currentUserID));
            parameters.Add(new SqlParameter("@trainingId", trainingId));
            parameters.Add(new SqlParameter("@attachmentPath", filePath));

            return _dataAccessLayer.InsertData(registerAccountSql, parameters) > 0;
        }

        public List<TrainingApplication> GetUserApplications(int userId)
        {
            List<TrainingApplication> applications = new List<TrainingApplication>();
            string sql = "SELECT A.*, T.Title AS TrainingTitle FROM  ApplicationsTemp A JOIN  Training T ON A.TrainingID = T.TrainingID  WHERE   A.UserID = @UserId";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserId", userId)
            };
            using (SqlDataReader reader = _dataAccessLayer.GetDataUsingParameters(sql, parameters))
            {
                if (!reader.HasRows) return applications;
                while (reader.Read())
                {
                    TrainingApplication application = new TrainingApplication
                    {
                        ApplicationId = reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                        ApplicationStatus = reader.GetString(reader.GetOrdinal("ApplicationStatus")),
                        SubmissionDate = reader.GetDateTime(reader.GetOrdinal("SubmissionDate")),
                        ManagerApprovalStatus = reader.GetString(reader.GetOrdinal("ManagerApprovalStatus")),
                        DeclineReason = reader.IsDBNull(reader.GetOrdinal("DeclineReason"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("DeclineReason")),
                        UserID = reader.GetInt32(reader.GetOrdinal("UserId")),
                        TrainingId = reader.GetInt32(reader.GetOrdinal("TrainingId")),
                        AttachmentPath = reader.IsDBNull(reader.GetOrdinal("AttachmentPath"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("AttachmentPath")),
                    TrainingTitle = reader.GetString(reader.GetOrdinal("TrainingTitle"))

                    };
                    applications.Add(application);

                }
            }
            return applications;
        }
    }
}