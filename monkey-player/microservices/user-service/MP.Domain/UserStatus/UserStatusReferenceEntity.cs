using System.Collections.Generic;
using MP.Domain.User;
using MVNormanNativeKit.Domain.EntityRoot;
using MVNormanNativeKit.Tools.Extensions;

namespace MP.Domain.UserStatus
{
    public class UserStatusReferenceEntity: IEntity<UserStatusEnum>
    {
        public UserStatusReferenceEntity()
        {}

        public UserStatusReferenceEntity(UserStatusEnum userStatus)
        {
            Id = userStatus;
            Name = userStatus.GetDescription();
        }
        
        public UserStatusEnum Id { get; set; }
        public string Name { get; }
        
        
        public IEnumerable<UserEntity> Users { get; set; }
    }
}