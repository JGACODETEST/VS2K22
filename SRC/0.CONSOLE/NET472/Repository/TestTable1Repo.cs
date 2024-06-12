using NET472.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace NET472.Repository
{
    public class TestTable1Repo : ITestTable1Repo
    {
        // Flag to indicate if the object has been disposed.
        private bool _disposed = false;
        private bool _useSchema { get; set; }
        private DbConnection _existingConnection { get; set; }

        public TestTable1Repo(DbConnection existingConnection, bool useSchema)
        {
            this._existingConnection = existingConnection;

            this._useSchema = useSchema;
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

        public TestTable1 findOne(TestTable1 entity)
        {
            var result = new TestTable1();

            try
            {
                if (!this._useSchema)
                {
                    using (var context = new DBContext.TestDBContext(this._existingConnection))
                    {
                        result = context.testTable1s.Where(x => x.Id == entity.Id).FirstOrDefault();
                    }
                }
                else
                {
                    using (var context = new DBContext.TestDBSchemaContext(this._existingConnection))
                    {
                        result = (from x in context.testTable1s.ToList()
                                  where x.Id == entity.Id
                                  select new TestTable1()
                                  {
                                      Id = x.Id,
                                      Descripcion = x.Descripcion
                                  }).FirstOrDefault();
                    }
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
                if (!this._useSchema)
                {
                    using (var context = new DBContext.TestDBContext(this._existingConnection))
                    {
                        using (var dbContextTransaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                if (entity.Id == 0)
                                {
                                    result = context.testTable1s.Add(entity);
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
                }
                else
                {
                    using (var context = new DBContext.TestDBSchemaContext(this._existingConnection))
                    {
                        using (var dbContextTransaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                if (entity.Id == 0)
                                {
                                    result = ConvertSchemaToTestTable1(context.testTable1s.Add(ConvertTestTable1ToSchema(entity)));
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
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Free any other managed objects here.
                // ...
            }

            // Free any unmanaged objects here.
            // ...

            _disposed = true;
        }

        ~TestTable1Repo() {
            // Finalizer calls Dispose(false)
            Dispose(true);
        }
    }
}