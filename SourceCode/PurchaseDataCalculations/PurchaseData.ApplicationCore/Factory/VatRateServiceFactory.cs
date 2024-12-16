using PurchaseData.ApplicationCore.Interfaces;

namespace PurchaseData.ApplicationCore.Factory;

public class VatRateServiceFactory : IVatRateServiceFactory, IDisposable
{
    private readonly IEnumerable<IVatRateService> _strategies;

    public VatRateServiceFactory(IEnumerable<IVatRateService> strategies)
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
        Console.WriteLine($"{nameof(VatRateServiceFactory)}.Dispose()");
    }
}
