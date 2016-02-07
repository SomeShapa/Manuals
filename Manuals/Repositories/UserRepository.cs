using Manuals.Entities;
using Manuals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manuals.Repositories
{
    public class UserRepository : IRepository<ApplicationUser>
    {
        private ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public void Add(ApplicationUser item)
        {
            context.Users.Add(item);
        }

        public void Delete(object id)
        {
            ApplicationUser user = context.Users.Find((string)id);
            if (user != null)
            {
                context.Users.Remove(user);
            }

        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return context.Users.ToList();
        }

        public ApplicationUser GetById(object id)
        {
            return context.Users.Find((string)id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(ApplicationUser item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}