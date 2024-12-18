using PurchaseData.ApplicationCore.Interfaces;

namespace PurchaseData.ApplicationCore.BusinessServices;

public class PortugalRateService : IVatRateService, IDisposable
{
    private static readonly List<decimal> PortugalRates = new() { 6m, 13m, 23m };

    public bool IsValidVatRate(decimal vatRate) => PortugalRates.Contains(vatRate);

    public IEnumerable<decimal> GetVatRates() => PortugalRates;

    public bool SupportsCountry(string country)
    {
        return string.Equals(country, "Portugal", StringComparison.OrdinalIgnoreCase);
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(PortugalRateService)}.Dispose()");
    }
}
