using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploader.Service.Transaction.Data
{
    public class File
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public Transaction Transaction { get; set; }

        public Guid TransactionId { get; set; }
    }
}
