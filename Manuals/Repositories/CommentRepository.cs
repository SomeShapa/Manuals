using Manuals.Entities;
using Manuals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manuals.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private ApplicationDbContext context;

        public CommentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Comment item)
        {
            context.Comments.Add(item);
        }

        public void Delete(object id)
        {
            Comment comment = context.Comments.Find((int)id);
            if (comment != null)
            {
                context.Comments.Remove(comment);
            }
        }

        public IEnumerable<Comment> GetAll()
        {
            return context.Comments.ToList();
        }

        public Comment GetById(object id)
        {
            return context.Comments.Find((int)id);
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

        public void Update(Comment item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}