using DataAccessLayer.Repo.IRepositories;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DataAccessLayer.Common;
using System.Web;

namespace DataAccessLayer.Repo
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDAL _dataAccessLayer;
        public AccountRepository(IDAL dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }         
        public bool IsUserAuthenticated(UserAccount account)
        {
            string authenticateUserSql = @"SELECT * FROM AccountTemp WHERE EmailAddress = @EmailAddress AND Password = @Password";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@EmailAddress", SqlDbType.VarChar, 100) { Value = account.EmailAddress },
                new SqlParameter("@Password", SqlDbType.VarChar, 30) { Value = account.Password }
            };
            SqlDataReader result = _dataAccessLayer.GetDataUsingParameters(authenticateUserSql, parameters);
            return result.HasRows;
        }
        public bool RegisterAccount(User user)
        {
            string registerAccountSql = @"
                                   DECLARE @AccountTempID INT
                                   DECLARE @ManagerUserID INT
                                   DECLARE @DepartmentID INT
                                   INSERT INTO AccountTemp (EmailAddress, Password) VALUES (@EmailAddress,@Password)
                                   SET @AccountTempID = SCOPE_IDENTITY()
                                   SET @ManagerUserID=(SELECT ManagerUserID FROM ManagerDetails WHERE ManagerName=@ManagerName)
                                   SET @DepartmentID =(SELECT DepartmentID FROM Department WHERE DepartmentName=@DepartmentName)
                                   INSERT INTO UserDetailsTemp (FirstName, LastName, NationalIdentityNumber, PhoneNumber, ManagerUserID, DepartmentID, AccountTempID)
                                   VALUES (@FirstName, @LastName, @NationalIdentityNumber, @PhoneNumber, @ManagerUserID, @DepartmentID, @AccountTempID)
                                   ";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@EmailAddress", SqlDbType.VarChar, 100) { Value = user.EmailAddress},
                new SqlParameter("@Password", SqlDbType.VarChar, 30) { Value = user.Password},
                new SqlParameter("@FirstName", SqlDbType.NVarChar, 128) { Value = user.FirstName},
                new SqlParameter("@LastName", SqlDbType.NVarChar, 128) { Value = user.LastName},
                new SqlParameter("@NationalIdentityNumber", SqlDbType.VarChar, 15) { Value = user.NationalIdentityNumber},
                new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 20) { Value = user.PhoneNumber},
                new SqlParameter("@ManagerName", SqlDbType.NVarChar, 128) { Value = user.ManagerName},
                new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 128) { Value = user.DepartmentName},
             };
            return _dataAccessLayer.InsertData(registerAccountSql, parameters) > 0;
        }

        public int GetAccountIdByEmailAddress(string emailAddress)
        {
            int accountId = 0;
            List<SqlParameter> parameters = new List<SqlParameter>();
            string SelectAccountUsingEmailAddress = @"SELECT AccountTempID FROM AccountTemp WHERE EmailAddress=@emailAddress";
            parameters.Add(new SqlParameter("@emailAddress", emailAddress));
            using (SqlDataReader reader = _dataAccessLayer.GetDataUsingParameters(SelectAccountUsingEmailAddress, parameters))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    accountId = Convert.ToInt32(reader["AccountTempID"]);
                }
            }
            return accountId;
        }

        public string GetUserNameOfCurrentSession(string emailAddress)
        {
            string fullName = "";
            List<SqlParameter> parameters = new List<SqlParameter>();
            string SelectAccountUsingEmailAddress = @"SELECT FirstName, LastName FROM UserDetailsTemp WHERE AccountTempID = @AccountTempID";
            parameters.Add(new SqlParameter("@AccountTempID", HttpContext.Current.Session["accountId"]));
            using (SqlDataReader reader = _dataAccessLayer.GetDataUsingParameters(SelectAccountUsingEmailAddress, parameters))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    string firstName = reader["FirstName"].ToString();
                    string lastName = reader["LastName"].ToString();
                    fullName = $"{firstName} {lastName}";
                }
            }
            return fullName;
        }
 
        public int GetUserIDOfCurrentSession()
        {
            int userIDDoesNotExist = -1;
            int userID = userIDDoesNotExist;
            List<SqlParameter> parameters = new List<SqlParameter>();
            string GetCurrentUserID = @"Select UserTempID FROM UserDetailsTemp  WHERE AccountTempID = @AccountTempID";
            parameters.Add(new SqlParameter("@AccountTempID", HttpContext.Current.Session["accountId"]));
            using (SqlDataReader reader = _dataAccessLayer.GetDataUsingParameters(GetCurrentUserID, parameters))
            {
                if (reader != null && reader.Read())
                {
                    userID = Convert.ToInt32(reader["UserTempID"]);
                }
            }
            return userID;
        }

        public string EmailExists(string email)
        {
            string sql = "SELECT EmailAddress FROM AccountTemp WHERE EmailAddress = @EmailAddress";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@EmailAddress", email) 
            };
            SqlDataReader result = _dataAccessLayer.GetDataUsingParameters(sql, parameters);
            if (result.HasRows)
            {
                result.Read();
                return result["EmailAddress"].ToString();
            }
            return null;
        }

        public string NicExists(string nic)
        {
            string getNicSql = "SELECT NationalIdentityNumber FROM UserDetailsTemp WHERE NationalIdentityNumber = @NationalIdentityNumber";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@NationalIdentityNumber", nic)
            };
            SqlDataReader result = _dataAccessLayer.GetDataUsingParameters(getNicSql, parameters);
            if (result.HasRows)
            {
                result.Read();
                return result["NationalIdentityNumber"].ToString();
            }
            return null;
        }
    }
}


