﻿using MySql.Data.MySqlClient;
using NET8.Repository;
using NET8.Service.Model.Dto;
using System;
using System.Data.Common;
using NET8.Repository.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Microsoft.Data.Sqlite;

namespace NET8.Service
{
    public class SQLiteService : IService<TestTable1Dto>
    {
        private SqliteConnectionStringBuilder builderSqlite;

        public SQLiteService()
        {
            builderSqlite = new SqliteConnectionStringBuilder
            {
                DataSource = "D:\\DESARROLLO\\PROYECTOS\\JGACODETEST\\VS2K22\\VS2K22\\SRC\\0.CONSOLE\\NET8\\Repository\\SQLite\\TESTSQLITEDB.sqlite",
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

        public void Listar(HostApplicationBuilder builder)
        {
            using (DbConnection connection = new SqliteConnection(builderSqlite.ConnectionString))
            {
                connection.Open();

                var testTable1Repo = ActivatorUtilities.CreateInstance<TestTableRepo>(builder.Services.BuildServiceProvider(), connection, 3);

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
                using (DbConnection connection = new SqliteConnection(builderSqlite.ConnectionString))
                {
                    connection.Open();

                    using (var testTable1Repo = ActivatorUtilities.CreateInstance<TestTableRepo>(builder.Services.BuildServiceProvider(), connection, 3))
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
                using (DbConnection connection = new SqliteConnection(builderSqlite.ConnectionString))
                {
                    connection.Open();

                    using (var testTable1Repo = ActivatorUtilities.CreateInstance<TestTableRepo>(builder.Services.BuildServiceProvider(), connection, 3))
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