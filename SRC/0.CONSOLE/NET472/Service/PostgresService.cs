using MySql.Data.MySqlClient;
using NET472.Repository;
using NET472.Service.Model.Dto;
using Npgsql;
using System;
using System.Data.Common;
using Unity;
using Unity.Resolution;
using NET472.Repository.Entities;

namespace NET472.Service
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
                SearchPath = "public"
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

        public void Listar(IUnityContainer container)
        {
            using (DbConnection connection = new NpgsqlConnection(builderPostgres.ConnectionString))
            {
                connection.Open();

                var testTable1Repo = container.Resolve<ITestTable1Repo>(
                    new ParameterOverride("existingConnection", connection),
                    new ParameterOverride("useSchema", true)
                );

                Console.WriteLine("ITEMS POSTGRES: ");

                var testTable1List = testTable1Repo.getAll();

                foreach (var item in testTable1List)
                {
                    Console.WriteLine("- " + item.Id.ToString() + " - " + item.Descripcion);
                }
            }
        }

        public TestTable1Dto Grabar(IUnityContainer container, TestTable1Dto dto)
        {
            TestTable1Dto result = new TestTable1Dto();

            try
            {
                using (DbConnection connection = new NpgsqlConnection(builderPostgres.ConnectionString))
                {
                    connection.Open();

                    using (var testTable1Repo = container.Resolve<ITestTable1Repo>(
                        new ParameterOverride("existingConnection", connection),
                        new ParameterOverride("useSchema", true)
                    ))
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
    }
}