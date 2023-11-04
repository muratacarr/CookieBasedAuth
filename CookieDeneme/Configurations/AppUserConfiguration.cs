using CustomCookieBased.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCookieBased.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasData(new AppUser
            {
                Id = 1,
                Username = "Murat",
                Password = "1",
            });
            builder.Property(x => x.Username).HasMaxLength(200).IsRequired();
            builder.Property(x=>x.Password).HasMaxLength(200).IsRequired();
        }
    }
}
