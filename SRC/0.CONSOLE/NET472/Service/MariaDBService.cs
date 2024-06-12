using MySql.Data.MySqlClient;
using NET472.Repository;
using NET472.Service.Model.Dto;
using System;
using System.Data.Common;
using Unity;
using Unity.Resolution;
using NET472.Repository.Entities;

namespace NET472.Service
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

        public void Listar(IUnityContainer container)
        {
            using (DbConnection connection = new MySqlConnection(builderMariaDB.ConnectionString))
            {
                connection.Open();

                var testTable1Repo = container.Resolve<ITestTable1Repo>(
                    new ParameterOverride("existingConnection", connection),
                    new ParameterOverride("useSchema", false)
                );

                Console.WriteLine("ITEMS MARIADB: ");

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
                using (DbConnection connection = new MySqlConnection(builderMariaDB.ConnectionString))
                {
                    connection.Open();

                    using (var testTable1Repo = container.Resolve<ITestTable1Repo>(
                        new ParameterOverride("existingConnection", connection),
                        new ParameterOverride("useSchema", false)
                    ))
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
                            var testTable1Tmp = testTable1Repo.findOne(ConvertDtoToTestTable1(dto));

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
    }
}