using Manuals.Entities;
using Manuals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manuals.Repositories
{
    public class UserProfileRepository : IRepository<UserProfile>
    {
        private ApplicationDbContext context;

        public UserProfileRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(UserProfile item)
        {
            context.UserProfiles.Add(item);
        }

        public void Delete(object id)
        {
            UserProfile userProfile = context.UserProfiles.Find((string)id);
            if (userProfile != null)
            {
                context.UserProfiles.Remove(userProfile);
            }

        }

        public IEnumerable<UserProfile> GetAll()
        {
            return context.UserProfiles.ToList();
        }

        public UserProfile GetById(object id)
        {
            return context.UserProfiles.Find((string)id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(UserProfile item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}