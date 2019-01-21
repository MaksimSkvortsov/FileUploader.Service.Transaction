using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploader.Service.Transaction.Model
{
    public class NewTransactionModel
    {
        public string Id { get; set; }
        
        public int ExpectedFilesCount { get; set; }
    }
}
