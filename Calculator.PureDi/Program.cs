using Calculator.Common;
using Microsoft.Extensions.Configuration;

namespace Calculator.PureDi;

internal class Program
{
    static void Main(string[] args)
    {
        ConsoleKey exitKey = ConsoleKey.A;
        //one loop body is an operation or scope
        while (exitKey != ConsoleKey.D0 && exitKey != ConsoleKey.NumPad0)
        {
            var calculator = CreatePrimitiveCalculator();
            calculator.Run();

            Console.WriteLine("To exit press 0: ");
            exitKey = Console.ReadKey().Key;
        }
    }

    /// <summary> Pure DI composition root </summary>
    private static PrimitiveCalculator CreatePrimitiveCalculator()
    {
        var displayOptions = ParseDisplayOptions();
        
        IMenu menu = new Menu();
        ICalculatingOperation[] operations = new ICalculatingOperation[]
        {
            new LoggingAdd(new Add()),
            new Subtract(),
            new Multiply(),
            new Divide(),
        };
        IOperationRunner operationRunner = new OperationRunner(operations);

        PrimitiveCalculator calculator = new (menu, operationRunner, displayOptions);
        return calculator;
    }

    private static DisplayOptions ParseDisplayOptions()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var digitsAmount = configuration.GetSection("DisplayOptions:DecimalDigits").Value;
        
        return new()
        {
            DecimalDigits = int.TryParse(digitsAmount, out var da) ? da : 4
        };
    }
}