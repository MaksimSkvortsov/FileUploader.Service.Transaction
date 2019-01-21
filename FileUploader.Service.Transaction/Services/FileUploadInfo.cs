using System;

namespace FileUploader.Service.Transaction.Services
{
    public class FileUploadInfo
    {
        public string TransactionId { get; set; }

        public string FilePath { get; set; }

        public DateTime ReceiveDate { get; set; }
    }
}