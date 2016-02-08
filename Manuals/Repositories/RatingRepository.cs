using Manuals.Entities;
using Manuals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manuals.Repositories
{
    public class RatingRepository : IRepository<Rating>
    {
        private ApplicationDbContext context;

        public RatingRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Rating item)
        {
            context.Ratings.Add(item);
        }

        public void Delete(object id)
        {
            Rating rating = context.Ratings.Find((int)id);
            if (rating != null)
            {
                context.Ratings.Remove(rating);
            }

        }

        public IEnumerable<Rating> GetAll()
        {
            return context.Ratings.ToList();
        }

        public Rating GetById(object id)
        {
            return context.Ratings.Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Rating item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}