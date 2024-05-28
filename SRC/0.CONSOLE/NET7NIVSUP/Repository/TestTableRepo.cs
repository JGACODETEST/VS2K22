using NET7NIVSUP.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NET7NIVSUP.Repository
{
    public class TestTableRepo : ITestTable1Repo
    {

        private DbConnection _existingConnection { get; set; }
        private int _dbConnectionType { get; set; } // 0 = SQL Server | 1 = MySQL

        public TestTableRepo(DbConnection existingConnection, int dbConnectionType)
        {
            this._existingConnection = existingConnection;
            this._dbConnectionType = dbConnectionType;
        }

        public List<TestTable1> getAll()
        {
            var result = new List<TestTable1>();

            try
            {                
                switch (this._dbConnectionType)
                {
                    case 0:
                        using (var context = new DBContext.TestDBSql2K19Context(new Microsoft.Data.SqlClient.SqlConnection( this._existingConnection.ConnectionString)))
                        {
                            result = context.testTable1s.ToList();
                        }

                        break;

                    case 1:
                        using (var context = new DBContext.TestDBMySQLContext(new MySql.Data.MySqlClient.MySqlConnection(this._existingConnection.ConnectionString)))
                        {
                            result = context.testTable1s.ToList();
                        }

                        break;

                    default:
                        break;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return result;
        }
    }
}
