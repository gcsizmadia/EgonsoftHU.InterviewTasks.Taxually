using System;

using Autofac;

using FluentAssertions;

using Taxually.TechnicalTest.DataTransfer;
using Taxually.TechnicalTest.Services.Abstractions;

using Xunit;

namespace Taxually.TechnicalTest.Services.Tests
{
    public class VatRegistrationServiceProxyTests
    {
        private readonly IContainer container;

        public VatRegistrationServiceProxyTests()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<DependencyModule>();

            container = builder.Build();
        }

        [Theory]
        [InlineData(CountryCodes.France, typeof(CsvVatRegistrationService))]
        [InlineData(CountryCodes.Germany, typeof(XmlVatRegistrationService))]
        [InlineData(CountryCodes.UnitedKingdom, typeof(HttpVatRegistrationService))]
        public void WhenSupportedCountry_ThenAppropriateServiceSelected(string countryCode, Type serviceType)
        {
            // Arrange
            IVatRegistrationService? service = null;

            // Act
            Action sut = () => service = container.ResolveNamed<IVatRegistrationService>(countryCode);

            // Assert
            sut.Should().NotThrow();
            service.Should().NotBeNull();
            service.Should().BeOfType(serviceType);
        }

        [Theory]
        [InlineData(CountryCodes.France)]
        [InlineData(CountryCodes.Germany)]
        [InlineData(CountryCodes.UnitedKingdom)]
        public void WhenSupportedCountry_ThenNoCountryNotSupportedExceptionThrown(string countryCode)
        {
            // Arrange
            IVatRegistrationService service = container.Resolve<IVatRegistrationService>();

            // Act
            Action sut = () => service.RegisterAsync(new VatRegistrationRequest() { Country = countryCode });

            // Assert
            sut.Should().NotThrow<CountryNotSupportedException>();
        }

        [Fact]
        public void WhenNotSupportedCountry_ThenExceptionThrown()
        {
            // Arrange
            IVatRegistrationService service = container.Resolve<IVatRegistrationService>();
            string notSupportedCountryCode = "HU";
            string errorMessage = $"Country not supported: {notSupportedCountryCode}";

            // Act
            Action sut = () => service.RegisterAsync(new VatRegistrationRequest() { Country = notSupportedCountryCode });

            // Assert
            sut.Should().ThrowExactly<CountryNotSupportedException>().WithMessage(errorMessage);
        }
    }
}
