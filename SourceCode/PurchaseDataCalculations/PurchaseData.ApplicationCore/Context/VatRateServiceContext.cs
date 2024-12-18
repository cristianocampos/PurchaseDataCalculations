using PurchaseData.ApplicationCore.Interfaces;

namespace PurchaseData.ApplicationCore.Context;

public class VatRateServiceContext : IVatRateServiceContext, IDisposable
{
    private readonly IEnumerable<IVatRateService> _strategies;

    public VatRateServiceContext(IEnumerable<IVatRateService> strategies)
    {
        _strategies = strategies;
    }

    public IVatRateService GetStrategy(string country)
    {
        var strategy = _strategies.FirstOrDefault(s => s.SupportsCountry(country));
        if (strategy == null)
        {
            throw new NotSupportedException($"Country '{country}' not supported");
        }
        return strategy;
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(VatRateServiceContext)}.Dispose()");
    }
}
