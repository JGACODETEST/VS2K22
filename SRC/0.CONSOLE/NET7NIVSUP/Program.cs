// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using NET7NIVSUP.Repository;
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
        var builderSQLServer = new SqlConnectionStringBuilder
        {
            DataSource = "localhost",
            InitialCatalog = "TESTDBSQL2K19",
            UserID = "SA",
            Password = "sql2K19@",
            IntegratedSecurity = false,
            TrustServerCertificate = true, // SOLO SI ES DESARROLLO ESTO NO DEBERÍA IR EN PROD
            PersistSecurityInfo = true
        };

        var builderMySQL = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            Database = "TESTDBMYSQL57",
            UserID = "root",
            Password = "root@2K24",
            Port = 3306,
            PersistSecurityInfo = true
        };

        var builderPostgres = new NpgsqlConnectionStringBuilder
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

        //ListarSqlServer(builder, builderSQLServer);

        //ListarMySql(builder, builderMySQL);

        ListarPostgres(builder, builderPostgres);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred.");
    }
}



Console.ReadLine();

static void ListarMySql(HostApplicationBuilder builder, MySqlConnectionStringBuilder builderMySQL)
{
    using (DbConnection connection = new MySqlConnection(builderMySQL.ConnectionString))
    {
        connection.Open();


        var testTable1Repo = ActivatorUtilities.CreateInstance<TestTableRepo>(builder.Services.BuildServiceProvider(), connection, 1);

        Console.WriteLine("ITEMS MYSQL: ");

        var testTable1List = testTable1Repo.getAll();

        foreach (var item in testTable1List)
        {
            Console.WriteLine("- " + item.Id.ToString() + " - " + item.Descripcion);
        }
    }
}

static void ListarSqlServer(HostApplicationBuilder builder, SqlConnectionStringBuilder builderSQLServer)
{
    using (DbConnection connection = new SqlConnection(builderSQLServer.ConnectionString))
    {
        connection.Open();

        var testTable1Repo = ActivatorUtilities.CreateInstance<TestTableRepo>(builder.Services.BuildServiceProvider(), connection, 0);

        Console.WriteLine("ITEMS SQL: ");

        var testTable1List = testTable1Repo.getAll();

        foreach (var item in testTable1List)
        {
            Console.WriteLine("- " + item.Id.ToString() + " - " + item.Descripcion);
        }
    }
}

static void ListarPostgres(HostApplicationBuilder builder, NpgsqlConnectionStringBuilder builderPostgres)
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
