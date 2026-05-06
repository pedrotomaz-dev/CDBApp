using CDB.Domain.Interfaces;

namespace CDB.Domain.Services;

public class TaxCalculator : ITaxCalculator
{
    public decimal GetTaxRate(int months)
    {
        if (months <= 6)
            return 0.225m;
        if (months <= 12)
            return 0.20m;
        if (months <= 24)
            return 0.175m;
        
        return 0.15m;
    }
}
