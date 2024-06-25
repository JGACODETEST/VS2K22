// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using NET7NIVSUP.Repository;
using NET7NIVSUP.Service.Model.Dto;
using NET7NIVSUP.Service;
using Npgsql;
using System.Data.Common;

Console.WriteLine("Hello, World!");

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

/*
 En la inyección de dependencias, los términos transient, scoped y singleton se refieren a cómo se maneja la vida útil de los objetos que se crean. Aquí te dejo una descripción de cada uno:

Transient: Cada vez que se solicita una dependencia, se crea una nueva instancia del objeto. Esto es útil para objetos ligeros que no mantienen un estado123.
Scoped: Se crea una única instancia del objeto durante el ciclo de vida de una solicitud HTTP. Si la misma dependencia se solicita más de una vez durante una sola solicitud, se utiliza la misma instancia123.
Singleton: Se crea una única instancia del objeto cuando se solicita por primera vez, y esa misma instancia se reutiliza en todas las solicitudes futuras123.

Aquí te dejo algunos ejemplos de cuándo podrías querer usar cada uno:

Transient: Podrías usar este enfoque para servicios ligeros con poco o ningún estado1.
Scoped: Este es una mejor opción cuando quieres mantener un estado dentro de una solicitud1.
Singleton: Podrías usar este para servicios como el registro de eventos, la activación/desactivación de módulos durante la implementación y el servicio de correo electrónico1.

 */

builder.Services.AddTransient<ITestTable1Repo, TestTableRepo>();
//builder.Services.AddScoped<IExampleScopedService, ExampleScopedService>();
//builder.Services.AddSingleton<IExampleSingletonService, ExampleSingletonService>();
//builder.Services.AddTransient<ServiceLifetimeReporter>();

using IHost host = builder.Build();


using (var serviceScope = host.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    try
    {
        CrudSQLServer(builder);
        CrudMySQL(builder);
        CrudPostgres(builder);
        CrudMariaDB(builder);
        CrudSQLite(builder);
        CrudMongoDB(builder);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred.");
    }
}



Console.ReadLine();

static void CrudSQLServer(HostApplicationBuilder builder)
{
    var instanceSQLServerService = ActivatorUtilities.CreateInstance<SQLServerService>(builder.Services.BuildServiceProvider());

    instanceSQLServerService.Listar(builder);

    instanceSQLServerService.Grabar(builder, new TestTable1Dto()
    {
        Id = 0,
        Descripcion = "TEST ITEM SQL " + Guid.NewGuid().ToString()
    });

    //instanceSQLServerService.Grabar(container, new TestTable1Dto()
    //{
    //    Id = 8,
    //    Descripcion = "TEST ITEM SQL - MOD"
    //});

    instanceSQLServerService.Listar(builder);

    //instanceSQLServerService.Eliminar(container, new TestTable1Dto()
    //{
    //    Id = 9
    //});

    instanceSQLServerService.Listar(builder);
}

static void CrudMySQL(HostApplicationBuilder builder)
{
    var instanceMySQLService = ActivatorUtilities.CreateInstance<MySQLService>(builder.Services.BuildServiceProvider());

    instanceMySQLService.Listar(builder);

    instanceMySQLService.Grabar(builder, new TestTable1Dto()
    {
        Id = 4,
        Descripcion = "TEST ITEM MYSQL " + Guid.NewGuid().ToString()
    });

    //instanceMySQLService.Grabar(builder, new TestTable1Dto()
    //{
    //    Id = 8,
    //    Descripcion = "TEST ITEM MYSQL - MOD"
    //});

    instanceMySQLService.Listar(builder);

    //instanceMySQLService.Eliminar(builder, new TestTable1Dto()
    //{
    //    Id = 3
    //});

    instanceMySQLService.Listar(builder);
}

static void CrudPostgres(HostApplicationBuilder builder)
{
    var instancePostgresService = ActivatorUtilities.CreateInstance<PostgresService>(builder.Services.BuildServiceProvider());

    instancePostgresService.Listar(builder);

    instancePostgresService.Grabar(builder, new TestTable1Dto()
    {
        Id = 0,
        Descripcion = "TEST ITEM POSTGRES " + Guid.NewGuid().ToString()
    });

    //instancePostgresService.Grabar(builder, new TestTable1Dto()
    //{
    //    Id = 8,
    //    Descripcion = "TEST ITEM POSTGRES - MOD"
    //});

    instancePostgresService.Listar(builder);

    //instancePostgresService.Eliminar(container, new TestTable1Dto()
    //{
    //    Id = 3
    //});

    instancePostgresService.Listar(builder);
}

static void CrudMariaDB(HostApplicationBuilder builder)
{
    var instanceMariaDBService = ActivatorUtilities.CreateInstance<MariaDBService>(builder.Services.BuildServiceProvider());

    instanceMariaDBService.Listar(builder);

    instanceMariaDBService.Grabar(builder, new TestTable1Dto()
    {
        Id = 0,
        Descripcion = "TEST ITEM MARIADB " + Guid.NewGuid().ToString()
    });

    //instanceMariaDBService.Grabar(builder, new TestTable1Dto()
    //{
    //    Id = 8,
    //    Descripcion = "TEST ITEM MARIADB - MOD"
    //});

    instanceMariaDBService.Listar(builder);

    //instanceMariaDBService.Eliminar(builder, new TestTable1Dto()
    //{
    //    Id = 3
    //});

    instanceMariaDBService.Listar(builder);
}

static void CrudSQLite(HostApplicationBuilder builder)
{
    var instanceSQLiteService = ActivatorUtilities.CreateInstance<SQLiteService>(builder.Services.BuildServiceProvider());

    instanceSQLiteService.Listar(builder);

    instanceSQLiteService.Grabar(builder, new TestTable1Dto()
    {
        Id = 0,
        Descripcion = "TEST ITEM SQLITE " + Guid.NewGuid().ToString()
    });

    //instanceSQLiteService.Grabar(builder, new TestTable1Dto()
    //{
    //    Id = 8,
    //    Descripcion = "TEST ITEM instanceSQLiteService - MOD"
    //});

    instanceSQLiteService.Listar(builder);

    //instanceSQLiteService.Eliminar(builder, new TestTable1Dto()
    //{
    //    Id = 3
    //});

    instanceSQLiteService.Listar(builder);
}

static void CrudMongoDB(HostApplicationBuilder builder)
{
    var instanceMongoDBService = ActivatorUtilities.CreateInstance<MongoDBService>(builder.Services.BuildServiceProvider());

    instanceMongoDBService.Listar(builder);

    instanceMongoDBService.Grabar(builder, new TestTable1Dto()
    {
        Id = 4,
        Descripcion = "TEST ITEM MONGODB " + Guid.NewGuid().ToString()
    });

    //instanceMongoDBService.Grabar(builder, new TestTable1Dto()
    //{
    //    Id = 8,
    //    Descripcion = "TEST ITEM MONGODB - MOD"
    //});

    instanceMongoDBService.Listar(builder);

    //instanceMongoDBService.Eliminar(builder, new TestTable1Dto()
    //{
    //    Id = 3
    //});

    instanceMongoDBService.Listar(builder);
}

