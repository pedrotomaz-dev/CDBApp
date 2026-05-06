using CDB.Application.Queries;
using CDB.Domain.Entities;
using CDB.Domain.Interfaces;
using FluentAssertions;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CDB.Tests.Application;

public class CalculateCdbQueryHandlerTests
{
    private readonly ICdbCalculator _cdbCalculatorMock;
    private readonly CalculateCdbQueryHandler _sut;

    public CalculateCdbQueryHandlerTests()
    {
        _cdbCalculatorMock = Substitute.For<ICdbCalculator>();
        _sut = new CalculateCdbQueryHandler(_cdbCalculatorMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnCalculateCdbResult_WhenProvidingValidQuery()
    {
        // Arrange
        var query = new CalculateCdbQuery(1000m, 2);
        var expectedGrossValue = 1019.53m;
        var expectedNetValue = 1015.14m;

        _cdbCalculatorMock.Calculate(1000m, 2).Returns(new InvestmentResult(expectedGrossValue, expectedNetValue));

        // Act
        var result = await _sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.GrossValue.Should().Be(expectedGrossValue);
        result.NetValue.Should().Be(expectedNetValue);
        
        _cdbCalculatorMock.Received(1).Calculate(1000m, 2);
    }
}
