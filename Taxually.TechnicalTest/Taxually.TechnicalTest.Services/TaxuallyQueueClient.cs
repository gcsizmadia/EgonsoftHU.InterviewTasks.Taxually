using System;
using System.Threading.Tasks;

using Taxually.TechnicalTest.Services.Abstractions;

namespace Taxually.TechnicalTest.Services
{
    public class TaxuallyQueueClient : ITaxuallyQueueClient
    {
        private bool disposedValue;

        public Task EnqueueAsync<TPayload>(string queueName, TPayload payload)
        {
            // Code to send to message queue removed for brevity
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
