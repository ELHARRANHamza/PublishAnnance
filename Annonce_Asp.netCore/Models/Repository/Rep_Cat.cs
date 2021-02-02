using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.Models.Repository
{
    public class Rep_Cat : Repository<Categories>
    {
        public Rep_Cat(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        public void Add(Categories entity)
        {
            DbContext.categories.Add(entity);
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var find_Cat = Find(id);
            DbContext.categories.Remove(find_Cat);
            DbContext.SaveChanges();
        }

        public Categories Find(int id)
        {
            var cat = DbContext.categories.Include(a =>a.GetAnnonces).SingleOrDefault(c => c.id == id);
            return cat;
        }

        public IList<Categories> List()
        {
            var cat = DbContext.categories.Include(a => a.GetAnnonces).ToList();
            return cat;
        }

        public void Update(int id, Categories entity)
        {
            DbContext.categories.Update(entity);
            DbContext.SaveChanges();
        }
    }
}
