using System;
using System.ComponentModel.DataAnnotations;
using static MVNormanNativeKit.Tools.Helpers.DateTimeHelper;

namespace MVNormanNativeKit.Domain.EntityRoot
{
    public abstract class EntityBase<TId> : IEntity<TId>
    {
        protected EntityBase(TId id)
        {
            Id = id;
            CreatedAt = NewDateTime();
        }

        public DateTime CreatedAt { get; protected set; }

        public DateTime? UpdatedAt { get; protected set; }

        [Key] public TId Id { get; protected set; }
    }
}
