using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET472.Repository.DBContext
{
    public class TestDBContext : DbContext
    {

        public DbSet<Entities.TestTable1> testTable1s { get; set; }

        public TestDBContext(DbConnection existingConnection)
            : base(existingConnection, true)
        {
            
        }
    }
}
