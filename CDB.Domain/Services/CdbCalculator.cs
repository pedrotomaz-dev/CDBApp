using CDB.Domain.Entities;
using CDB.Domain.Interfaces;
using System;

namespace CDB.Domain.Services;

public class CdbCalculator : ICdbCalculator
{
    private const decimal ValidCDI = 0.009m;
    private const decimal ValidTB = 1.08m;
    
    private readonly ITaxCalculator _taxCalculator;

    public CdbCalculator(ITaxCalculator taxCalculator)
    {
        _taxCalculator = taxCalculator;
    }

    public InvestmentResult Calculate(decimal initialValue, int months)
    {
        if (initialValue <= 0)
            throw new ArgumentException("O valor inicial deve ser maior que zero.", nameof(initialValue));
        
        if (months <= 1)
            throw new ArgumentException("O prazo de resgate deve ser maior que 1 mês.", nameof(months));

        decimal grossValue = initialValue;
        
        // VF = VI x [1 + (CDI x TB)]
        decimal rate = 1 + (ValidCDI * ValidTB);

        for (int i = 0; i < months; i++)
        {
            grossValue *= rate;
        }

        decimal rawProfit = grossValue - initialValue;
        decimal taxRate = _taxCalculator.GetTaxRate(months);
        decimal netValue = initialValue + (rawProfit * (1 - taxRate));

        // Arredondando para 2 casas decimais de forma monetária
        return new InvestmentResult(
            Math.Round(grossValue, 2, MidpointRounding.ToEven),
            Math.Round(netValue, 2, MidpointRounding.ToEven)
        );
    }
}
