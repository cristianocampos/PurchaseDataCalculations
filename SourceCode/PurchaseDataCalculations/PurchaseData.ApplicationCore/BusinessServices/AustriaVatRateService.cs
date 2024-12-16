using PurchaseData.ApplicationCore.Interfaces;

namespace PurchaseData.ApplicationCore.BusinessServices;

public class AustriaVatRateService : IVatRateService, IDisposable
{
    private const string Austria = "Austria";
    private static readonly List<decimal> AustriaRates = new() { 10m, 13m, 20m };

    public bool IsValidVatRate(decimal vatRate) => AustriaRates.Contains(vatRate);

    public IEnumerable<decimal> GetVatRates() => AustriaRates;

    public bool SupportsCountry(string country)
    {
        return string.Equals(country, Austria, StringComparison.OrdinalIgnoreCase);
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(AustriaVatRateService)}.Dispose()");
    }
}
