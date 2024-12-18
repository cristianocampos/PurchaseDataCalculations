﻿using PurchaseData.ApplicationCore.Models;
using System.ComponentModel.DataAnnotations;

namespace PurchaseData.ApplicationCore.Validation;

public class GreaterThanZeroAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance is not
            VatCalculationsRequestModel requestEntity)
        {
            throw new Exception($"Attribute " +
                $"{nameof(GreaterThanZeroAttribute)} " +
                $"must be applied to a " +
                $"{nameof(VatCalculationsRequestModel)} or derived type.");
        }

        if (value == null)
        {
            return ValidationResult.Success;
        }

        if (value is decimal decimalValue && decimalValue > 0)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult($"{validationContext.DisplayName} must be greater than 0");
    }
}
