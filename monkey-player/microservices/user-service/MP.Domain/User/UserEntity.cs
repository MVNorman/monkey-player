using System;
using MP.Domain.UserStatus;
using MVNormanNativeKit.Domain.EntityRoot;

namespace MP.Domain.User
{
    public class UserEntity: IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public UserStatusReferenceEntity UserStatusReference { get; set; }

        public UserStatusEnum Status { get; set; } = UserStatusEnum.WaitingConfirmation;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}