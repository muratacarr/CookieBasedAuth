using CustomCookieBased.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomCookieBased.Configurations
{
    public class CookieContext : DbContext
    {
        public CookieContext(DbContextOptions<CookieContext> options) : base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserRoleConfigurition());
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
    }
}
