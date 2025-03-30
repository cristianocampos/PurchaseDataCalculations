using PurchaseData.ApplicationCore.Interfaces;

namespace PurchaseData.ApplicationCore.BusinessServices;

public class AustriaVatRateService : IVatRateService
{
    private const string Austria = "Austria";
    private readonly List<decimal> _austriaRates = new() { 10m, 13m, 20m };

    public bool IsValidVatRate(decimal vatRate) => _austriaRates.Contains(vatRate);

    public IEnumerable<decimal> GetVatRates() => _austriaRates;

    public bool SupportsCountry(string country)
    {
        return string.Equals(country, Austria, StringComparison.OrdinalIgnoreCase);
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(AustriaVatRateService)}.Dispose()");
    }
}
