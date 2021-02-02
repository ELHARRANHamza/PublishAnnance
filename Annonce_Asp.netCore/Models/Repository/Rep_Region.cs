using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.Models.Repository
{
    public class Rep_Region : Repository<Region>
    {
        public Rep_Region(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        public void Add(Region entity)
        {
            DbContext.regions.Add(entity);
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var find_Region = Find(id);
            DbContext.regions.Remove(find_Region);
            DbContext.SaveChanges();
        }

        public Region Find(int id)
        {
            var find_Region = DbContext.regions.SingleOrDefault(r => r.id == id);
            return find_Region;
        }

        public IList<Region> List()
        {
            var list_Regions = DbContext.regions.ToList();
            return list_Regions;
        }

        public void Update(int id, Region entity)
        {
            DbContext.regions.Update(entity);
            DbContext.SaveChanges();
        }
    }
}
