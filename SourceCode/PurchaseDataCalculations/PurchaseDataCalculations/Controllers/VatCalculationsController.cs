using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseData.ApplicationCore.Interfaces;
using PurchaseData.ApplicationCore.Models;

namespace PurchaseData.InterfaceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatCalculationsController : ControllerBase
    {
        private const string Country = "Austria";
        private readonly ILogger<VatCalculationsController> Logger;
        private readonly IVatCalculationsService Service;
        private readonly IVatRateServiceFactory RateServiceFactory;

        public VatCalculationsController(ILogger<VatCalculationsController> logger, IVatCalculationsService service, IVatRateServiceFactory rateServiceFactory)
        {
            Logger = logger;
            Service = service;
            RateServiceFactory = rateServiceFactory;
        }

        [HttpPost]
        public IActionResult CalculateVat(VatCalculationsRequestModel requestEntity)
        {
            var vatRateStrategy = RateServiceFactory.GetStrategy(Country);
            if (!vatRateStrategy.IsValidVatRate(requestEntity.VatRate.Value))
            {
                ModelState.AddModelError(
                    nameof(requestEntity.VatRate),
                    $"Invalid VAT rate for {Country}. Accepted values are: {string.Join(", ", vatRateStrategy.GetVatRates())}"
                );

                return ValidationProblem();
            }

            return Ok(Service.Calculate(requestEntity));
        }
    }
}
