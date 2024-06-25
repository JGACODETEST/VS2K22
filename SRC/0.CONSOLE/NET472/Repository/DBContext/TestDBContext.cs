using System.Data.Common;
using System.Data.Entity;

namespace NET472.Repository.DBContext
{
    public class TestDBContext : DbContext
    {
        public DbSet<Entities.TestTable1> testTable1s { get; set; }

        public TestDBContext(DbConnection existingConnection)
            : base(existingConnection, false)
        {
        }
    }
}