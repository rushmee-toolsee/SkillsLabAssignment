using DataAccessLayer.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DataAccessLayer.Common;


namespace DataAccessLayer.Repo.ActualRepositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDAL _dataAccessLayer;
        public DepartmentRepository(IDAL dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        public int GetDepartmentIdByName(string departmentName)
        {
            int departmentId = 0;
            string query = "SELECT DepartmentID FROM Department WHERE DepartmentName = @DepartmentName";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 128) { Value = departmentName }
            };
            using (SqlDataReader reader = _dataAccessLayer.GetDataUsingParameters(query, parameters))
            {
                if (reader.Read())
                {
                    departmentId = Convert.ToInt32(reader["DepartmentID"]);
                }
            }
            return departmentId;
        }

        public List<string> GetManagersByDepartmentId(int departmentId)
        {
            List<string> managers = new List<string>();
            string query = "SELECT ManagerName FROM ManagerDetails WHERE DepartmentID = @DepartmentID";
            List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@DepartmentID", SqlDbType.Int) { Value = departmentId }
                };
            using (SqlDataReader reader = _dataAccessLayer.GetDataUsingParameters(query, parameters))
            {
                while (reader.Read())
                {
                    managers.Add(reader["ManagerName"].ToString());
                }
            }
            return managers;
        }
    }
}


