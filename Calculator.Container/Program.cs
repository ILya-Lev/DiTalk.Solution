using System;
using Calculator.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Calculator.Container;

internal class Program
{
    static void Main(string[] args)
    {
        using ServiceProvider serviceProvider = BuildServiceProvider();

        var exitKey = ConsoleKey.A;
        //one loop body is an operation or scope
        while (exitKey != ConsoleKey.D0 && exitKey != ConsoleKey.NumPad0)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            var calculator = scope.ServiceProvider.GetRequiredService<PrimitiveCalculator>();
            calculator.Run();

            Console.WriteLine("To exit press 0: ");
            exitKey = Console.ReadKey().Key;
        }
    }

    /// <summary> DI-container based composition root </summary>
    private static ServiceProvider BuildServiceProvider()
    {
        #region step 1: get configuration
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        #endregion

        #region step 2: register all components
        var serviceCollection = new ServiceCollection();

        serviceCollection.Configure<DisplayOptions>(configuration.GetSection(nameof(DisplayOptions)));

        serviceCollection.TryAddScoped<IMenu, Menu>();
        serviceCollection.TryAddScoped<IOperationRunner, OperationRunner>();
        serviceCollection.TryAddScoped<PrimitiveCalculator>();

        //using Scrutor https://github.com/khellang/Scrutor  convention over configuration
        serviceCollection.Scan(scan => scan
            .FromAssemblyOf<ICalculatingOperation>()
            .AddClasses(classes => classes.AssignableTo(typeof(ICalculatingOperation)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
        );
        #endregion

        #region step 3: build service provider
        //whether scopes are validated depends on validateScopes option
        return serviceCollection.BuildServiceProvider(validateScopes: true);
        #endregion
    }
}