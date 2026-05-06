namespace CDB.Domain.Entities;

public class InvestmentResult
{
    public decimal GrossValue { get; private set; }
    public decimal NetValue { get; private set; }

    public InvestmentResult(decimal grossValue, decimal netValue)
    {
        GrossValue = grossValue;
        NetValue = netValue;
    }
}
