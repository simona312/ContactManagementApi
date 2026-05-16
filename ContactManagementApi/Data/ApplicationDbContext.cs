using ContactManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Contact> Contacts { get; set; }
    }
}
