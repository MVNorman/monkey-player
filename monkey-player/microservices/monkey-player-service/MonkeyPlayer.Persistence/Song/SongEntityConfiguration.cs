using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonkeyPlayer.Domain.Song;

namespace MonkeyPlayer.Persistence.Song
{
    public class SongEntityConfiguration : IEntityTypeConfiguration<SongEntity>
    {
        public void Configure(EntityTypeBuilder<SongEntity> builder)
        {
            builder.ToTable("Songs", "player");

            builder.HasKey(x => x.Id);
        }
    }
}