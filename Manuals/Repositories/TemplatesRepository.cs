using Manuals.Entities;
using Manuals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manuals.Repositories
{
    public class TemplatesRepository : IRepository<Template>
    {
        private ApplicationDbContext context;

        public TemplatesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Template item)
        {
            context.Templates.Add(item);
        }

        public void Delete(object id)
        {
            Template template = context.Templates.Find((int)id);
            if (template != null)
            {
                context.Templates.Remove(template);
            }
        }

        public IEnumerable<Template> GetAll()
        {
            return context.Templates.ToList();
        }

        public Template GetById(object id)
        {
            return context.Templates.Find((int)id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Template item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}