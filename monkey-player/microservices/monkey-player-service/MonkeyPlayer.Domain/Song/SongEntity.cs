using System;
using MVNormanNativeKit.Domain.EntityRoot;

namespace MonkeyPlayer.Domain.Song
{
    public class SongEntity: IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? ReleasedAt { get; set; }
        public int DurationInMinutes { get; set; }
        public SongStyleType StyleType { get; set; }
        public byte[] SongInBytes { get; set; }
    }
}