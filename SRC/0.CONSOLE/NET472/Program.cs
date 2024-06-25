using NET472.Repository;
using NET472.Repository.Entities;
using NET472.Service;
using NET472.Service.Model.Dto;
using System;
using Unity;

namespace NET472
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<ITestTable1Repo, TestTable1Repo>();

            container.RegisterType<IService<TestTable1Dto>, SQLServerService>("SQLServer");
            container.RegisterType<IService<TestTable1Dto>, MySQLService>("MySQL");
            container.RegisterType<IService<TestTable1Dto>, PostgresService>("Postgres");
            container.RegisterType<IService<TestTable1Dto>, MariaDBService>("MariaDB");
            container.RegisterType<IService<TestTable1Dto>, SQLiteService>("SQLite");
            container.RegisterType<IService<TestTable1Dto>, MongoDBService>("MongoDB");


            CrudSQLServer(container);
            CrudMySQL(container);
            CrudPostgres(container);
            CrudMariaDB(container);
            CrudSQLite(container);
            CrudMongoDB(container);


            // Force a garbage collection to occur for all generations.
            GC.Collect();

            // Wait for all finalizers to complete before continuing.
            // Without this call to WaitForPendingFinalizers, there might still be finalizers running on objects that were just collected.
            GC.WaitForPendingFinalizers();

            Console.ReadLine();
        }

        private static void CrudSQLServer(IUnityContainer container)
        {
            var instanceSQLServerService = container.Resolve<IService<TestTable1Dto>>("SQLServer");

            instanceSQLServerService.Listar(container);
            
            instanceSQLServerService.Grabar(container, new TestTable1Dto()
            {
                Id = 0,
                Descripcion = "TEST ITEM SQL " + Guid.NewGuid().ToString()
            });

            //instanceSQLServerService.Grabar(container, new TestTable1Dto()
            //{
            //    Id = 8,
            //    Descripcion = "TEST ITEM SQL - MOD"
            //});

            instanceSQLServerService.Listar(container);

            //instanceSQLServerService.Eliminar(container, new TestTable1Dto()
            //{
            //    Id = 9
            //});

            instanceSQLServerService.Listar(container);
        }

        private static void CrudMySQL(IUnityContainer container)
        {
            var instanceMySQLService = container.Resolve<IService<TestTable1Dto>>("MySQL");

            instanceMySQLService.Listar(container);

            instanceMySQLService.Grabar(container, new TestTable1Dto()
            {
                Id = 4,
                Descripcion = "TEST ITEM MYSQL " + Guid.NewGuid().ToString()
            });

            //instanceMySQLService.Grabar(container, new TestTable1Dto()
            //{
            //    Id = 8,
            //    Descripcion = "TEST ITEM MYSQL - MOD"
            //});

            instanceMySQLService.Listar(container);

            //instanceMySQLService.Eliminar(container, new TestTable1Dto()
            //{
            //    Id = 3
            //});

            instanceMySQLService.Listar(container);
        }

        private static void CrudPostgres(IUnityContainer container)
        {
            var instancePostgresService = container.Resolve<IService<TestTable1Dto>>("Postgres");

            instancePostgresService.Listar(container);

            instancePostgresService.Grabar(container, new TestTable1Dto()
            {
                Id = 0,
                Descripcion = "TEST ITEM POSTGRES " + Guid.NewGuid().ToString()
            });

            //instancePostgresService.Grabar(container, new TestTable1Dto()
            //{
            //    Id = 8,
            //    Descripcion = "TEST ITEM POSTGRES - MOD"
            //});

            instancePostgresService.Listar(container);

            //instancePostgresService.Eliminar(container, new TestTable1Dto()
            //{
            //    Id = 3
            //});

            instancePostgresService.Listar(container);
        }

        private static void CrudMariaDB(IUnityContainer container)
        {
            var instanceMariaDBService = container.Resolve<IService<TestTable1Dto>>("MariaDB");

            instanceMariaDBService.Listar(container);

            instanceMariaDBService.Grabar(container, new TestTable1Dto()
            {
                Id = 0,
                Descripcion = "TEST ITEM MARIADB " + Guid.NewGuid().ToString()
            });

            //instanceMariaDBService.Grabar(container, new TestTable1Dto()
            //{
            //    Id = 8,
            //    Descripcion = "TEST ITEM MARIADB - MOD"
            //});

            instanceMariaDBService.Listar(container);

            //instanceMariaDBService.Eliminar(container, new TestTable1Dto()
            //{
            //    Id = 3
            //});

            instanceMariaDBService.Listar(container);
        }

        private static void CrudSQLite(IUnityContainer container)
        {
            var instanceSQLiteService = container.Resolve<IService<TestTable1Dto>>("SQLite");

            instanceSQLiteService.Listar(container);

            instanceSQLiteService.Grabar(container, new TestTable1Dto()
            {
                Id = 0,
                Descripcion = "TEST ITEM SQLITE " + Guid.NewGuid().ToString()
            });

            //instanceSQLiteService.Grabar(container, new TestTable1Dto()
            //{
            //    Id = 8,
            //    Descripcion = "TEST ITEM instanceSQLiteService - MOD"
            //});

            instanceSQLiteService.Listar(container);

            //instanceSQLiteService.Eliminar(container, new TestTable1Dto()
            //{
            //    Id = 3
            //});

            instanceSQLiteService.Listar(container);
        }

        private static void CrudMongoDB(IUnityContainer container)
        {
            var instanceMongoDBService = container.Resolve<IService<TestTable1Dto>>("MongoDB");

            instanceMongoDBService.Listar(container);

            instanceMongoDBService.Grabar(container, new TestTable1Dto()
            {
                Id = 4,
                Descripcion = "TEST ITEM MONGODB " + Guid.NewGuid().ToString()
            });

            //instanceMongoDBService.Grabar(container, new TestTable1Dto()
            //{
            //    Id = 8,
            //    Descripcion = "TEST ITEM MONGODB - MOD"
            //});

            instanceMongoDBService.Listar(container);

            //instanceMongoDBService.Eliminar(container, new TestTable1Dto()
            //{
            //    Id = 3
            //});

            instanceMongoDBService.Listar(container);
        }
    }
}