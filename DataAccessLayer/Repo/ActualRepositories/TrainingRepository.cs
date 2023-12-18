using DataAccessLayer.Common;
using DataAccessLayer.Models;
using DataAccessLayer.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer.Repo
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly IDAL _dataAccessLayer;

        public TrainingRepository(IDAL dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        public List<Training> GetTraining()
        {
            List<Training> training = new List<Training>();
            string sql = "SELECT * FROM Training";
            using (SqlDataReader reader = _dataAccessLayer.GetData(sql))
            {
                if (!reader.HasRows) return training;  
                while (reader.Read())
                {
                    Training trainingItem = new Training();
                    foreach (var property in typeof(Training).GetProperties())
                    {
                        string columnName = property.Name;
                        if (columnName != null && columnName != "")
                        {
                            var value = reader[columnName] == DBNull.Value ? null : reader[columnName];
                            property.SetValue(trainingItem, value);
                        }
                    }
                    training.Add(trainingItem);
                }
            }
            return training;
        }

        public Training GetTrainingById(int trainingId)
        {
            Training trainingItem = null;            
            string sql = $"SELECT * FROM Training WHERE TrainingId = @TrainingId";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingId", trainingId));
            using (SqlDataReader reader  = _dataAccessLayer.GetDataUsingParameters(sql, parameters))
            {
                if (!reader.HasRows) return trainingItem;
                while (reader.Read())
                {
                    trainingItem = new Training();
                    foreach (var property in typeof(Training).GetProperties())
                    {
                        string columnName = property.Name;
                        if (columnName != null && columnName != "")
                        {
                            var value = reader[columnName] == DBNull.Value
                                ? null
                                : reader[columnName];
                            property.SetValue(trainingItem, value);
                        }
                    }
                }
            }                        
            return trainingItem;
        }
    }
}