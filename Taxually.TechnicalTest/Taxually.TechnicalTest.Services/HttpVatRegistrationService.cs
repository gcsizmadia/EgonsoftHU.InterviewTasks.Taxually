using System;
using System.Threading;
using System.Threading.Tasks;

using EgonsoftHU.Extensions.Bcl;

using Taxually.TechnicalTest.DataTransfer;
using Taxually.TechnicalTest.Services.Abstractions;

namespace Taxually.TechnicalTest.Services
{
    public class HttpVatRegistrationService : IVatRegistrationService
    {
        private readonly Func<ITaxuallyHttpClient> httpClientFactory;

        public HttpVatRegistrationService(Func<ITaxuallyHttpClient> httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task RegisterAsync(VatRegistrationRequest request, CancellationToken cancellationToken = default)
        {
            request.ThrowIfNull();

            using ITaxuallyHttpClient httpClient = httpClientFactory.Invoke();

            await httpClient.PostAsync("https://api.uktax.gov.uk", request);
        }
    }
}
