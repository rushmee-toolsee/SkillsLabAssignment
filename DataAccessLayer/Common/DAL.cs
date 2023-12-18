using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace DataAccessLayer.Common
{
    public class DAL : IDAL
    {
        private readonly ILogger _logger;
        public DAL(ILogger logger)
        {
            _logger = logger;
        }
        public SqlConnection Connection { get; private set; }
        public void Connect()
        {
            try
            {
                var connectionString = ConfigurationManager.AppSettings["DefaultConnectionString"];
                if (!string.IsNullOrEmpty(connectionString))
                {
                    Connection = new SqlConnection(connectionString);
                    Connection.Open();
                }
            }
            catch (Exception exception)
            {
                _logger.Log(exception.Message);
                throw;
            }
        }

        public void Disconnect()
        {
            if (Connection != null && Connection.State.Equals(ConnectionState.Open))
            {
                try
                {
                    Connection.Close();
                }
                catch (Exception exception)
                {
                    _logger.Log(exception.Message);
                    throw; 
                }
                finally
                {
                    Connection.Dispose();
                }
            }
        }

        public SqlDataReader GetDataUsingParameters(string sql, List<SqlParameter> parameters)
        {
            Connect();
            SqlDataReader reader = null;
            try
            {
                using (SqlCommand command = new SqlCommand(sql))
                {
                    command.Connection = Connection;
                    command.Parameters.AddRange(parameters.ToArray());
                    reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                }
                return reader;
            }
            catch (SqlException slqException)
            {
                _logger.Log(slqException.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.Log(exception.Message);
                throw;
            }
        }
        public SqlDataReader GetData(string sql)
        {
            Connect();
            SqlDataReader reader = null;
            try
            {
                using (SqlCommand command = new SqlCommand(sql))
                {
                    command.Connection = Connection;
                    reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                }
                return reader;
            }
            catch (SqlException slqException)
            {
                _logger.Log(slqException.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.Log(exception.Message);
                throw;
            }
        }


        public int InsertData(string sql, List<SqlParameter> parameters)
        {
            Connect();
            int numberOfRowsAffected = 0;
            try
            {
                using (SqlCommand command = new SqlCommand(sql))
                {
                    command.Connection = Connection;
                    command.Parameters.AddRange(parameters.ToArray());
                    numberOfRowsAffected = command.ExecuteNonQuery();
                }
                return numberOfRowsAffected;

            }
            catch (SqlException slqException)
            {
                _logger.Log(slqException.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.Log(exception.Message);
                throw;
            }
          }
        public void Dispose()
        {
            Disconnect();
        }

    }
}