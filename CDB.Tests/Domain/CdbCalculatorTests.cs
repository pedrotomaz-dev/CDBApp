using CDB.Domain.Interfaces;
using CDB.Domain.Services;
using FluentAssertions;
using NSubstitute;
using System;
using Xunit;

namespace CDB.Tests.Domain;

public class CdbCalculatorTests
{
    private readonly ITaxCalculator _taxCalculatorMock;
    private readonly CdbCalculator _sut;

    public CdbCalculatorTests()
    {
        _taxCalculatorMock = Substitute.For<ITaxCalculator>();
        _sut = new CdbCalculator(_taxCalculatorMock);
    }

    [Fact]
    public void Calculate_ShouldThrowArgumentException_WhenInitialValueIsZeroOrLess()
    {
        // Act
        Action act = () => _sut.Calculate(0, 10);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*O valor inicial deve ser maior que zero.*");
    }

    [Fact]
    public void Calculate_ShouldThrowArgumentException_WhenMonthsIsOneOrLess()
    {
        // Act
        Action act = () => _sut.Calculate(1000, 1);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*O prazo de resgate deve ser maior que 1 mês.*");
    }

    [Fact]
    public void Calculate_ShouldReturnCorrectGrossAndNetValue()
    {
        // Arrange
        decimal initialValue = 1000m;
        int months = 2; // rate per month is 0.00972
        
        // Month 1: 1000 * 1.00972 = 1009.72
        // Month 2: 1009.72 * 1.00972 = 1019.5344...
        decimal expectedGross = 1019.53m; // Arronded
        
        _taxCalculatorMock.GetTaxRate(months).Returns(0.225m); // 22.5% for 2 months
        
        // Profit: 1019.5344 - 1000 = 19.5344
        // Net: 1000 + (19.5344 * (1 - 0.225)) = 1000 + (19.5344 * 0.775) = 1000 + 15.139... = 1015.14
        decimal expectedNet = 1015.14m;

        // Act
        var result = _sut.Calculate(initialValue, months);

        // Assert
        result.GrossValue.Should().Be(expectedGross);
        result.NetValue.Should().Be(expectedNet);
    }
}
