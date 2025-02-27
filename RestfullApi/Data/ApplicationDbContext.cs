using Microsoft.EntityFrameworkCore;
using RestfullApi.Models.Entities;

namespace RestfullApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Employee>Employees { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}
