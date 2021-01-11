using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonkeyPlayer.Domain.User;

namespace MonkeyPlayer.Persistence.User
{

    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users", "access");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.LastName).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.CreatedAtUtc).IsRequired();

            builder.HasIndex(clusteredIndex => clusteredIndex.Email)
                .IsUnique();

            builder.HasOne(x => x.UserStatusReference)
                .WithMany(x=> x.Users)
                .HasForeignKey(fk => fk.Status)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}