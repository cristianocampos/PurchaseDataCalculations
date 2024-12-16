namespace PurchaseData.ApplicationCore.Interfaces;

public interface IVatRateServiceFactory
{
    IVatRateService GetStrategy(string country);
}
