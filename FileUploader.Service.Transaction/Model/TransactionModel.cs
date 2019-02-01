using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploader.Service.Transaction.Model
{
    public class TransactionModel
    {
        public TransactionModel(Data.Transaction submission)
        {
            Id = submission.Id.ToString();
            Status = submission.TransactionStatus.ToString();
            FilesCount = submission.Files != null ? submission.Files.Count : 0;
            ExpectedFilesCount = submission.ExpectedFilesCount;
        }

        public string Id { get; private set; }

        public string Status { get; private set; }

        public int FilesCount { get; private set; }

        public int ExpectedFilesCount { get; private set; }
    }
}
