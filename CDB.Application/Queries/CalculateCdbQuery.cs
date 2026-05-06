using MediatR;

namespace CDB.Application.Queries;

public class CalculateCdbQuery : IRequest<CalculateCdbResult>
{
    public decimal InitialValue { get; set; }
    public int Months { get; set; }

    public CalculateCdbQuery(decimal initialValue, int months)
    {
        InitialValue = initialValue;
        Months = months;
    }
}

public class CalculateCdbResult
{
    public decimal GrossValue { get; set; }
    public decimal NetValue { get; set; }

    public CalculateCdbResult(decimal grossValue, decimal netValue)
    {
        GrossValue = grossValue;
        NetValue = netValue;
    }
}
