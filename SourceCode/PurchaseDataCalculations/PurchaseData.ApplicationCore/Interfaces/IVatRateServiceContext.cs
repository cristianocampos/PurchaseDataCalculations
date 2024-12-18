namespace PurchaseData.ApplicationCore.Interfaces;

public interface IVatRateServiceContext
{
    IVatRateService GetStrategy(string country);
}
