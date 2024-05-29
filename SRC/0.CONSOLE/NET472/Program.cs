using MongoDB.Bson;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using NET472.Repository;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Resolution;

namespace NET472
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<ITestTable1Repo, TestTableRepo>();

            var builderSQLServer = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = "TESTDBSQL2K19",
                UserID = "SA",
                Password = "sql2K19@"
            };

            var builderMySQL = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "TESTDBMYSQL57",
                UserID = "root",
                Password = "root@2K24",
                Port = 3306
            };

            var builderPostgres = new NpgsqlConnectionStringBuilder
            {
                Host = "localhost",
                Database = "TESTDBPOSTGRES13",
                Username = "postgres",
                Password = "postgre@2K24",
                Port = 5432,
                SslMode = SslMode.Prefer,
                SearchPath = "public"
            };

            var builderMariaDB = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "TESTDBMARIADB1011",
                UserID = "root",
                Password = "root@2K24",
                Port = 3307
            };

            var builderSqlite = new SQLiteConnectionStringBuilder
            {
                DataSource = "D:\\DESARROLLO\\PROYECTOS\\JGACODETEST\\VS2K22\\VS2K22\\SRC\\0.CONSOLE\\NET472\\Repository\\SQLite\\TESTSQLITEDB.sqlite",
                Version = 3,
                JournalMode = SQLiteJournalModeEnum.Wal,
                Pooling = true
            };

            //ListSQLServer(container, builderSQLServer);

            //ListMySQL(container, builderMySQL);

            //ListPostgres(container, builderPostgres);

            //ListMariaDB(container, builderMariaDB);

            //ListSQLite(container, builderSqlite);


            ListMongoDB();

            Console.ReadLine();
        }

        
        
        private static void ListSQLServer(IUnityContainer container, SqlConnectionStringBuilder builderSQLServer)
        {
            using (DbConnection connection = new SqlConnection(builderSQLServer.ConnectionString))
            {
                connection.Open();

                var testTable1Repo = container.Resolve<ITestTable1Repo>(
                    new ParameterOverride("existingConnection", connection),
                    new ParameterOverride("useSchema", false)
                );

                Console.WriteLine("ITEMS SQL: ");

                var testTable1List = testTable1Repo.getAll();

                foreach (var item in testTable1List)
                {
                    Console.WriteLine("- " + item.Id.ToString() + " - " + item.Descripcion);
                }
            }
        }

        private static void ListMySQL(IUnityContainer container, MySqlConnectionStringBuilder builderMySQL)
        {
            using (DbConnection connection = new MySqlConnection(builderMySQL.ConnectionString))
            {
                connection.Open();

                var testTable1Repo = container.Resolve<ITestTable1Repo>(
                    new ParameterOverride("existingConnection", connection),
                    new ParameterOverride("useSchema", false)
                );

                Console.WriteLine("ITEMS MYSQL: ");

                var testTable1List = testTable1Repo.getAll();

                foreach (var item in testTable1List)
                {
                    Console.WriteLine("- " + item.Id.ToString() + " - " + item.Descripcion);
                }
            }
        }

        private static void ListPostgres(IUnityContainer container, NpgsqlConnectionStringBuilder builderPostgres)
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

        private static void ListMariaDB(IUnityContainer container, MySqlConnectionStringBuilder builderMariaDB)
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

        private static void ListSQLite(IUnityContainer container, SQLiteConnectionStringBuilder builderSqlite)
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

        private static void ListMongoDB()
        {
            // Replace with your connection string
            const string connectionString = "mongodb://localhost:27017";

            // Create a MongoClient object
            var client = new MongoClient(connectionString);

            // Use the MongoClient to access the server
            var database = client.GetDatabase("TESTMONGODB44");

            // For example, to get a collection from the database
            var collection = database.GetCollection<BsonDocument>("TESTCOLLECTION1");

            var filter = Builders<BsonDocument>.Filter.Empty;
            var documents = collection.Find(filter).ToList();

            foreach (var document in documents)
            {
                Console.WriteLine(document.ToString());
            }
        }


    }
}