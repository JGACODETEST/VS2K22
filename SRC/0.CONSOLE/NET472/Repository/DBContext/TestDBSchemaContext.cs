using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET472.Repository.DBContext
{
    public class TestDBSchemaContext : DbContext
    {

        public DbSet<Entities.TestTable1Schema> testTable1s { get; set; }

        public TestDBSchemaContext(DbConnection existingConnection)
            : base(existingConnection, true)
        {
            
        }
    }
}
