using NET8NIVSUP.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NET8NIVSUP.Repository
{
    public class TestTableRepo : ITestTable1Repo
    {
        private bool disposedValue;

        private DbConnection _existingConnection { get; set; }
        private int _dbConnectionType { get; set; } // 0 = SQL Server | 1 = MySQL

        public TestTableRepo(DbConnection existingConnection, int dbConnectionType)
        {
            this._existingConnection = existingConnection;
            this._dbConnectionType = dbConnectionType;
        }


        private TestTable1Schema ConvertTestTable1ToSchema(TestTable1 entity)
        {
            var result = new TestTable1Schema();

            result.Id = entity.Id;
            result.Descripcion = entity.Descripcion;

            return result;
        }

        private TestTable1 ConvertSchemaToTestTable1(TestTable1Schema entity)
        {
            var result = new TestTable1();

            result.Id = entity.Id;
            result.Descripcion = entity.Descripcion;

            return result;
        }


        public List<TestTable1> getAll()
        {
            var result = new List<TestTable1>();

            try
            {                
                switch (this._dbConnectionType)
                {
                    case 0:
                        using (var context = new DBContext.TestDBSql2K19Context(new Microsoft.Data.SqlClient.SqlConnection( this._existingConnection.ConnectionString)))
                        {
                            result = context.testTable1s.ToList();
                        }

                        break;

                    case 1:
                        using (var context = new DBContext.TestDBMySQLContext(new MySql.Data.MySqlClient.MySqlConnection(this._existingConnection.ConnectionString)))
                        {
                            result = context.testTable1s.ToList();
                        }

                        break;

                    case 2:
                        using (var context = new DBContext.TestDBPostgresContext(new Npgsql.NpgsqlConnection(this._existingConnection.ConnectionString)))
                        {
                            result = (from x in context.testTable1s
                                      select new TestTable1()
                                      {
                                          Id = x.Id,
                                          Descripcion = x.Descripcion
                                      }).ToList();
                        }

                        break;
                    case 3:
                        using (var context = new DBContext.TestDBSqliteContext(new Microsoft.Data.Sqlite.SqliteConnection(this._existingConnection.ConnectionString)))
                        {
                            result = context.testTable1s.ToList();
                        }

                        break;
                    default:
                        break;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return result;
        }

        public TestTable1 findOne(TestTable1 entity)
        {
            var result = new TestTable1();

            try
            {
                switch (this._dbConnectionType)
                {
                    case 0:
                        using (var context = new DBContext.TestDBSql2K19Context(new Microsoft.Data.SqlClient.SqlConnection(this._existingConnection.ConnectionString)))
                        {
                            result = context.testTable1s.Where(x => x.Id == entity.Id).FirstOrDefault();
                        }

                        break;

                    case 1:
                        using (var context = new DBContext.TestDBMySQLContext(new MySql.Data.MySqlClient.MySqlConnection(this._existingConnection.ConnectionString)))
                        {
                            result = context.testTable1s.Where(x => x.Id == entity.Id).FirstOrDefault();
                        }

                        break;

                    case 2:
                        using (var context = new DBContext.TestDBPostgresContext(new Npgsql.NpgsqlConnection(this._existingConnection.ConnectionString)))
                        {
                            result = (from x in context.testTable1s
                                      where x.Id == entity.Id
                                      select new TestTable1()
                                      {
                                          Id = x.Id,
                                          Descripcion = x.Descripcion
                                      }).FirstOrDefault();
                        }

                        break;

                    case 3:
                        using (var context = new DBContext.TestDBSqliteContext(new Microsoft.Data.Sqlite.SqliteConnection(this._existingConnection.ConnectionString)))
                        {
                            result = context.testTable1s.Where(x => x.Id == entity.Id).FirstOrDefault();
                        }

                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public TestTable1 save(TestTable1 entity)
        {
            var result = new TestTable1();

            try
            {
                switch (this._dbConnectionType)
                {
                    case 0:
                        using (var context = new DBContext.TestDBSql2K19Context(new Microsoft.Data.SqlClient.SqlConnection(this._existingConnection.ConnectionString)))
                        {
                            using (var dbContextTransaction = context.Database.BeginTransaction())
                            {
                                try
                                {
                                    if (entity.Id == 0)
                                    {
                                        result = context.testTable1s.Add(entity).Entity;
                                    }
                                    else if (entity.Id != 0)
                                    {
                                        var resultTmp = context.testTable1s.Where(x => x.Id.Equals(entity.Id)).FirstOrDefault();

                                        if (resultTmp != null)
                                        {
                                            resultTmp.Descripcion = entity.Descripcion;

                                            result = resultTmp;
                                        }
                                        else
                                            result = null;
                                    }

                                    context.SaveChanges();
                                    dbContextTransaction.Commit();
                                }
                                catch (Exception ex)
                                {
                                    dbContextTransaction.Rollback();

                                    result = null;
                                    // TODO Logging error
                                }
                            }
                        }

                        break;

                    case 1:
                        using (var context = new DBContext.TestDBMySQLContext(new MySql.Data.MySqlClient.MySqlConnection(this._existingConnection.ConnectionString)))
                        {
                            using (var dbContextTransaction = context.Database.BeginTransaction())
                            {
                                try
                                {
                                    if (entity.Id == 0)
                                    {
                                        result = context.testTable1s.Add(entity).Entity;
                                    }
                                    else if (entity.Id != 0)
                                    {
                                        var resultTmp = context.testTable1s.Where(x => x.Id.Equals(entity.Id)).FirstOrDefault();

                                        if (resultTmp != null)
                                        {
                                            resultTmp.Descripcion = entity.Descripcion;

                                            result = resultTmp;
                                        }
                                        else
                                            result = null;
                                    }

                                    context.SaveChanges();
                                    dbContextTransaction.Commit();
                                }
                                catch (Exception ex)
                                {
                                    dbContextTransaction.Rollback();

                                    result = null;
                                    // TODO Logging error
                                }
                            }
                        }

                        break;

                    case 2:
                        using (var context = new DBContext.TestDBPostgresContext(new Npgsql.NpgsqlConnection(this._existingConnection.ConnectionString)))
                        {
                            using (var dbContextTransaction = context.Database.BeginTransaction())
                            {
                                try
                                {
                                    if (entity.Id == 0)
                                    {
                                        result = ConvertSchemaToTestTable1(context.testTable1s.Add(ConvertTestTable1ToSchema(entity)).Entity);
                                    }
                                    else if (entity.Id != 0)
                                    {
                                        var resultTmp = context.testTable1s.Where(x => x.Id.Equals(entity.Id)).FirstOrDefault();

                                        if (resultTmp != null)
                                        {
                                            resultTmp.Descripcion = entity.Descripcion;

                                            result = ConvertSchemaToTestTable1(resultTmp);
                                        }
                                        else
                                            result = null;
                                    }

                                    context.SaveChanges();
                                    dbContextTransaction.Commit();
                                }
                                catch (Exception ex)
                                {
                                    dbContextTransaction.Rollback();

                                    result = null;
                                    // TODO Logging error
                                }
                            }

                        }

                        break;

                    case 3:
                        using (var context = new DBContext.TestDBSqliteContext(new Microsoft.Data.Sqlite.SqliteConnection(this._existingConnection.ConnectionString)))
                        {
                            using (var dbContextTransaction = context.Database.BeginTransaction())
                            {
                                try
                                {
                                    if (entity.Id == 0)
                                    {
                                        result = context.testTable1s.Add(entity).Entity;
                                    }
                                    else if (entity.Id != 0)
                                    {
                                        var resultTmp = context.testTable1s.Where(x => x.Id.Equals(entity.Id)).FirstOrDefault();

                                        if (resultTmp != null)
                                        {
                                            resultTmp.Descripcion = entity.Descripcion;

                                            result = resultTmp;
                                        }
                                        else
                                            result = null;
                                    }

                                    context.SaveChanges();
                                    dbContextTransaction.Commit();
                                }
                                catch (Exception ex)
                                {
                                    dbContextTransaction.Rollback();

                                    result = null;
                                    // TODO Logging error
                                }
                            }
                        }

                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public bool delete(TestTable1 entity)
        {
            var result = false;

            try
            {
                switch (this._dbConnectionType)
                {
                    case 0:
                        using (var context = new DBContext.TestDBSql2K19Context(new Microsoft.Data.SqlClient.SqlConnection(this._existingConnection.ConnectionString)))
                        {
                            using (var dbContextTransaction = context.Database.BeginTransaction())
                            {
                                try
                                {
                                    if (entity.Id != 0)
                                    {
                                        var resultTmp = context.testTable1s.Where(x => x.Id.Equals(entity.Id)).FirstOrDefault();

                                        if (resultTmp != null)
                                        {
                                            context.testTable1s.Remove(resultTmp);

                                            context.SaveChanges();
                                            dbContextTransaction.Commit();

                                            result = true;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    dbContextTransaction.Rollback();

                                    result = false;
                                    // TODO Logging error
                                }
                            }
                        }

                        break;

                    case 1:
                        using (var context = new DBContext.TestDBMySQLContext(new MySql.Data.MySqlClient.MySqlConnection(this._existingConnection.ConnectionString)))
                        {
                            using (var dbContextTransaction = context.Database.BeginTransaction())
                            {
                                try
                                {
                                    if (entity.Id != 0)
                                    {
                                        var resultTmp = context.testTable1s.Where(x => x.Id.Equals(entity.Id)).FirstOrDefault();

                                        if (resultTmp != null)
                                        {
                                            context.testTable1s.Remove(resultTmp);

                                            context.SaveChanges();
                                            dbContextTransaction.Commit();

                                            result = true;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    dbContextTransaction.Rollback();

                                    result = false;
                                    // TODO Logging error
                                }
                            }
                        }

                        break;

                    case 2:
                        using (var context = new DBContext.TestDBPostgresContext(new Npgsql.NpgsqlConnection(this._existingConnection.ConnectionString)))
                        {
                            using (var dbContextTransaction = context.Database.BeginTransaction())
                            {
                                try
                                {
                                    if (entity.Id != 0)
                                    {
                                        var resultTmp = context.testTable1s.Where(x => x.Id.Equals(entity.Id)).FirstOrDefault();

                                        if (resultTmp != null)
                                        {
                                            context.testTable1s.Remove(resultTmp);

                                            context.SaveChanges();
                                            dbContextTransaction.Commit();

                                            result = true;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    dbContextTransaction.Rollback();

                                    result = false;
                                    // TODO Logging error
                                }
                            }

                        }

                        break;

                    case 3:
                        using (var context = new DBContext.TestDBSqliteContext(new Microsoft.Data.Sqlite.SqliteConnection(this._existingConnection.ConnectionString)))
                        {
                            using (var dbContextTransaction = context.Database.BeginTransaction())
                            {
                                try
                                {
                                    if (entity.Id != 0)
                                    {
                                        var resultTmp = context.testTable1s.Where(x => x.Id.Equals(entity.Id)).FirstOrDefault();

                                        if (resultTmp != null)
                                        {
                                            context.testTable1s.Remove(resultTmp);

                                            context.SaveChanges();
                                            dbContextTransaction.Commit();

                                            result = true;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    dbContextTransaction.Rollback();

                                    result = false;
                                    // TODO Logging error
                                }
                            }
                        }

                        break;

                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: eliminar el estado administrado (objetos administrados)
                }

                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
}
