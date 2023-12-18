using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Common
{
    public interface IDAL : IDisposable
    {
        void Connect();
        void Disconnect();
        SqlDataReader GetDataUsingParameters(string sql, List<SqlParameter> parameters);
        SqlDataReader GetData(string sql);
        int InsertData(string query, List<SqlParameter> parameters);



    }
}
