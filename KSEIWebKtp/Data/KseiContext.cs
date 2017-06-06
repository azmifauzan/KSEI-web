using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KSEIWebKtp.Models
{
    public class KseiContext : IdentityDbContext<ApplicationUser>
    {

        public KseiContext(DbContextOptions<KseiContext> options)
            : base(options)
        {
        }

        public DbSet<KSEIWebKtp.Models.User> User { get; set; }
        public DbSet<KSEIWebKtp.Models.Dataktp> Dataktp { get; set; }
        public DbSet<KSEIWebKtp.Models.Upload> Upload { get; set; }
    }
}
