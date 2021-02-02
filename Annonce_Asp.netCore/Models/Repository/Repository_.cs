using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.Models.Repository
{
   public interface Repository<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(int id, TEntity entity);
        void Delete(int id);
        //IList<TEntity> Search(string term);

    }
}
