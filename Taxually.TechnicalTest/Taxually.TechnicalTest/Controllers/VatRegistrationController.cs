using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Taxually.TechnicalTest.DataTransfer;
using Taxually.TechnicalTest.Services.Abstractions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        private readonly IVatRegistrationService vatRegistrationService;

        public VatRegistrationController(IVatRegistrationService vatRegistrationService)
        {
            this.vatRegistrationService = vatRegistrationService;
        }

        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] VatRegistrationRequest request, CancellationToken cancellationToken)
        {
            await vatRegistrationService.RegisterAsync(request, cancellationToken);

            return NoContent();
        }
    }
}
