using MySql.Data.MySqlClient;
using NET472.Repository;
using NET472.Service.Model.Dto;
using System;
using System.Data.Common;
using System.Data.SQLite;
using Unity;
using Unity.Resolution;
using NET472.Repository.Entities;

namespace NET472.Service
{
    public class SQLiteService : IService<TestTable1Dto>
    {
        private SQLiteConnectionStringBuilder builderSqlite;

        public SQLiteService()
        {
            builderSqlite = new SQLiteConnectionStringBuilder
            {
                DataSource = "D:\\DESARROLLO\\PROYECTOS\\JGACODETEST\\VS2K22\\VS2K22\\SRC\\0.CONSOLE\\NET472\\Repository\\SQLite\\TESTSQLITEDB.sqlite",
                Version = 3,
                JournalMode = SQLiteJournalModeEnum.Wal,
                Pooling = true
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
            using (DbConnection connection = new SQLiteConnection(builderSqlite.ConnectionString))
            {
                connection.Open();

                var testTable1Repo = container.Resolve<ITestTable1Repo>(
                    new ParameterOverride("existingConnection", connection),
                    new ParameterOverride("useSchema", false)
                );

                Console.WriteLine("ITEMS SQLITE: ");

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
                using (DbConnection connection = new SQLiteConnection(builderSqlite.ConnectionString))
                {
                    connection.Open();

                    using (var testTable1Repo = container.Resolve<ITestTable1Repo>(
                        new ParameterOverride("existingConnection", connection),
                        new ParameterOverride("useSchema", false)
                    ))
                    {
                        if (dto != null && dto.Id == 0) // Save
                        {
                            Console.WriteLine("ITEM SQLITE - BEFORE SAVE : " + dto.ToString());

                            // TODO Logging info save

                            var testTable1Tmp = testTable1Repo.save(ConvertDtoToTestTable1(dto));

                            Console.WriteLine("ITEM SQLITE - AFTER SAVE : " + ConvertTestTable1ToDto(testTable1Tmp).ToString());

                            // TODO Logging info save
                        }
                        else if (dto != null && dto.Id != 0) // Update
                        {
                            var testTable1Tmp = testTable1Repo.findOne(ConvertDtoToTestTable1(dto));

                            if (testTable1Tmp != null)
                            {
                                Console.WriteLine("ITEM SQLITE - BEFORE UPDATE : " + ConvertTestTable1ToDto(testTable1Tmp).ToString());

                                // TODO Logging info before update

                                testTable1Repo.save(ConvertDtoToTestTable1(dto));

                                Console.WriteLine("ITEM SQLITE - AFTER UPDATE : " + dto.ToString());

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