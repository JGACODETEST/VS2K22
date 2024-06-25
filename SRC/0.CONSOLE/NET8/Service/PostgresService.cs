using MySql.Data.MySqlClient;
using NET8.Repository;
using NET8.Service.Model.Dto;
using Npgsql;
using System;
using System.Data.Common;
using NET8.Repository.Entities;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NET8.Service
{
    public class PostgresService : IService<TestTable1Dto>
    {
        private NpgsqlConnectionStringBuilder builderPostgres;

        public PostgresService()
        {
            builderPostgres = new NpgsqlConnectionStringBuilder
            {
                Host = "localhost",
                Database = "TESTDBPOSTGRES13",
                Username = "postgres",
                Password = "postgre@2K24",
                Port = 5432,
                SslMode = SslMode.Prefer,
                SearchPath = "public",
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
            using (DbConnection connection = new NpgsqlConnection(builderPostgres.ConnectionString))
            {
                connection.Open();

                var testTable1Repo = ActivatorUtilities.CreateInstance<TestTableRepo>(builder.Services.BuildServiceProvider(), connection, 2);

                Console.WriteLine("ITEMS POSTGRES: ");

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
                using (DbConnection connection = new NpgsqlConnection(builderPostgres.ConnectionString))
                {
                    connection.Open();

                    using (var testTable1Repo = ActivatorUtilities.CreateInstance<TestTableRepo>(builder.Services.BuildServiceProvider(), connection, 2))
                    {
                        if (dto != null && dto.Id == 0) // Save
                        {
                            Console.WriteLine("ITEM POSTGRES - BEFORE SAVE : " + dto.ToString());

                            // TODO Logging info save

                            var testTable1Tmp = testTable1Repo.save(ConvertDtoToTestTable1(dto));

                            Console.WriteLine("ITEM POSTGRES - AFTER SAVE : " + ConvertTestTable1ToDto(testTable1Tmp).ToString());

                            // TODO Logging info save
                        }
                        else if (dto != null && dto.Id != 0) // Update
                        {
                            var testTable1Tmp = testTable1Repo.findOne(ConvertDtoToTestTable1(dto));

                            if (testTable1Tmp != null)
                            {
                                Console.WriteLine("ITEM POSTGRES - BEFORE UPDATE : " + ConvertTestTable1ToDto(testTable1Tmp).ToString());

                                // TODO Logging info before update

                                testTable1Repo.save(ConvertDtoToTestTable1(dto));

                                Console.WriteLine("ITEM POSTGRES - AFTER UPDATE : " + dto.ToString());

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
                using (DbConnection connection = new NpgsqlConnection(builderPostgres.ConnectionString))
                {
                    connection.Open();

                    using (var testTable1Repo = ActivatorUtilities.CreateInstance<TestTableRepo>(builder.Services.BuildServiceProvider(), connection, 2))
                    {

                        Console.WriteLine("ITEM POSTGRES - BEFORE DELETE : " + dto.ToString());

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