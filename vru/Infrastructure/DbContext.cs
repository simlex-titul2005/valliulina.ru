using System.Data.Entity;
using vru.Models;

namespace vru.Infrastructure
{
    public class DbContext : SX.WebCore.SxDbContext
    {
        public DbSet<Education> Educations { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Service> Servises { get; set; }

        public DbContext() : base("DbContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}