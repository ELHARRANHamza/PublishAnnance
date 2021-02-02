using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.Models.Repository
{
    public class Rep_News : Repository<Latest_News>
    {
        public Rep_News(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        public void Add(Latest_News entity)
        {
            DbContext.news.Add(entity);
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var news = Find(id);
            DbContext.news.Remove(news);
            DbContext.SaveChanges();
        }

        public Latest_News Find(int id)
        {
            var find_News = DbContext.news.SingleOrDefault(n => n.id == id);
            return find_News;
                }

        public IList<Latest_News> List()
        {
            var List_News = DbContext.news.ToList();
            return List_News;
        }

        public void Update(int id, Latest_News entity)
        {
            DbContext.news.Update(entity);
            DbContext.SaveChanges();
        }
    }
}
