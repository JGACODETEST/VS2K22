using MySql.Data.MySqlClient;
using NET472.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Resolution;

namespace NET472
{
    internal class Program
    {
        static void Main(string[] args)
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




            using (DbConnection connection = new SqlConnection(builderSQLServer.ConnectionString))
            {
                connection.Open();

                var testTable1Repo = container.Resolve<ITestTable1Repo>(new ParameterOverride("existingConnection", connection));

                Console.WriteLine("ITEMS SQL: ");

                var testTable1List = testTable1Repo.getAll();

                foreach (var item in testTable1List)
                {
                    Console.WriteLine("- " + item.Id.ToString()  + " - " + item.Descripcion);
                }

                
            }

            using (DbConnection connection = new MySqlConnection(builderMySQL.ConnectionString))
            {
                connection.Open();

                var testTable1Repo = container.Resolve<ITestTable1Repo>(new ParameterOverride("existingConnection", connection));

                Console.WriteLine("ITEMS MYSQL: ");

                var testTable1List = testTable1Repo.getAll();

                foreach (var item in testTable1List)
                {
                    Console.WriteLine("- " + item.Id.ToString() + " - " + item.Descripcion);
                }


            }

            Console.ReadLine();
        }
    }
}
