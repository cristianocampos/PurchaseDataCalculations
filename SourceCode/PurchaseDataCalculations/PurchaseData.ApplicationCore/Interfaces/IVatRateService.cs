namespace PurchaseData.ApplicationCore.Interfaces;

public interface IVatRateService
{
    bool IsValidVatRate(decimal vatRate);
    IEnumerable<decimal> GetVatRates();
}
