using PurchaseData.ApplicationCore.BusinessServices;
using PurchaseData.ApplicationCore.Interfaces;

namespace PurchaseData.ApplicationCore.Factory;

public class VatRateServiceFactory : IVatRateServiceFactory
{
    private const string Austria = "Austria";
    private readonly Dictionary<string, IVatRateService> VatRateStrategies;

    public VatRateServiceFactory()
    {
        VatRateStrategies = new Dictionary<string, IVatRateService>
        {
            { Austria, new AustriaVatRateService() }
        };
    }

    public IVatRateService GetStrategy(string country)
    {
        if (VatRateStrategies.TryGetValue(country, out var strategy))
        {
            return strategy;
        }

        throw new NotSupportedException($"Country '{country}' is not supported");
    }
}
