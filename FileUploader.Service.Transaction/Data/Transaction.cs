using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploader.Service.Transaction.Data
{
    public enum TransactionStatus { InProgress, Failed, Success }

    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public DateTime TransactionDate { get; set; }

        public TransactionStatus TransactionStatus { get; set; }

        public int ExpectedFilesCount { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}
