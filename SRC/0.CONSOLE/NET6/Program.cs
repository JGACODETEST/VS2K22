using Microsoft.Extensions.Hosting;
using NET6.Repository;

namespace NET6
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
            //builder.Services.AddTransient<ITestTable1Repo, TestTableRepo>();
            //builder.Services.AddScoped<IExampleScopedService, ExampleScopedService>();
            //builder.Services.AddSingleton<IExampleSingletonService, ExampleSingletonService>();
            //builder.Services.AddTransient<ServiceLifetimeReporter>();

            using IHost host = builder.Build();


            Console.ReadLine();
        }
    }
}
