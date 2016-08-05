using System.Data.Entity;
using vru.Models;

namespace vru.Infrastructure
{
    public class DbContext : SX.WebCore.SxDbContext
    {
        public DbContext() : base("DbContext") { }

        public new DbSet<Article> Articles { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Service> Servises { get; set; }

        public DbSet<Situation> Situations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}