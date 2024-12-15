using PurchaseData.ApplicationCore.Models;

namespace PurchaseData.ApplicationCore.Interfaces;

public interface IVatCalculationsService
{
    VatCalculationsResponseModel Calculate(VatCalculationsRequestModel request);
}
