using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class _DBContext:DbContext
    {
        public _DBContext(DbContextOptions<_DBContext> options)
         : base(options)
        {
        }

       protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Sample> Samples { get; set; }
        public DbSet<FileUp> FilesUp { get; set; }
    }
}
