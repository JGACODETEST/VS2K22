using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace NET7NIVSUP.Repository.DBContext
{
    public class TestDBSql2K19Context : DbContext
    {

        public DbSet<Entities.TestTable1> testTable1s { get; set; }

        private readonly SqlConnection _existingConnection;

        public TestDBSql2K19Context(SqlConnection existingConnection)
        {
            _existingConnection = existingConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_existingConnection);
        }
    }
}
