﻿using PurchaseData.ApplicationCore.Validation;
using System.ComponentModel.DataAnnotations;

namespace PurchaseData.ApplicationCore.Models;

[ValidateAmountInputAttribute]
public class VatCalculationsRequestModel
{
    [NonZero]
    public decimal? NetAmount { get; set; }

    [NonZero]
    public decimal? GrossAmount { get; set; }

    [NonZero]
    public decimal? VatAmount { get; set; }

    [Required(ErrorMessage = "VAT rate is required")]
    //[AllowedValues(10, 13, 20)]
    public decimal? VatRate { get; set; }
}
