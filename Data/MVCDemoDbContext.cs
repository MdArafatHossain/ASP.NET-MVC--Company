using Company.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Company.Data
{
    public class MVCDemoDbContext : DbContext
    {
    
        
        public MVCDemoDbContext(DbContextOptions options) : base(options)   //Constructor
        {
        }

        //properties are used to access to the table that EFC will create in our database
        public DbSet <Employee> Employees{ get; set; }

    }
}
