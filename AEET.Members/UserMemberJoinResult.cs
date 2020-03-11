using System.Collections.Generic;

namespace AEET.Members
{
    public class UserMemberJoinResult
    {
        public IReadOnlyCollection<UserMember> UsersMembers { get; }
        public IReadOnlyCollection<User> OnlyOnUsers{ get; }

        public IReadOnlyCollection<Member> OnlyOnMembers { get; }

        public UserMemberJoinResult(IReadOnlyCollection<UserMember> userMembers, 
            IReadOnlyCollection<User> onlyOnUsers, IReadOnlyCollection<Member> onlyOnMembers)
        {
            UsersMembers = userMembers;
            OnlyOnUsers = onlyOnUsers;
            OnlyOnMembers = onlyOnMembers;
        }
    }
}