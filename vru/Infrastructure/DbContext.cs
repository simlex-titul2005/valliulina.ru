using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using vru.Models;

namespace vru.Infrastructure
{
    public class DbContext : IdentityDbContext<User>
    {
        public DbSet<Service> Servises { get; set; }

        public DbContext() : base("DbContext") { }
    }
}