using Martial.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Martial.Data.Services
{
    public static class Seeder
    {
        public static void Seed()
        {
            var service = new DataService();

            service.Initialise();

            var m1 = new Member { Name = "Joe Bloggs", Dob = DateTime.Now.Date, 
                                InsuranceDue = DateTime.Parse("31/03/2020"), InsurancePaid = true };
            var m2 = new Member { Name = "Mary Bloggs", Dob = new DateTime(DateTime.Now.Date.Year - 1, 1, 1), 
                                InsuranceDue = DateTime.Parse("05/06/2020"), InsurancePaid = false };
            service.AddMember(m1);            
            service.AddBadgeToMember(m1.Id, new Badge { Title = "Blue", Awarded = DateTime.Now.Date });

            service.AddMember(m2);

            service.RegisterUser(new User { Username = "admin", Password = "admin", Role = Role.Admin });
            service.RegisterUser(new User { Username = "guest", Password = "guest", Role = Role.Guest });

        }
    }
}
