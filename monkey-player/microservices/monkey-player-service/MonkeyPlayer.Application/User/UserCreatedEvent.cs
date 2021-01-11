using System;
using FluentValidation;
using MVNormanNativeKit.Infrastructure.Core.Events;

namespace MonkeyPlayer.Application.User
{
    public class UserCreatedEvent : IEvent
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public class Validator : AbstractValidator<UserCreatedEvent>
        {
            public Validator()
            {
                RuleFor(e => e.UserId).NotEmpty();
                RuleFor(e => e.FirstName).NotEmpty();
                RuleFor(e => e.LastName).NotEmpty();
                RuleFor(e => e.Email).NotEmpty().EmailAddress();
            }
        }
    }
}