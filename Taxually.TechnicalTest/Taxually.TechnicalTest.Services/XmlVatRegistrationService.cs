using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

using EgonsoftHU.Extensions.Bcl;

using Taxually.TechnicalTest.DataTransfer;
using Taxually.TechnicalTest.Services.Abstractions;

namespace Taxually.TechnicalTest.Services
{
    public class XmlVatRegistrationService : IVatRegistrationService
    {
        private readonly Func<ITaxuallyQueueClient> queueClientFactory;

        public XmlVatRegistrationService(Func<ITaxuallyQueueClient> queueClientFactory)
        {
            this.queueClientFactory = queueClientFactory;
        }

        public async Task RegisterAsync(VatRegistrationRequest request, CancellationToken cancellationToken = default)
        {
            request.ThrowIfNull();

            using var stringWriter = new StringWriter();

            var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
            serializer.Serialize(stringWriter, request);

            string xmlContent = stringWriter.ToString();

            using ITaxuallyQueueClient xmlQueueClient = queueClientFactory.Invoke();
            await xmlQueueClient.EnqueueAsync("vat-registration-xml", xmlContent);
        }
    }
}
