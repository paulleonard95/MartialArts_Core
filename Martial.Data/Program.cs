using Martial.Data.Models;
using Martial.Data.Repositories;
using Martial.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Martial.Data
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new DataService();
            service.Initialise();

            var m1 = new Member { Name = "Joe Bloggs", Dob = DateTime.Now.Date, InsuranceDue = DateTime.Parse("31/05/20020"), InsurancePaid = true };
            m1.Badges.Add(new Badge { Title = "Blue", Awarded = new DateTime(2017, 1, 1) });
            m1.Badges.Add(new Badge { Title = "Green", Awarded = new DateTime(2018, 1, 1) });
            service.AddMember(m1);

            var member = service.FindMemberById(m1.Id);
            //var result = service.DeleteMember(member.Id);
            
            var members = service.GetAllMembers();
            foreach(var m in members)
            {
                Console.WriteLine($"{m.Id} - {m.Name} - {m.Dob} - {m.InsuranceDue} - {m.InsurancePaid}");
            }


        }
    }
}
