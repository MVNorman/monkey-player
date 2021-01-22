using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.Domain.UserStatus;

namespace MP.Data.UserStatus
{
    public class UserStatusReferenceEntityConfiguration : IEntityTypeConfiguration<UserStatusReferenceEntity>
    {
        public void Configure(EntityTypeBuilder<UserStatusReferenceEntity> builder)
        {
            builder.ToTable("UserStatusReferences", "access");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            
            builder.HasData(Enum.GetValues(
                    typeof(UserStatusEnum))
                .Cast<UserStatusEnum>()
                .Select(userStatus => new UserStatusReferenceEntity(userStatus)).ToArray());
        }
    }
}