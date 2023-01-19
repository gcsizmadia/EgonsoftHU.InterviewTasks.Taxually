using System;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using Moq;

using Taxually.TechnicalTest.DataTransfer;
using Taxually.TechnicalTest.Services.Abstractions;

using Xunit;

namespace Taxually.TechnicalTest.Services.Tests
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class VatRegistrationServiceTests
    {
        [Fact]
        public async Task WhenValidRequest_ThenNoExceptionThrownByHttpRequest()
        {
            // Arrange
            ITaxuallyHttpClient client = Mock.Of<ITaxuallyHttpClient>();
            IVatRegistrationService service = new HttpVatRegistrationService(() => client);
            
            var request = new VatRegistrationRequest()
            {
                CompanyId = "C",
                CompanyName = "Company",
                Country = CountryCodes.UnitedKingdom
            };

            Mock.Get(client)
                .Setup(httpClient => httpClient.PostAsync(It.IsAny<string>(), It.IsAny<VatRegistrationRequest>()))
                .Verifiable();

            // Act
            Func<Task> sut = async () => await service.RegisterAsync(request);

            // Assert
            await sut.Should().NotThrowAsync();

            Mock.Get(client)
                .Verify(httpClient => httpClient.PostAsync("https://api.uktax.gov.uk", request), Times.Once());
        }

        [Fact]
        public async Task WhenValidRequest_ThenValidCsvEnqueued()
        {
            // Arrange
            ITaxuallyQueueClient client = Mock.Of<ITaxuallyQueueClient>();
            IVatRegistrationService service = new CsvVatRegistrationService(() => client);

            var request = new VatRegistrationRequest()
            {
                CompanyId = "C",
                CompanyName = "Company"
            };

            string csvContent =
                new StringBuilder()
                    .AppendLine("CompanyName,CompanyId")
                    .AppendLine($"{request.CompanyName},{request.CompanyId}")
                    .ToString();

            byte[] expected = Encoding.UTF8.GetBytes(csvContent);

            Mock.Get(client)
                .Setup(queueClient => queueClient.EnqueueAsync(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Verifiable();

            // Act
            Func<Task> sut = async () => await service.RegisterAsync(request);

            // Assert
            await sut.Should().NotThrowAsync();

            Mock.Get(client)
                .Verify(queueClient => queueClient.EnqueueAsync("vat-registration-csv", expected), Times.Once());
        }
    }
}
