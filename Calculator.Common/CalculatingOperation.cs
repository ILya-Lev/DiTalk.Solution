namespace Calculator.Common;

public interface ICalculatingOperation
{
    double Calculate(double lhs, double rhs);
}

public class Add : ICalculatingOperation
{
    public double Calculate(double lhs, double rhs) => lhs + rhs;
}

public class Subtract : ICalculatingOperation
{
    public double Calculate(double lhs, double rhs) => lhs - rhs;
}

public class Multiply : ICalculatingOperation
{
    public double Calculate(double lhs, double rhs) => lhs * rhs;
}

public class Divide : ICalculatingOperation
{
    public double Calculate(double lhs, double rhs) => rhs == 0.0 ? double.NaN : lhs / rhs;
}

//public class Power : ICalculatingOperation
//{
//    public double Calculate(double aBase, double power) => Math.Pow(aBase, power);
//}

//public class Root : ICalculatingOperation
//{
//    public double Calculate(double number, double power) => power == 0.0
//        ? double.NaN
//        : Math.Pow(number, 1 / power);
//}