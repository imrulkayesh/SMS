using Microsoft.EntityFrameworkCore;
using SMS.Models;

namespace SMS.Data
{
    public class SMSDBContext : DbContext
    {
        public SMSDBContext(DbContextOptions<SMSDBContext> options): base(options) { }
        public DbSet<User>? User { get; set; } // Example DbSet. Replace 'User' with your actual entity
    }
}
