using CDB.Domain.Entities;

namespace CDB.Domain.Interfaces;

public interface ICdbCalculator
{
    InvestmentResult Calculate(decimal initialValue, int months);
}
