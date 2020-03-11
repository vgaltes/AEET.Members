using System.Collections.ObjectModel;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace AEET.Members.Tests
{
    public class UsersMembersJoinerShould
    {
        [Fact]
        public void JoinUsersAndMembersWithTheSameId()
        {
            var fixture = new Fixture();
            var users = fixture.CreateMany<User>(2);
            var members = fixture.CreateMany<Member>(2);

            users.First().docidentidad = "1";
            members.First().Docidentidad = "1";

            users.Last().docidentidad = "2";
            members.Last().Docidentidad = "2";

            var joined = UsersMembersJoiner.Join(users.ToList(), members.ToList()).UsersMembers;

            joined.Should().HaveCount(2);
            joined.First().User.Should().Be(users.First());
            joined.Last().User.Should().Be(users.Last());
            joined.First().Member.Should().Be(members.First());
            joined.Last().Member.Should().Be(members.Last());            
        }

        [Fact]
        public void LeftJoinUsersWithNoMemberInformation()
        {
            var fixture = new Fixture();
            var users = fixture.CreateMany<User>(2);
            var members = fixture.CreateMany<Member>(1);

            users.First().docidentidad = "1";
            members.First().Docidentidad = "1";

            var result = UsersMembersJoiner.Join(users.ToList(), members.ToList());

            var joined = result.UsersMembers;
            joined.Should().HaveCount(2);
            joined.First().User.Should().Be(users.First());
            joined.First().Member.Should().Be(members.First());
            joined.Last().User.Should().Be(users.Last());
            joined.Last().Member.Should().BeEquivalentTo(new Member());

            var onlyOnUsers = result.OnlyOnUsers;
            onlyOnUsers.Should().HaveCount(1);
            onlyOnUsers.Single().Should().Be(users.Last());
        }

        [Fact]
        public void NotReturnMembersWithNoUserInformation()
        {
            var fixture = new Fixture();
            var users = fixture.CreateMany<User>(1);
            var members = fixture.CreateMany<Member>(2);

            users.First().docidentidad = "1";
            members.First().Docidentidad = "1";
            members.Last().Docidentidad = "2";

            var result = UsersMembersJoiner.Join(users.ToList(), members.ToList());

            var joined = result.UsersMembers;
            joined.Should().HaveCount(1);
            joined.First().User.Should().Be(users.First());
            joined.First().Member.Should().Be(members.First());

            var onlyOnMembers = result.OnlyOnMembers;
            onlyOnMembers.Should().HaveCount(1);
            onlyOnMembers.Single().Should().Be(members.Last());
        }
    }
}
