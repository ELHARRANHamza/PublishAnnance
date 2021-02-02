using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.Models.Repository
{
    public class Rep_Annonces : Repository<Annonces>
    {
        public Rep_Annonces(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        public void Add(Annonces entity)
        {
            DbContext.annonces.Add(entity);
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var find_Ann = Find(id);
            DbContext.annonces.Remove(find_Ann);
            DbContext.SaveChanges();
        }

        public Annonces Find(int id)
        {
            var find_Ann = DbContext.annonces.Include(a=>a.categories).Include(a => a.ville).Include(a => a.region).Include(a => a.users).SingleOrDefault(a => a.id == id);
            return find_Ann;
        }

        public IList<Annonces> List()
        {
            var annonces = DbContext.annonces.Include(a => a.categories).Include(a => a.ville).Include(a => a.region).Include(a => a.users).ToList();
            return annonces;
        }

        public void Update(int id, Annonces entity)
        {
            DbContext.annonces.Update(entity);
            DbContext.SaveChanges();
        }
    }
}
