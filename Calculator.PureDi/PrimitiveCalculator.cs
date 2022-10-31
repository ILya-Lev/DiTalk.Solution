namespace Calculator.PureDi;

internal class PrimitiveCalculator
{
    private readonly IMenu _menu;
    private readonly IOperationRunner _operationRunner;
    private readonly DisplayOptions _config;

    public PrimitiveCalculator(
        IMenu menu
        , IOperationRunner operationRunner
        , DisplayOptions config)
    {
        _menu = menu;
        _operationRunner = operationRunner;
        _config = config;
    }

    public void Run()
    {
        var operationNames = _operationRunner.GetAllOperationNames();

        var operationIndex = _menu.SelectOneOperationIndex(operationNames);
        var (lhs, rhs) = _menu.GetOperands();

        var result = _operationRunner.Calculate(operationIndex, lhs, rhs);
        var roundedResult = Math.Round(result, _config.DecimalDigits);

        _menu.DisplayResult(roundedResult);
    }
}