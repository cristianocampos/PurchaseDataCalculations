using PurchaseData.ApplicationCore.BusinessServices;
using PurchaseData.ApplicationCore.Interfaces;
using PurchaseData.ApplicationCore.Models;

namespace PurchaseData.Tests;

public class VatCalculatorTests
{
    private const decimal VatRate = 20M;
    private readonly IVatCalculationsService vatCalculator;

    public VatCalculatorTests()
    {
        vatCalculator = new VatCalculationsService();
    }

    [Fact]
    public async Task Calculate_ProvidedNetAmount_ShouldReturnCorrectGrossAndVat()
    {
        // Arrange
        var netAmount = 76.42M;
        var request = new VatCalculationsRequestModel
        {
            NetAmount = netAmount,
            VatRate = VatRate
        };

        var response = new VatCalculationsResponseModel
        {
            NetAmount = netAmount,
            GrossAmount = 91.70M,
            VatAmount = 15.28M,
            VatRate = 20M
        };

        // Act
        var actual = vatCalculator.Calculate(request);

        // Assert
        Assert.Equal(response.GrossAmount, actual.GrossAmount);
        Assert.Equal(response.VatAmount, actual.VatAmount);
    }

    [Fact]
    public async Task Calculate_ProvidedGrossAmount_ShouldReturnCorrectNetAndVat()
    {
        // Arrange
        var grossAmount = 0.5M;
        var request = new VatCalculationsRequestModel
        {
            GrossAmount = grossAmount,
            VatRate = VatRate
        };

        var response = new VatCalculationsResponseModel
        {
            GrossAmount = grossAmount,
            NetAmount = 0.42M,
            VatAmount = 0.08M,
            VatRate = 20M
        };

        // Act
        var actual = vatCalculator.Calculate(request);

        // Assert
        Assert.Equal(response.NetAmount, actual.NetAmount);
        Assert.Equal(response.VatAmount, actual.VatAmount);
    }

    [Fact]
    public async Task Calculate_ProvidedVatAmount_ShouldReturnCorrectNetAndGross()
    {
        // Arrange
        var vatAmount = 54.27M;
        var request = new VatCalculationsRequestModel
        {
            VatAmount = vatAmount,
            VatRate = VatRate
        };

        var response = new VatCalculationsResponseModel
        {
            VatAmount = vatAmount,
            GrossAmount = 325.62M,
            NetAmount = 271.35M,
            VatRate = 20M
        };

        // Act
        var actual = vatCalculator.Calculate(request);

        // Assert
        Assert.Equal(response.NetAmount, actual.NetAmount);
        Assert.Equal(response.GrossAmount, actual.GrossAmount);
    }
}