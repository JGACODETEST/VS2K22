using NET472.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET472.Repository
{
    public class TestTableRepo : ITestTable1Repo
    {
        private DbConnection _existingConnection { get; set; }

        public TestTableRepo(DbConnection existingConnection)
        {
            this._existingConnection = existingConnection;
        }

        public List<TestTable1> getAll()
        {
            var result = new List<TestTable1>();

            try
            {
                using (var context = new DBContext.TestDBContext(this._existingConnection))
                {
                    result = context.testTable1s.ToList();
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
