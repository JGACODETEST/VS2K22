using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Npgsql;

namespace NET7NIVSUP.Repository.DBContext
{
    public class TestDBPostgresContext : DbContext
    {

        public DbSet<Entities.TestTable1Schema> testTable1s { get; set; }

        private readonly NpgsqlConnection _existingConnection;

        public TestDBPostgresContext(NpgsqlConnection existingConnection)
        {
            _existingConnection = existingConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_existingConnection);
        }
    }
}
