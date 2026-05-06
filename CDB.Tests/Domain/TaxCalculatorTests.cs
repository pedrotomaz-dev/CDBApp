using CDB.Domain.Services;
using FluentAssertions;
using Xunit;

namespace CDB.Tests.Domain;

public class TaxCalculatorTests
{
    private readonly TaxCalculator _sut;

    public TaxCalculatorTests()
    {
        _sut = new TaxCalculator();
    }

    [Theory]
    [InlineData(2, 0.225)]
    [InlineData(6, 0.225)]
    [InlineData(7, 0.20)]
    [InlineData(12, 0.20)]
    [InlineData(13, 0.175)]
    [InlineData(24, 0.175)]
    [InlineData(25, 0.15)]
    [InlineData(36, 0.15)]
    public void GetTaxRate_ShouldReturnExpectedRate_BasedOnMonths(int months, decimal expectedTaxRate)
    {
        // Act
        var result = _sut.GetTaxRate(months);

        // Assert
        result.Should().Be(expectedTaxRate);
    }
}
