using Martial.Data.Models;
using Martial.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Martial.Data.Services
{
    public class DataService
    {
        private DataContext db;

        public DataService()
        {
            db = new DataContext();
        }

        public void Initialise()
        {
            db.Initialise();
        }

        public Member AddMember(Member m)
        {
            var exists = db.Members.FirstOrDefault(e => e.Dob == m.Dob);
            if (exists != null)
            {
                return null;
            }
            db.Members.Add(m);
            db.SaveChanges();
            return m;
        }

        public List<Member> GetAllMembers()
        {
            return db.Members.ToList();
        }

        public Member FindMemberById(int id)
        {
            return db.Members.Include(m => m.Badges).FirstOrDefault(m => m.Id == id);
        }

        public bool DeleteMember(int id)
        {
            var m = FindMemberById(id);
            if (m == null)
            {
                return false;
            }
            db.Members.Remove(m);
            db.SaveChanges();
            return true;
        }

        public void Update(Member obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Member AddBadgeToMember(int id, Badge b)
        {
            var m = FindMemberById(id);
            if (m == null)
            {
                return null;
            }
            //b.MemberId = m.Id;
            //db.Badges.Add(b);
            m.Badges.Add(b);
            db.SaveChanges();
            return m;
        }

        // Authentication
        // ==============   User management =========================

        /// <summary>
        /// Return the specified user
        /// </summary>
        /// <param name="id">id of the user to retrieve</param>
        /// <returns>The user if found otherwise null</returns>
        public User GetUserById(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Authenticate a user
        /// </summary>
        /// <param name="userName">User name of user to authenticate</param>
        /// <param name="password">Plain text password of user to authenticate</param>
        /// <returns>The user if authenticated, otherewise null</returns>
        public User Authenticate(string userName, string password)
        {
            // retrieve the user based on the UserName (assumes Username is unique)
            var user = db.Users
                .Where(u => u.Username == userName && password == u.Password)
                .FirstOrDefault();

            // user not authenticated
            if (user == null)
            {
                return null;
            }

            return user;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="u">User to register</param>
        /// <returns>The user if registered, otherewise null</returns>
        public User RegisterUser(User u)
        {
            // check that the user does not already exist (unique user name)
            var exists = db.Users.FirstOrDefault(x => x.Username == u.Username);
            if (exists != null)
            {
                return null;
            }

            // should really encrypt the password before storing in database          
            db.Users.Add(u);
            db.SaveChanges();
            return u;
        }

        /// <summary>
        /// Find a user by name (name should be unique)
        /// </summary>
        /// <param name="username">user username</param>
        /// <returns>The user if exists, otherewise null</returns>
        public User GetUserByName(string username)
        {
            return db.Users.FirstOrDefault(u => u.Username == username);
        }

    }
}
