using Microsoft.EntityFrameworkCore;

namespace DropsApi.Models
{
    public class DropsContext : DbContext
    {
        public DropsContext(DbContextOptions<DropsContext> options)
            : base(options)
        {
        }


        public DbSet<DropsUser> DropsUsers { get; set; }
    }
}