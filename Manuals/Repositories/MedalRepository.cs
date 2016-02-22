using Manuals.Entities;
using Manuals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manuals.Repositories
{
    public class MedalRepository : IRepository<Medal>
    {
        private ApplicationDbContext context;

        public MedalRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Medal item)
        {
            context.Medals.Add(item);
        }

        public void Delete(object id)
        {
            Medal Medal = context.Medals.Find((int)id);
            if (Medal != null)
            {
                context.Medals.Remove(Medal);
            }
        }

        public IEnumerable<Medal> GetAll()
        {
            return context.Medals.ToList();
        }

        public Medal GetById(object id)
        {
            return context.Medals.Find((int)id);
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public void Update(Medal item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}