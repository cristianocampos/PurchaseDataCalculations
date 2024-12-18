using PurchaseData.ApplicationCore.BusinessServices;
using PurchaseData.ApplicationCore.Interfaces;
using PurchaseData.ApplicationCore.Models;

namespace PurchaseData.Tests.UnitTests;

public class VatCalculatorValidLogicTests
{
    private const decimal VatRate = 20M;
    private readonly IVatCalculationsService vatCalculator;

    public VatCalculatorValidLogicTests()
    {
        vatCalculator = new VatCalculationsService();
    }

    [Fact]
    public void Calculate_ProvidedNetAmount_ShouldReturnCorrectGrossAndVat()
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
    public void Calculate_ProvidedGrossAmount_ShouldReturnCorrectNetAndVat()
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
    public void Calculate_ProvidedVatAmount_ShouldReturnCorrectNetAndGross()
    {
        // Arrange
        var vatAmount = (decimal)Math.Pow(10, 6);
        var request = new VatCalculationsRequestModel
        {
            VatAmount = vatAmount,
            VatRate = VatRate
        };

        var response = new VatCalculationsResponseModel
        {
            VatAmount = vatAmount,
            GrossAmount = 6000000M,
            NetAmount = 5000000M,
            VatRate = 20M
        };

        // Act
        var actual = vatCalculator.Calculate(request);

        // Assert
        Assert.Equal(response.NetAmount, actual.NetAmount);
        Assert.Equal(response.GrossAmount, actual.GrossAmount);
    }
}