using PurchaseData.ApplicationCore.BusinessServices;
using PurchaseData.ApplicationCore.Context;
using PurchaseData.ApplicationCore.Interfaces;
using PurchaseData.ApplicationCore.Models;
using System.ComponentModel.DataAnnotations;

namespace PurchaseData.Tests.UnitTests;

public class VatCalculatorVatRateTests
{
    private readonly IVatRateService vatValidator;
    private readonly IVatRateServiceContext vatServiceContext;

    public VatCalculatorVatRateTests()
    {
        vatValidator = new AustriaVatRateService();
        vatServiceContext = new VatRateServiceContext(new List<IVatRateService> { vatValidator });
    }

    [Fact]
    public void Calculate_WithInvalidVatRate_ShouldFailValidation()
    {
        // Arrange
        var vatRate = 15M;

        // Act
        var actual = vatValidator.IsValidVatRate(vatRate);

        // Assert
        Assert.False(actual);
    }

    [Fact]
    public void Calculate_WithZeroVatRate_ShouldFailValidation()
    {
        // Arrange
        var vatRate = 0M;

        // Act
        var actual = vatValidator.IsValidVatRate(vatRate);

        // Assert
        Assert.False(actual);
    }

    [Fact]
    public void Calculate_WithMissingVatRate_ShouldFailValidation()
    {
        // Arrange
        var request = new VatCalculationsRequestModel
        {
            GrossAmount = 0.5M
        };
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        // Act
        var actual = Validator.TryValidateObject(request, context, results, true);

        // Assert
        Assert.False(actual);
        Assert.NotNull(results);
        Assert.Contains(results, v => v?.ErrorMessage?.Contains("VAT rate is required") == true);
    }

    [Fact]
    public void Calculate_WithNullVatRate_ShouldFailValidation()
    {
        // Arrange
        var request = new VatCalculationsRequestModel
        {
            GrossAmount = 0.5M,
            VatRate = null
        };
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        // Act
        var actual = Validator.TryValidateObject(request, context, results, true);

        // Assert
        Assert.False(actual);
        Assert.NotNull(results);
        Assert.Contains(results, v => v?.ErrorMessage?.Contains("VAT rate is required") == true);
    }

    [Fact]
    public void Calculate_WithValidVatRate_ShouldPassValidatorValidation()
    {
        var request = new VatCalculationsRequestModel
        {
            GrossAmount = 0.5M,
            VatRate = 10M
        };
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        // Act
        var actual = Validator.TryValidateObject(request, context, results, true);

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void Calculate_WithValidVatRate_ShouldPassValidation()
    {
        // Arrange
        var vatRate = 10M;

        // Act
        var actual = vatValidator.IsValidVatRate(vatRate);

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void Calculate_WithInvalidCountry_ShouldFailValidation()
    {
        // Arrange
        var invalidCountry = "Spain";

        // Act and Assert
        Assert.Throws<NotSupportedException>(() => vatServiceContext.GetStrategy(invalidCountry));
    }
}
