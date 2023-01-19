using System.Threading;
using System.Threading.Tasks;

using Autofac.Features.Indexed;

using EgonsoftHU.Extensions.Bcl;

using Taxually.TechnicalTest.DataTransfer;
using Taxually.TechnicalTest.Services.Abstractions;

namespace Taxually.TechnicalTest.Services
{
    public class VatRegistrationServiceProxy : IVatRegistrationService
    {
        private readonly IIndex<string, IVatRegistrationService> services;

        public VatRegistrationServiceProxy(IIndex<string, IVatRegistrationService> services)
        {
            this.services = services;
        }

        public Task RegisterAsync(VatRegistrationRequest request, CancellationToken cancellationToken = default)
        {
            request.ThrowIfNull();

            if (!services.TryGetValue(request.Country, out IVatRegistrationService service))
            {
                throw new CountryNotSupportedException(request.Country);
            }

            return service.RegisterAsync(request, cancellationToken);
        }
    }
}
