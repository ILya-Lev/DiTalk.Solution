using Calculator.Common;

namespace Calculator.TightlyCoupled;

public interface IOperationRunner
{
    IReadOnlyList<string> GetAllOperationNames();
    double Calculate(int operationIndex, double lhs, double rhs);
}

public class OperationRunner : IOperationRunner
{
    private readonly IReadOnlyList<ICalculatingOperation> _operations = new ICalculatingOperation[]
    {
        new Add(),
        new Subtract(),
        new Multiply(),
        new Divide()
    };

    public IReadOnlyList<string> GetAllOperationNames() => _operations
        .Select(op => op.GetType().Name)
        .ToArray();

    public double Calculate(int operationIndex, double lhs, double rhs) => 
        GetOperation(operationIndex)
            .Calculate(lhs, rhs);

    private ICalculatingOperation GetOperation(int index) => _operations[index];
}