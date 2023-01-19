using System;
using System.Threading.Tasks;

namespace Taxually.TechnicalTest.Services.Abstractions
{
    public interface ITaxuallyHttpClient : IDisposable
    {
        Task PostAsync<TRequest>(string url, TRequest request);
    }
}
