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
        private readonly ILogger<VatCalculationsController> Logger;
        private readonly IVatCalculationsService Service;

        public VatCalculationsController(ILogger<VatCalculationsController> logger, IVatCalculationsService service)
        {
            Logger = logger;
            Service = service;
        }

        [HttpPost]
        public IActionResult CalculateVat(VatCalculationsRequestModel requestEntity)
        {
            //if (!Service.ValidateVAT(request.VatRate))
            //{
            //    // TODO: Add meaningfull message
            //    return BadRequest();
            //}

            return Ok(Service.Calculate(requestEntity));
        }
    }
}
