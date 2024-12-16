﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseData.ApplicationCore.Interfaces;
using PurchaseData.ApplicationCore.Models;

namespace PurchaseData.InterfaceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatCalculationsController : ControllerBase
    {
        private const string _country = "Austria";
        private readonly ILogger<VatCalculationsController> _logger;
        private readonly IVatCalculationsService _service;
        private readonly IVatRateServiceFactory _rateServiceFactory;

        public VatCalculationsController(ILogger<VatCalculationsController> logger, IVatCalculationsService service, IVatRateServiceFactory rateServiceFactory)
        {
            _logger = logger;
            _service = service;
            _rateServiceFactory = rateServiceFactory;
        }

        [HttpPost]
        public IActionResult CalculateVat(VatCalculationsRequestModel requestEntity)
        {
            var vatRateStrategy = _rateServiceFactory.GetStrategy(_country);
            if (!vatRateStrategy.IsValidVatRate(requestEntity.VatRate.Value))
            {
                ModelState.AddModelError(
                    nameof(requestEntity.VatRate),
                    $"Invalid VAT rate for {_country}. Accepted values are: {string.Join(", ", vatRateStrategy.GetVatRates())}"
                );

                return ValidationProblem();
            }

            return Ok(_service.Calculate(requestEntity));
        }
    }
}
