using PurchaseData.ApplicationCore.Models;
using System.ComponentModel.DataAnnotations;

namespace PurchaseData.Tests.UnitTests;

public class VatCalculatorAmountInputTests
{
    [Fact]
    public void Calculate_WithValidAmountInputs_ShouldPassValidation()
    {
        var request = new VatCalculationsRequestModel
        {
            NetAmount = 23.2M,
            VatRate = 10M
        };

        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        var actual = Validator.TryValidateObject(request, context, results, true);

        Assert.True(actual);
    }
    
    [Fact]
    public void Calculate_WithMissAmount_ShouldFailValidation()
    {
        var request = new VatCalculationsRequestModel
        {
            VatRate = 10M
        };

        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        var actual = Validator.TryValidateObject(request, context, results, true);

        Assert.False(actual);
        Assert.Contains(results, v => v.ErrorMessage.Contains("Missing amount input."));
    }

    [Fact]
    public void Calculate_WithAmountNull_ShouldFailValidation()
    {
        var request = new VatCalculationsRequestModel
        {
            GrossAmount = null,
            VatRate = 10M
        };
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        var actual = Validator.TryValidateObject(request, context, results, true);

        Assert.False(actual);
        Assert.Contains(results, v => v.ErrorMessage.Contains("Missing amount input."));
    }

    [Fact]
    public void Calculate_WithZeroNetAmount_ShouldFailValidation()
    {
        var request = new VatCalculationsRequestModel
        {
            NetAmount = 0M,
            VatRate = 10M
        };
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        var actual = Validator.TryValidateObject(request, context, results, true);

        Assert.False(actual);
        Assert.Contains(results, v => v.ErrorMessage.Contains("NetAmount must be different than 0"));
    }

    [Fact]
    public void Calculate_WithZeroGrossAmount_ShouldFailValidation()
    {
        var request = new VatCalculationsRequestModel
        {
            GrossAmount = 0M,
            VatRate = 10M
        };
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        var actual = Validator.TryValidateObject(request, context, results, true);

        Assert.False(actual);
        Assert.Contains(results, v => v.ErrorMessage.Contains("GrossAmount must be different than 0"));
    }

    [Fact]
    public void Calculate_WithMultipleInputs_ShouldFailValidation()
    {
        var request = new VatCalculationsRequestModel
        {
            NetAmount = 100M,
            GrossAmount = 120M,
            VatRate = 10M
        };
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        var actual = Validator.TryValidateObject(request, context, results, true);
        
        Assert.False(actual);
        Assert.Contains(results, v => v.ErrorMessage.Contains("More than one amount input provided."));
    }


    [Fact]
    public void Calculate_WithGrossAmountNullButNetAmountValid_ShouldPassValidation()
    {
        var request = new VatCalculationsRequestModel
        {
            GrossAmount = null,
            NetAmount = 10M,
            VatRate = 10M
        };
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        var actual = Validator.TryValidateObject(request, context, results, true);

        Assert.True(actual);
    }
}
