using Full_Stack_a_Web_API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Full_Stack_a_Web_API.Data
{
    public class CustomerDbContext: DbContext
    {
        public CustomerDbContext(DbContextOptions options): base(options)
        { 
        }

        public DbSet<Customer> Customers { get; set; }
        
    }
}
