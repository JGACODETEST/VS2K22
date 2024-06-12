using System.Data.Common;
using System.Data.Entity;

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