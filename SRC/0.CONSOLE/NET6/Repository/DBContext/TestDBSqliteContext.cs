using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace NET6.Repository.DBContext
{
    public class TestDBSqliteContext : DbContext
    {

        public DbSet<Entities.TestTable1> testTable1s { get; set; }

        private readonly SqliteConnection _existingConnection;

        public TestDBSqliteContext(SqliteConnection existingConnection)
        {
            _existingConnection = existingConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_existingConnection);
        }
    }
}
