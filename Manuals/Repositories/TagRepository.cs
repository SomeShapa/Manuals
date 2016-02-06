using Manuals.Entities;
using Manuals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manuals.Repositories
{
    public class TagRepository : IRepository<Tag>
    {
        private ApplicationDbContext context;

        public TagRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Tag item)
        {
            context.Tags.Add(item);
        }

        public void Delete(object id)
        {
            Tag tag = context.Tags.Find((int)id);
            if (tag != null)
            {
                context.Tags.Remove(tag);
            }
        }

        public IEnumerable<Tag> GetAll()
        {
            return context.Tags.ToList();
        }

        public Tag GetById(object id)
        {
            return context.Tags.Find((int)id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Tag item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}