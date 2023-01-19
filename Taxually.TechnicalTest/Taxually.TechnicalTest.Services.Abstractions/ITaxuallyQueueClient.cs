using System;
using System.Threading.Tasks;

namespace Taxually.TechnicalTest.Services.Abstractions
{
    public interface ITaxuallyQueueClient : IDisposable
    {
        Task EnqueueAsync<TPayload>(string queueName, TPayload payload);
    }
}
