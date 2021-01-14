using System;
using FluentValidation;
using MVNormanNativeKit.Infrastructure.Core.Events;

namespace MP.Shared.Events.Song
{
    public class SongCreatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string StyleType { get; set; }

        public class Validator : AbstractValidator<SongCreatedEvent>
        {
            public Validator()
            {
                RuleFor(e => e.Id).NotEmpty();
                RuleFor(e => e.Name).NotEmpty();
                RuleFor(e => e.StyleType).NotEmpty();
            }
        }
    }
}