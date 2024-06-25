using NET6NIVSUP.Repository;
using NET6NIVSUP.Service.Model.Dto;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using NET6NIVSUP.Repository.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.SqlClient;

namespace NET6NIVSUP.Service
{
    public class SQLServerService : IService<TestTable1Dto>
    {
        private SqlConnectionStringBuilder builderSQLServer;

        public SQLServerService()
        {
            builderSQLServer = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = "TESTDBSQL2K19",
                UserID = "SA",
                Password = "sql2K19@",
                IntegratedSecurity = false,
                TrustServerCertificate = true, // solo si es desarrollo esto no debería ir en prod
                PersistSecurityInfo = true
            };

        }

        public TestTable1 ConvertDtoToTestTable1(TestTable1Dto dto)
        {
            var result = new TestTable1();

            result.Id = dto.Id;
            result.Descripcion = dto.Descripcion;

            return result;
        }

        public TestTable1Dto ConvertTestTable1ToDto(TestTable1 entity)
        {
            var result = new TestTable1Dto();

            result.Id = entity.Id;
            result.Descripcion = entity.Descripcion;

            return result;
        }

        public void Listar(HostApplicationBuilder builder)
        {
            using (DbConnection connection = new SqlConnection(builderSQLServer.ConnectionString))
            {
                connection.Open();

                var testTable1Repo = ActivatorUtilities.CreateInstance<TestTableRepo>(builder.Services.BuildServiceProvider(), connection, 0);

                Console.WriteLine("ITEMS SQL: ");

                var testTable1List = testTable1Repo.getAll();

                foreach (var item in testTable1List)
                {
                    Console.WriteLine("- " + item.Id.ToString() + " - " + item.Descripcion);
                }
            }
        }

        public TestTable1Dto Grabar(HostApplicationBuilder builder, TestTable1Dto dto)
        {
            TestTable1Dto result = new TestTable1Dto();

            try
            {
                using (DbConnection connection = new SqlConnection(builderSQLServer.ConnectionString))
                {
                    connection.Open();

                    using (var testTable1Repo = ActivatorUtilities.CreateInstance<TestTableRepo>(builder.Services.BuildServiceProvider(), connection, 0))
                    {
                        if (dto != null && dto.Id == 0) // Save
                        {
                            Console.WriteLine("ITEM SQL - BEFORE SAVE : " + dto.ToString());

                            // TODO Logging info save

                            var testTable1Tmp = testTable1Repo.save(ConvertDtoToTestTable1(dto));

                            Console.WriteLine("ITEM SQL - AFTER SAVE : " + ConvertTestTable1ToDto(testTable1Tmp).ToString());

                            // TODO Logging info save
                        }
                        else if (dto != null && dto.Id != 0) // Update
                        {
                            var testTable1Tmp = testTable1Repo.findOne(ConvertDtoToTestTable1(dto));

                            if (testTable1Tmp != null)
                            {
                                Console.WriteLine("ITEM SQL - BEFORE UPDATE : " + ConvertTestTable1ToDto(testTable1Tmp).ToString());

                                // TODO Logging info before update

                                testTable1Repo.save(ConvertDtoToTestTable1(dto));

                                Console.WriteLine("ITEM SQL - AFTER UPDATE : " + dto.ToString());

                                // TODO Logging info after update
                            }
                            else
                            {
                                result = null;
                                // TODO Logging error
                            }
                        }
                        else
                        {
                            result = null;
                            // TODO Logging error
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                // TODO Logging error
            }

            return result;
        }

        public bool Eliminar(HostApplicationBuilder builder, TestTable1Dto dto)
        {
            bool result = false;

            try
            {
                using (DbConnection connection = new SqlConnection(builderSQLServer.ConnectionString))
                {
                    connection.Open();

                    using (var testTable1Repo = ActivatorUtilities.CreateInstance<TestTableRepo>(builder.Services.BuildServiceProvider(), connection, 0))
                    {

                        Console.WriteLine("ITEM SQL - BEFORE DELETE : " + dto.ToString());

                        // TODO Logging info save

                        result = testTable1Repo.delete(ConvertDtoToTestTable1(dto));

                        // TODO Logging info save                        
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                // TODO Logging error
            }

            return result;
        }
    }
}