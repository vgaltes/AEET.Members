using System.Collections.Generic;
using System.Linq;

namespace AEET.Members
{
    public static class UsersMembersJoiner
    {
        public static UserMemberJoinResult Join(IReadOnlyCollection<User> users, IReadOnlyCollection<Member> members)
        {
            var onlyOnUsers = users
                .Where( u => !members.Any(m => m.Docidentidad == u.docidentidad));

            var onlyOnMembers = members
                .Where( m => !users.Any(u => u.docidentidad == m.Docidentidad));

            var userMembers = 
                from u in users join m in members
                on u.docidentidad equals m.Docidentidad
                select new UserMember { User = u, Member = m };

            return new UserMemberJoinResult(
                userMembers.Concat(
                    onlyOnUsers.Select(u => new UserMember{User = u, Member = new Member()}))
                .ToList(),
                onlyOnUsers.ToList(),
                onlyOnMembers.ToList());
        }
    }
}