using Microsoft.EntityFrameworkCore;
using RestfullApi.Models.Entities;

namespace RestfullApi.Data
{
    public class ApplicationDbConext : DbContext
    {
        public ApplicationDbConext(DbContextOptions<ApplicationDbConext> options):base(options)
        {
            
        }

        public DbSet<Employee>Employees { get; set; }

    }
}
