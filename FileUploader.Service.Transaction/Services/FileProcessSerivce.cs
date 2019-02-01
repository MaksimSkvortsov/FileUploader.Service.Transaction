using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FileUploader.Service.Transaction.Data;
using FileUploader.Service.Transaction.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileUploader.Service.Transaction.Services
{
    public class FileProcessSerivce : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IServiceBus _serviceBus;
        private readonly IServiceScopeFactory _scopeFactory;

        public FileProcessSerivce(ILogger<FileProcessSerivce> logger, IServiceBus sericeBus, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _serviceBus = sericeBus;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _serviceBus.RegisterHandler(ProcessMessage);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        private async Task ProcessMessage(string message, CancellationToken token)
        {
            _logger.LogInformation("ProcessMessage. File received " + message);

            FileUploadInfo uploadInfo = null;
            try
            {
                uploadInfo = JsonConvert.DeserializeObject<FileUploadInfo>(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProcessMessage. Deserialization failed for file " + message);
            }


            _logger.LogInformation("ProcessMessage. File parsed. Message " + message);

            try
            {
                await AddFileToTransaction(uploadInfo);
            }
            catch(InvalidOperationException ex)
            {
                _logger.LogError(ex, "Add file information failed", new { UploadInfo = uploadInfo });
            }            

            _logger.LogInformation("ProcessMessage. Completed. Message: " + message);
        }

        private async Task AddFileToTransaction(FileUploadInfo fileUploadInfo)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                var transactionExists = context.Transactions.Any(s => s.Id.ToString() == fileUploadInfo.TransactionId);
                if(transactionExists)
                {
                    context.Files.Add(new File {  Path = fileUploadInfo.FilePath, TransactionId = Guid.Parse(fileUploadInfo.TransactionId) });
                }
                else
                {
                    throw new InvalidOperationException("Transaction does not exist");
                }
                                
                await context.SaveChangesAsync();
            }
        }
    }
}
