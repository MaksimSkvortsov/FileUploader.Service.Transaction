using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileUploader.Service.Transaction.Infrastructure
{
    public class AzureServiceBus : IServiceBus
    {
        private readonly ISubscriptionClient _subscriptionClient;
        private Func<string, CancellationToken, Task> _handler;

        public AzureServiceBus(string serviceBusConnectionString, string subscriptionName)
        {
            _subscriptionClient = new SubscriptionClient(new ServiceBusConnectionStringBuilder(serviceBusConnectionString), subscriptionName);
        }

        public void RegisterHandler(Func<string, CancellationToken, Task> handler)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _handler = handler;
            _subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            //add logging

            return Task.CompletedTask;
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            string rawMessage = Encoding.UTF8.GetString(message.Body);

            await _handler(rawMessage, token);
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }
    }
}
