using Manuals.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manuals.Entities;

namespace Manuals.Repositories
{
    public class ManualRepository : IRepository<Manual>
    {
        private ApplicationDbContext context;

        public ManualRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Manual item)
        {
            context.Manuals.Add(item);
        }

        public void Delete(object id)
        {
            Manual Manual = context.Manuals.Find((int)id);
            if (Manual != null)
            {
                context.Manuals.Remove(Manual);
            }
        }

        public IEnumerable<Manual> GetAll()
        {
            return context.Manuals.ToList();
        }

        public Manual GetById(object id)
        {
            return context.Manuals.Find((int)id);
        }

        public void Save()
        {
            context.SaveChangesAsync();
        }

        public void Update(Manual item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
