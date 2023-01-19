using Autofac;

using Taxually.TechnicalTest.DataTransfer;
using Taxually.TechnicalTest.Services.Abstractions;

namespace Taxually.TechnicalTest.Services
{
    public class DependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<CsvVatRegistrationService>()
                .Named<IVatRegistrationService>(CountryCodes.France)
                .InstancePerLifetimeScope();

            builder
                .RegisterType<XmlVatRegistrationService>()
                .Named<IVatRegistrationService>(CountryCodes.Germany)
                .InstancePerLifetimeScope();

            builder
                .RegisterType<HttpVatRegistrationService>()
                .Named<IVatRegistrationService>(CountryCodes.UnitedKingdom)
                .InstancePerLifetimeScope();

            builder
                .RegisterType<VatRegistrationServiceProxy>()
                .As<IVatRegistrationService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<TaxuallyHttpClient>()
                .As<ITaxuallyHttpClient>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<TaxuallyQueueClient>()
                .As<ITaxuallyQueueClient>()
                .InstancePerLifetimeScope();
        }
    }
}
