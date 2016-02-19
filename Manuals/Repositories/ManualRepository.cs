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
            Manual manual = context.Manuals.Find((int)id);
            if (manual != null)
            {
                context.Ratings.RemoveRange(manual.Ratings);
                foreach (Comment el in manual.Comments) {
                    context.RatingComments.RemoveRange(el.RatingComments);
                }
                context.Comments.RemoveRange(manual.Comments);
                context.Tags.RemoveRange(manual.Tags);
                context.Manuals.Remove(manual);
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
          //  context.SaveChanges();
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

        public void Update(Manual item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
