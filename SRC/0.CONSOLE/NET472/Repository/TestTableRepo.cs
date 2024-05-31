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
        private bool _useSchema { get; set; }
        private DbConnection _existingConnection { get; set; }

        public TestTableRepo(DbConnection existingConnection, bool useSchema)
        {
            this._existingConnection = existingConnection;

            this._useSchema = useSchema;
        }

        public List<TestTable1> getAll()
        {
            var result = new List<TestTable1>();

            try
            { 
                if (!this._useSchema)
                {
                    using (var context = new DBContext.TestDBContext(this._existingConnection))
                    {
                        result = context.testTable1s.ToList();
                    }
                }
                else
                {
                    using (var context = new DBContext.TestDBSchemaContext(this._existingConnection))
                    {
                        result = (from x in context.testTable1s.ToList()
                                  select new TestTable1()
                                  {
                                      Id = x.Id,
                                      Descripcion = x.Descripcion
                                  }).ToList();
                    }
                    
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
