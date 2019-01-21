using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileUploader.Service.Transaction.Infrastructure
{
    public interface IServiceBus
    {
        void RegisterHandler(Func<string, CancellationToken, Task> handler);
    }
}
