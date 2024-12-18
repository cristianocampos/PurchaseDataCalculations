using PurchaseData.ApplicationCore.Interfaces;
using PurchaseData.ApplicationCore.Models;

namespace PurchaseData.ApplicationCore.BusinessServices;

public class VatCalculationsService : IVatCalculationsService, IDisposable
{

    public VatCalculationsResponseModel Calculate(VatCalculationsRequestModel request)
    {
        decimal net = 0, 
                gross = 0,
                vat = 0;

        try
        {
            if (request.NetAmount.HasValue && request.VatRate.HasValue)
            {
                net = request.NetAmount.Value;
                vat = net * (request.VatRate.Value / 100);
                gross = net + vat;
            }
            else if (request.GrossAmount.HasValue && request.VatRate.HasValue)
            {
                gross = request.GrossAmount.Value;
                net = gross / (1 + (request.VatRate.Value / 100));
                vat = gross - net;
            }
            else if (request.VatAmount.HasValue && request.VatRate.HasValue)
            {
                vat = request.VatAmount.Value;
                net = vat / (request.VatRate.Value / 100);
                gross = net + vat;
            }

            return new VatCalculationsResponseModel()
            {
                GrossAmount = Math.Round(gross, 2),
                VatAmount = Math.Round(vat, 2),
                NetAmount = Math.Round(net, 2),
                VatRate = request.VatRate ?? 0
            };
        }
        catch (Exception ex)
        {
            throw new Exception("An error occured during calculation", ex.InnerException);
        }
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(VatCalculationsService)}.Dispose()");
    }
}
