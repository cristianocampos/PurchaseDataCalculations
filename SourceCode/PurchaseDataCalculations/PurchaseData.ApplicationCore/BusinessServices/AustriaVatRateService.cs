using PurchaseData.ApplicationCore.Interfaces;

namespace PurchaseData.ApplicationCore.BusinessServices;

public class AustriaVatRateService : IVatRateService
{
    private static readonly List<decimal> AustriaRates = new() { 10M, 13M, 20M };

    public bool IsValidVatRate(decimal vatRate) => AustriaRates.Contains(vatRate);

    public IEnumerable<decimal> GetVatRates() => AustriaRates;
}
