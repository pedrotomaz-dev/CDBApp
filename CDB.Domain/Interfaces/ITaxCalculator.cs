namespace CDB.Domain.Interfaces;

public interface ITaxCalculator
{
    decimal GetTaxRate(int months);
}
