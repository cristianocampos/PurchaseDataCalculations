using PurchaseData.ApplicationCore.Models;
using System.ComponentModel.DataAnnotations;

namespace PurchaseData.ApplicationCore.Validation;

public class ValidateAmountInputAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance is not
            VatCalculationsRequestModel requestEntity)
        {
            throw new Exception($"Attribute " +
                $"{nameof(ValidateAmountInputAttribute)} " +
                $"must be applied to a " +
                $"{nameof(VatCalculationsRequestModel)} or derived type.");
        }

        var validInputs = new[] { requestEntity.NetAmount, requestEntity.GrossAmount, requestEntity.VatAmount }.Count(x => x.HasValue);
        if (validInputs > 1)
        {
            return new ValidationResult("More than one amount input provided.");
        } 
        else if (validInputs < 1)
        {
            return new ValidationResult("Missing amount input.");
        }

        return ValidationResult.Success;
    }
}
