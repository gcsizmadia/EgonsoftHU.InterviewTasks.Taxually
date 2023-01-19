using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using EgonsoftHU.Extensions.Bcl;

using Taxually.TechnicalTest.DataTransfer;
using Taxually.TechnicalTest.Services.Abstractions;

namespace Taxually.TechnicalTest.Services
{
    public class CsvVatRegistrationService : IVatRegistrationService
    {
        private readonly Func<ITaxuallyQueueClient> queueClientFactory;

        public CsvVatRegistrationService(Func<ITaxuallyQueueClient> queueClientFactory)
        {
            this.queueClientFactory = queueClientFactory;
        }

        public async Task RegisterAsync(VatRegistrationRequest request, CancellationToken cancellationToken = default)
        {
            request.ThrowIfNull();

            string csvContent =
                new StringBuilder()
                    .AppendLine("CompanyName,CompanyId")
                    .AppendLine($"{request.CompanyName},{request.CompanyId}")
                    .ToString();

            byte[] csvBytes = Encoding.UTF8.GetBytes(csvContent);

            using ITaxuallyQueueClient excelQueueClient = queueClientFactory.Invoke();

            await excelQueueClient.EnqueueAsync("vat-registration-csv", csvBytes);
        }
    }
}
