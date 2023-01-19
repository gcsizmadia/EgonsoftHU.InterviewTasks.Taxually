using System.Threading;
using System.Threading.Tasks;

using Taxually.TechnicalTest.DataTransfer;

namespace Taxually.TechnicalTest.Services.Abstractions
{
    public interface IVatRegistrationService
    {
        Task RegisterAsync(VatRegistrationRequest request, CancellationToken cancellationToken = default);
    }
}
