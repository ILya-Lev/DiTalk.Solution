namespace Calculator.TightlyCoupled;

internal class Program
{
    static void Main(string[] args)
    {
        ConsoleKey exitKey = ConsoleKey.A;
        //one loop body is an operation or scope
        while (exitKey != ConsoleKey.D0 && exitKey != ConsoleKey.NumPad0)
        {
            var calculator = new PrimitiveCalculator();
            calculator.Run();

            Console.WriteLine("To exit press 0: ");
            exitKey = Console.ReadKey().Key;
        }
    }
}