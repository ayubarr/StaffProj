using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StaffProj.DAL.SqlServer.Configuration;
using StaffProj.Domain.Models.Abstractions.BaseUsers;
using StaffProj.Domain.Models.Entities;

namespace StaffProj.DAL.SqlServer
{
    public class PgDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Employee> Employees { get; set; }

        public PgDbContext() : base()
        {
        }

        public PgDbContext(DbContextOptions<PgDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationUserConfig).Assembly);

        }
    }
}
