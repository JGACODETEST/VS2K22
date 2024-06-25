using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace NET7.Repository.DBContext
{
    public class TestDBMySQLContext : DbContext
    {

        public DbSet<Entities.TestTable1> testTable1s { get; set; }

        private readonly MySqlConnection _existingConnection;

        public TestDBMySQLContext(MySqlConnection existingConnection)
        {
            _existingConnection = existingConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_existingConnection);
        }
    }
}
