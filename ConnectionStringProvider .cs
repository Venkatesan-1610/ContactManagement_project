using EmployeeDetails.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDetails.Services
{
    public class ConnectionStringProvider : DbContext
    {
        public ConnectionStringProvider(DbContextOptions<ConnectionStringProvider> options) : base(options)
        {
        }

        public DbSet<EmployeeDetailsRequest> Contacts { get; set; }
    }
}
