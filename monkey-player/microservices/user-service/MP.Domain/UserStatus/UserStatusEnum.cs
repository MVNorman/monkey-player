using System.ComponentModel;

namespace MP.Domain.UserStatus
{
    public enum UserStatusEnum
    {
        [Description("Waiting for confirmation")]
        WaitingConfirmation = 0,

        [Description("Active")]
        Active = 1,

        [Description("Blocked")]
        Blocked = 2
    }
}