using System;
using Xunit;

using Martial.Data.Models;
using Martial.Data.Services;

namespace Martial.Test
{
    public class ServiceTests
    {
        private DataService service;

        public ServiceTests()
        {
            service = new DataService();
            service.Initialise();
        }

        [Fact]
        public void EmptyDbShouldReturnNoMembers()
        {
            var m1 = new Member { Name = "Joe Bloggs", Dob = DateTime.Now.Date, HasInsurance = true };
            var m2 = new Member { Name = "Mary Bloggs", Dob = new DateTime(DateTime.Now.Date.Year-1,1,1), HasInsurance = false };
            service.AddMember(m1);
            service.AddMember(m2);

            var members = service.GetAllMembers();

            Assert.Equal(2, members.Count);

        }


        [Fact]
        public void OneMemberShouldHaveOneBadge()
        {
            var m1 = new Member { Name = "Joe Bloggs", Dob = DateTime.Now.Date, HasInsurance = true };
            m1.Badges.Add(new Badge { Title = "Blue", Awarded = new DateTime(2017, 1, 1) });
            service.AddMember(m1);

            var member = service.FindMemberById(m1.Id);

            Assert.Equal(1, member.Badges.Count);

        }

        [Fact]
        public void DeleteMemberShouldDeleteBadges()
        {
            var m1 = new Member { Name = "Joe Bloggs", Dob = DateTime.Now.Date, HasInsurance = true };
            m1.Badges.Add(new Badge { Title = "Blue", Awarded = new DateTime(2017, 1, 1) });
            m1.Badges.Add(new Badge { Title = "Green", Awarded = new DateTime(2018, 1, 1) });
            service.AddMember(m1);

            var member = service.FindMemberById(m1.Id);

            Assert.Equal(2, member.Badges.Count);

            var result = service.DeleteMember(member.Id);

            Assert.True(result);
        }
    }
}
