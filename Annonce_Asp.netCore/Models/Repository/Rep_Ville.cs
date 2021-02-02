using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.Models.Repository
{
    public class Rep_Ville : Repository<Ville>
    {
        public Rep_Ville(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        public void Add(Ville entity)
        {

            DbContext.villes.Add(entity);
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var Find_Ville = Find(id);
            DbContext.villes.Remove(Find_Ville);
            DbContext.SaveChanges();
        }

        public Ville Find(int id)
        {
            var fnd_Ville = DbContext.villes.SingleOrDefault(v => v.id == id);
            return fnd_Ville;
        }

        public IList<Ville> List()
        {
            var liste_Villes = DbContext.villes.ToList();
            return liste_Villes;
        }

        public void Update(int id, Ville entity)
        {
            DbContext.villes.Update(entity);
            DbContext.SaveChanges();
        }
    }
}
