using System;
using FluentValidation;
using MVNormanNativeKit.Infrastructure.Core.Events;

namespace MP.Shared.Events.User
{
    public class UserDeletedEvent : Event
    {
        public Guid UserId { get; set; }

        public class Validator : AbstractValidator<UserDeletedEvent>
        {
            public Validator()
            {
                RuleFor(e => e.UserId).NotEmpty();
            }
        }
    }
}