using MySql.Data.MySqlClient;
using NET7.Repository;
using NET7.Service.Model.Dto;
using System;
using System.Data.Common;
using NET7.Repository.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace NET7.Service
{
    public class MariaDBService : IService<TestTable1Dto>
    {
        private MySqlConnectionStringBuilder builderMariaDB;

        public MariaDBService()
        {
            builderMariaDB = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "TESTDBMARIADB1011",
                UserID = "root",
                Password = "root@2K24",
                Port = 3307,
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
            using (DbConnection connection = new MySqlConnection(builderMariaDB.ConnectionString))
            {
                connection.Open();

                var testTable1Repo = ActivatorUtilities.CreateInstance<TestTableRepo>(builder.Services.BuildServiceProvider(), connection, 1);

                Console.WriteLine("ITEMS MARIADB: ");

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
                using (DbConnection connection = new MySqlConnection(builderMariaDB.ConnectionString))
                {
                    connection.Open();

                    using (var testTable1Repo = ActivatorUtilities.CreateInstance<TestTableRepo>(builder.Services.BuildServiceProvider(), connection, 1))
                    {
                        if (dto != null && dto.Id == 0) // Save
                        {
                            Console.WriteLine("ITEM MARIADB - BEFORE SAVE : " + dto.ToString());

                            // TODO Logging info save

                            var testTable1Tmp = testTable1Repo.save(ConvertDtoToTestTable1(dto));

                            Console.WriteLine("ITEM MARIADB - AFTER SAVE : " + ConvertTestTable1ToDto(testTable1Tmp).ToString());

                            // TODO Logging info save
                        }
                        else if (dto != null && dto.Id != 0) // Update
                        {
                            var testTable1Tmp = testTable1Repo  .findOne(ConvertDtoToTestTable1(dto));

                            if (testTable1Tmp != null)
                            {
                                Console.WriteLine("ITEM MARIADB - BEFORE UPDATE : " + ConvertTestTable1ToDto(testTable1Tmp).ToString());

                                // TODO Logging info before update

                                testTable1Repo.save(ConvertDtoToTestTable1(dto));

                                Console.WriteLine("ITEM MARIADB - AFTER UPDATE : " + dto.ToString());

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
                using (DbConnection connection = new MySqlConnection(builderMariaDB.ConnectionString))
                {
                    connection.Open();

                    using (var testTable1Repo = ActivatorUtilities.CreateInstance<TestTableRepo>(builder.Services.BuildServiceProvider(), connection, 1))
                    {

                        Console.WriteLine("ITEM MARIADB - BEFORE DELETE : " + dto.ToString());

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

            return result; ;
        }
    }
}