using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Player.Domain.Song;

namespace Player.Data.Song
{
    public class SongEntityConfiguration : IEntityTypeConfiguration<SongEntity>
    {
        public void Configure(EntityTypeBuilder<SongEntity> builder)
        {
            builder.ToTable("Songs", "player");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.DurationInMinutes).IsRequired();
            builder.Property(x => x.StyleType).IsRequired();
            builder.Property(x => x.SongInBytes).IsRequired();
        }
    }
}