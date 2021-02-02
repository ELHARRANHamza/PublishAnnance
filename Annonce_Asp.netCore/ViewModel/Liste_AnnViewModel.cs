using Annonce_Asp.netCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.ViewModel
{
    public class Liste_AnnViewModel
    {
        public IList<Annonces> GetAnnonces { get; set; }
        public IList<Categories> GetCategories { get; set; }
        public IList<Latest_News> news { get; set; }
    }
}
