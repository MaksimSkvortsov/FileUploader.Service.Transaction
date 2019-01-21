using Microsoft.EntityFrameworkCore;

namespace FileUploader.Service.Transaction.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<File> Files { get; set; }
    }
}
