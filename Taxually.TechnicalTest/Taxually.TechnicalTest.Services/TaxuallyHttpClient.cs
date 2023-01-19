using System;
using System.Threading.Tasks;

using Taxually.TechnicalTest.Services.Abstractions;

namespace Taxually.TechnicalTest.Services
{
    public class TaxuallyHttpClient : ITaxuallyHttpClient
    {
        private bool disposedValue;

        public Task PostAsync<TRequest>(string url, TRequest request)
        {
            // Actual HTTP call removed for purposes of this exercise
            return Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
