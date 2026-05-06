using CDB.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CDB.Application.Queries;

public class CalculateCdbQueryHandler : IRequestHandler<CalculateCdbQuery, CalculateCdbResult>
{
    private readonly ICdbCalculator _cdbCalculator;

    public CalculateCdbQueryHandler(ICdbCalculator cdbCalculator)
    {
        _cdbCalculator = cdbCalculator;
    }

    public Task<CalculateCdbResult> Handle(CalculateCdbQuery request, CancellationToken cancellationToken)
    {
        var investmentResult = _cdbCalculator.Calculate(request.InitialValue, request.Months);

        var result = new CalculateCdbResult(investmentResult.GrossValue, investmentResult.NetValue);

        return Task.FromResult(result);
    }
}
