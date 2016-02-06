using Manuals.Entities;
using Manuals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manuals.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Category item)
        {
            context.Categories.Add(item);
        }

        public void Delete(object id)
        {
            Category category = context.Categories.Find((int)id);
            if (category != null)
            {
                context.Categories.Remove(category);
            }
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public Category GetById(object id)
        {
            return context.Categories.Find((int)id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Category item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}