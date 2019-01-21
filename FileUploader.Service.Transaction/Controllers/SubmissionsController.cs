using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FileUploader.Service.Transaction.Data;
using FileUploader.Service.Transaction.Model;

namespace FileUploader.Service.Transaction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        private readonly ILogger<SubmissionsController> _logger;
        private readonly DataContext _dataContext;


        public SubmissionsController(ILogger<SubmissionsController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TransactionModel>> Get()
        {
            _logger.LogInformation("Submissions.get called");
            return _dataContext.Transactions.Select(s => new TransactionModel(s)).ToList();
        }

        [HttpPost]
        public IActionResult Post(NewTransactionModel model)
        {
            Guid submissionId = Guid.Parse(model.Id);

            var existingSubmission = _dataContext.Transactions.FirstOrDefault(s => s.Id == submissionId);
            if(existingSubmission == null)
            {
                _dataContext.Transactions.Add(new Data.Transaction { Id = submissionId, ExpectedFilesCount = model.ExpectedFilesCount, TransactionStatus = TransactionStatus.InProgress, TransactionDate = DateTime.Now });
            }
            else
            {
                existingSubmission.ExpectedFilesCount = model.ExpectedFilesCount;
            }

            _dataContext.SaveChanges();
            
            return NoContent();
        }
    }
}
