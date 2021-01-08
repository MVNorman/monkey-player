using System;
using System.Collections.Generic;
using MVNormanNativeKit.Domain.EntityRoot;
using MVNormanNativeKit.Domain.EventRoot;

namespace MonkeyPlayer.Domain.Song
{
    public class SongEntity: IAggregateRoot<Guid>
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public DateTime? ReleasedAt { get; set; }
        public int DurationInMinutes { get; set; }
        public SongStyleType StyleType { get; set; }
        public byte[] SongInBytes { get; set; }

        public IAggregateRoot<Guid> ApplyEvent(IEvent payload)
        {
            throw new NotImplementedException();
        }

        public List<IEvent> GetUncommittedEvents()
        {
            throw new NotImplementedException();
        }

        public void ClearUncommittedEvents()
        {
            throw new NotImplementedException();
        }

        public IAggregateRoot<Guid> RemoveEvent(IEvent @event)
        {
            throw new NotImplementedException();
        }

        public IAggregateRoot<Guid> AddEvent(IEvent uncommittedEvent)
        {
            throw new NotImplementedException();
        }

        public IAggregateRoot<Guid> RegisterHandler<T>(Action<T> handler)
        {
            throw new NotImplementedException();
        }
    }
}