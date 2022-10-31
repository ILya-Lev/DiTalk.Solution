namespace Calculator.TightlyCoupled;

internal class PrimitiveCalculator
{
    private const int DecimalDigits = 3;

    public void Run()
    {
        var operationRunner = new OperationRunner();
        var operationNames = operationRunner.GetAllOperationNames();

        var menu = new Menu();
        var operationIndex = menu.SelectOneOperationIndex(operationNames);
        var (lhs, rhs) = menu.GetOperands();

        var result = operationRunner.Calculate(operationIndex, lhs, rhs);
        var roundedResult = Math.Round(result, DecimalDigits);

        menu.DisplayResult(roundedResult);
    }
}