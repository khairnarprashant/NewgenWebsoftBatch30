using Microsoft.EntityFrameworkCore;
using NewgenWebsoftBatch30.Models;

namespace NewgenWebsoftBatch30.DataContext
{
    public class NewgenWebsoftDbContext: DbContext
    {
        public NewgenWebsoftDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }  

        public DbSet<Employee> Employees { get; set; }

    }
}
