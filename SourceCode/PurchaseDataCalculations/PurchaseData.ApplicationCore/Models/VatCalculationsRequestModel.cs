using PurchaseData.ApplicationCore.Validation;
using System.ComponentModel.DataAnnotations;

namespace PurchaseData.ApplicationCore.Models;

public class VatCalculationsRequestModel
{
    [GreaterThanZero]
    public decimal? NetAmount { get; set; }

    [GreaterThanZero]
    public decimal? GrossAmount { get; set; }

    [GreaterThanZero]
    public decimal? VatAmount { get; set; }

    [Required(ErrorMessage = "VAT rate is required")]
    [GreaterThanZero]
    public decimal? VatRate { get; set; }
}
